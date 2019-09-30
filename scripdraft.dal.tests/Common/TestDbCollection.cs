using Xunit;

namespace ScripDraft.Tests.DAL
{
    [CollectionDefinition("Test DB collection")]
    public class TestDbCollection : ICollectionFixture<TestDBConnection>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}