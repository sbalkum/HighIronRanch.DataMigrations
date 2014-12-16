namespace HighIronRanch.DataMigrations
{
	public interface IMigrator
	{
		void MigrateUp(int currentVersion);
		void MigrateDown(int currentVersion, int targetVersion);
	}
}