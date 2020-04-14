using Dnd.Ddd.Infrastructure.Tests.Fixture;
using Dnd.Ddd.Infrastructure.Tests.TestsCollection.Names;

using Xunit;

namespace Dnd.Ddd.Infrastructure.Tests.TestsCollection
{
    [CollectionDefinition(TestCollectionNames.IntegrationTestsCollection)]
    public class IntegrationTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture>
    {
    }
}