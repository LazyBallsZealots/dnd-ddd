using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Dnd.Ddd.CharacterCreation.Api.Controllers.Character.CreateDraft;
using Dnd.Ddd.CharacterCreation.Api.Tests.Fixture;

using Xunit;

namespace Dnd.Ddd.CharacterCreation.Api.Tests.Specifications.Characters
{
    public class CharacterControllerTests : BaseIntegrationTest
    {
        public CharacterControllerTests(IntegrationTestsFixture fixture)
            : base(fixture)
        {
        }

        [Fact]
        public async Task ControllerExists()
        {
            var playerId = Guid.NewGuid();
            var request = new CreateDraftRequest
            {
                PlayerId = playerId
            };
            var requestBody = JsonSerializer.Serialize(request);

            _ = await Client.PostAsync("api/character/new", new StringContent(requestBody, Encoding.UTF8, "application/json"));
        }
    }
}