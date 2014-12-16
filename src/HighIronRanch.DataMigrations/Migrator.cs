using System.Collections.Generic;
using System.Linq;

namespace HighIronRanch.DataMigrations
{
	public class Migrator : IMigrator
	{
		private readonly IEnumerable<IMigration> _allMigrations;

		public Migrator(IEnumerable<IMigration> allMigrations)
		{
			_allMigrations = allMigrations;
		}

		public void MigrateUp(int currentVersion)
		{
			var neededMigrations = _allMigrations.Where(m => m.Version > currentVersion).OrderBy(m => m.Version);
			foreach (var migration in neededMigrations)
			{
				migration.Up();
			}
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