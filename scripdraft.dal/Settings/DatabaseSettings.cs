using Microsoft.Extensions.Configuration;

namespace ScripDraft.Data.Settings
{
    internal static class DatabaseSettings
    {
        public static string ReadConnectionString(IConfiguration configuration)
        {
            string connectionString = configuration.GetValue<string>("DatabaseSettings:ConnectionString");
            
            return connectionString;
        }

        public static string ReadDatabaseName(IConfiguration configuration)
        {
            string databaseName = configuration.GetValue<string>("DatabaseSettings:DatabaseName");
            
            return databaseName;
        }
    }
}