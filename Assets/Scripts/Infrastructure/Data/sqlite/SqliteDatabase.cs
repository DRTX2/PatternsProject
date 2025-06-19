using System;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;

namespace Assets.Scripts.Infrastructure.Data.sqlite
{
    public class SqliteDatabase
    {
        private static SqliteDatabase _instance;
        private static SqliteOptions _options;
        private static readonly object _lock = new();

        private SqliteDatabase() { }

        public static SqliteDatabase GetInstance()
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    _instance ??= new SqliteDatabase();
                }
            }
            return _instance;
        }

        public void Initialize(SqliteOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public IDbConnection GetConnection()
        {
            if (_options == null)
                throw new InvalidOperationException("Database has not been initialized");

            var connectionString = $"URI=file:{_options.DatabasePath}";
            return new SqliteConnection(connectionString);
        }
    }
}
