using System.Data;
using Dapper;
using Microsoft.Extensions.Options;

namespace AccountsApi.DataContext
{
    public class ApplicationDbContext
    {
        private DatabaseSettings _databaseSettings;

        public ApplicationDbContext(IOptions<DatabaseSettings> databaseSettings)
        {
            _databaseSettings = databaseSettings.Value;
        }

        public IDbConnection CreateConnection()
        {
            var connectionString = $"Host={_databaseSettings.Server}; Database={_databaseSettings.Database}; User Id={_databaseSettings.UserId}; Password={_databaseSettings.Password};";
            return new Npgsql.NpgsqlConnection(connectionString);
        }

        public async Task Init()
        {
            await _initDatabase();
            await _createTables();
        }

        private async Task _initDatabase()
        {
            //create database if not exists
            var connectionString = $"Host={_databaseSettings.Server}; Database=postgres; User Id={_databaseSettings.UserId}; Password={_databaseSettings.Password};";
            using var connect = new Npgsql.NpgsqlConnection(connectionString);
            var sqlDbCount = $"SELECT COUNT(*) FROM pg_database WHERE datname = '{_databaseSettings.Database}';";
            var dbCount = await connect.ExecuteScalarAsync<int>(sqlDbCount);
            if(dbCount == 0)
            {
                var sqlCreateDb = $"CREATE DATABASE \"_databaseSettings.Database\"";
                await connect.ExecuteAsync(sqlCreateDb);
            }
            
        }

        private async Task _createTables()
        {
            using var connect = CreateConnection();
            await _initUsers();

             async Task _initUsers()
             {
                var sqlCreateTable = """
                            CREATE TABLE IF NOT EXISTS Users 
                            (
                                Id SERIAL PRIMARY KEY, 
                                Title VARCHAR (50) NOT NULL,
                                FirstName VARCHAR (50) NOT NULL,
                                LastName VARCHAR (50) NOT NULL, 
                                Email VARCHAR (50) UNIQUE NOT NULL,
                                Role VARCHAR NOT NULL, 
                                PasswordHash VARCHAR NOT NULL);

                """;
                await connect.ExecuteAsync(sqlCreateTable);
             }
        }
    }
}