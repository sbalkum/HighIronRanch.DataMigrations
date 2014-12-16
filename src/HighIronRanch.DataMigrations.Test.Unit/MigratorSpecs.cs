using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;
using Machine.Specifications;
using Rhino.Mocks;

namespace HighIronRanch.DataMigrations.Test.Unit
{
    public class MigratorSpecs
    {
	    [Subject(typeof (Migrator))]
	    public class Concern : Observes<Migrator>
	    {
		    protected static IMigration migration1;
		    protected static IMigration migration2;
			protected static IMigration migration3;

		    private Establish ee = () =>
		    {
			    var migrations = new List<IMigration>();
			    migration1 = fake.an<IMigration>();
			    migration1.setup(m => m.Version).Return(1);
			    migrations.Add(migration1);
				migration2 = fake.an<IMigration>();
				migration2.setup(m => m.Version).Return(2);
				migrations.Add(migration2);
				migration3 = fake.an<IMigration>();
				migration3.setup(m => m.Version).Return(3);
				migrations.Add(migration3);

			    depends.on((IEnumerable<IMigration>)migrations);
		    };
	    }

	    public class When_migrating_up_from_version_1 : Concern
	    {
		    private Because b = () => sut.MigrateUp(1);

		    private It should_not_migrate_to_version_1 = () => migration1.AssertWasNotCalled(m => m.Up());
			private It should_migrate_to_version_2 = () => migration2.AssertWasCalled(m => m.Up());
			private It should_migrate_to_version_3 = () => migration3.AssertWasCalled(m => m.Up());
		}

	    public class When_migrating_down_from_version_3 : Concern
	    {
		    private Because b = () => sut.MigrateDown(3, 1);

		    private It should_not_migrate_down_version_3 = () => migration3.AssertWasNotCalled(m => m.Down());
			private It should_migrate_down_version_2 = () => migration2.AssertWasCalled(m => m.Down());
			private It should_migrate_down_version_1 = () => migration1.AssertWasCalled(m => m.Down());
		}
    }
}
