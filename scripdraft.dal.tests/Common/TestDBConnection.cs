using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

using ScripDraft.Data;

namespace ScripDraft.Tests.DAL
{
    public class TestDBConnection : IDisposable
    {
        private const string AppSettingsFile = "appsettings.json";
        
        private IConfiguration _configuration = null;

        public TestDBConnection()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(AppSettingsFile, optional: false, reloadOnChange: true);
            
            _configuration = builder.Build();
        }   

        public async void Dispose()
        {
            await SDDatabase.DropDatabase(_configuration);
        }

        public IMongoDatabase Connection 
        { 
            get 
            {
                return SDDatabase.GetDatabase(_configuration);
            } 
        }
    }
}