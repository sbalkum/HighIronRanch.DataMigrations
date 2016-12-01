using System.Collections.Generic;
using System.Linq;

namespace HighIronRanch.DataMigrations
{
	public class Migrator : IMigrator
	{
	    private readonly IVersionRepository _repository;
	    protected readonly IEnumerable<IMigration> _allMigrations;

	    public Migrator(IVersionRepository repository, IEnumerable<IMigration> allMigrations)
	    {
	        _repository = repository;
	        _allMigrations = allMigrations;
	    }

	    public int GetCurrentVersion()
	    {
	        return _repository.GetCurrentVersion();
	    }

	    public void MigrateUp()
	    {
	        MigrateUp(GetCurrentVersion());
	    }

        public void MigrateUp(int currentVersion)
		{
			var neededMigrations = _allMigrations.Where(m => m.Version > currentVersion).OrderBy(m => m.Version);
			foreach (var migration in neededMigrations)
			{
				migration.Up();
			}
		}

	    public void MigrateDown(int targetVersion)
	    {
	        MigrateDown(GetCurrentVersion(), targetVersion);
	    }

		public void MigrateDown(int currentVersion, int targetVersion)
		{
			var neededMigrations = _allMigrations
				.Where(m => m.Version < currentVersion && m.Version >= targetVersion)
				.OrderByDescending(m => m.Version);
			foreach (var migration in neededMigrations)
			{
				migration.Down();
			}
		}
	}
}