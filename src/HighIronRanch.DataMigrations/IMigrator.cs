namespace HighIronRanch.DataMigrations
{
	public interface IMigrator
	{
	    int GetCurrentVersion();
        void MigrateUp();
        void MigrateUp(int currentVersion);
	    void MigrateDown(int targetVersion);
        void MigrateDown(int currentVersion, int targetVersion);
	}
}