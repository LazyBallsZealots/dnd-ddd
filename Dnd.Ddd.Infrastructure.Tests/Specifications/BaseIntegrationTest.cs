using System.Net.Http;

using Dnd.Ddd.CharacterCreation.Api.Tests.Fixture;
using Dnd.Ddd.CharacterCreation.Api.Tests.TestsCollection.Names;

using Microsoft.AspNetCore.Mvc.Testing;

using Xunit;

namespace Dnd.Ddd.CharacterCreation.Api.Tests.Specifications
{
    [Collection(TestCollectionNames.IntegrationTestsCollection)]
    public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestsFixture>
    {
        protected BaseIntegrationTest(IntegrationTestsFixture fixture)
        {
            Fixture = fixture;
            Client = fixture.CreateClient(
                new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false
                });

            DbManager = new DatabaseManager(fixture.Container);
        }

        protected IntegrationTestsFixture Fixture { get; }

        protected HttpClient Client { get; }

        protected DatabaseManager DbManager { get; }
    }
}