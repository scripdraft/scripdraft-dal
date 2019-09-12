using System;

namespace ScripDraft.Tests.DAL
{
    public class TestDBConnection : IDisposable
    {
        public TestDBConnection()
        {
        //     var database = VSCDatabase.GetSCInMemoryDatabase();

        //     var serviceProvider = DatabaseMigrationRunner.CreateServices(database);
        //     DatabaseMigrationRunner.MigrateDatabase(serviceProvider);

        //     EntityMapper.InitEntityMaps();
        }   

        public void Dispose()
        {
           // Connection.Dispose();
        }

        // public IDbConnection Connection { 
        //     get 
        //     {
        //         return VSCDatabase.GetInMemoryIDBConnection();
        //     } 
        // }
    }
}