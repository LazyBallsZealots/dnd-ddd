using Dnd.Ddd.CharacterCreation.Api.Tests.Fixture;
using Dnd.Ddd.CharacterCreation.Api.Tests.TestsCollection.Names;

using Xunit;

namespace Dnd.Ddd.CharacterCreation.Api.Tests.TestsCollection
{
    [CollectionDefinition(TestCollectionNames.IntegrationTestsCollection)]
    public class IntegrationTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture>
    {
    }
}