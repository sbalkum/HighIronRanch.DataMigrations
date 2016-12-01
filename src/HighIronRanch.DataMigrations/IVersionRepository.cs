namespace HighIronRanch.DataMigrations
{
    public interface IVersionRepository
    {
        int GetCurrentVersion();
        void SetCurrentVersion(int version);
    }
}