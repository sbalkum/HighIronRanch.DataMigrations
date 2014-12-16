namespace HighIronRanch.DataMigrations
{
	public interface IMigration
	{
		int Version { get; }
		void Up();
		void Down();
	}
}