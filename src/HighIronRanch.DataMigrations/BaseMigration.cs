using System;
using System.Text.RegularExpressions;

namespace HighIronRanch.DataMigrations
{
    public abstract class BaseMigration : IMigration
    {
        private readonly IVersionRepository _repository;

        protected BaseMigration(IVersionRepository repository)
        {
            _repository = repository;
        }

        public int Version
        {
            get
            {
                var name = GetType().Name;
                var match = Regex.Match(name, "[0-9]+$");
                if (!match.Success)
                    throw new Exception("Poorly named migration: " + name + ". Cannot determine version.");
                var version = int.Parse(match.Value);
                return version;
            }
        }

        public void Up()
        {
            UpWork();
            _repository.SetCurrentVersion(Version);
        }

        public void Down()
        {
            DownWork();
            _repository.SetCurrentVersion(Version);
        }

        public abstract void UpWork();
        public abstract void DownWork();
    }
}