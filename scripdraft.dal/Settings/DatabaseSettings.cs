using Microsoft.Extensions.Configuration;

namespace ScripDraft.Data.Settings
{
    internal static class DatabaseSettings
    {
        public static string ReadConnectionString(IConfiguration configuration) => configuration.GetValue<string>("DatabaseSettings:ConnectionString");

        public static string ReadDatabaseName(IConfiguration configuration) => configuration.GetValue<string>("DatabaseSettings:DatabaseName");
    }
}