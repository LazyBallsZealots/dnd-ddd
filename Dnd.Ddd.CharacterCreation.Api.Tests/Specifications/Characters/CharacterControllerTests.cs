using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Dnd.Ddd.CharacterCreation.Api.Controllers.Character.CreateCharacterDraft;
using Dnd.Ddd.CharacterCreation.Api.Tests.Fixture;
using Dnd.Ddd.CharacterCreation.Api.Tests.TestsCollection.Names;

using Xunit;

namespace Dnd.Ddd.CharacterCreation.Api.Tests.Specifications.Characters
{
    [Collection(TestCollectionNames.IntegrationTestsCollection), Trait("Category", "Integration tests: controllers")]
    public class CharacterControllerTests
    {
        private readonly HttpClient client;

        public CharacterControllerTests(IntegrationTestsFixture fixture)
        {
            client = fixture.CreateClient();
        }

        [Fact]
        public async Task CharacterController_OnPostingValidCreateCharacterDraftRequest_ReturnsValidResponse()
        {
            var playerId = Guid.NewGuid();
            var request = new CreateCharacterDraftRequest
            {
                PlayerId = playerId
            };
            var requestBody = JsonSerializer.Serialize(request);

            var response = await client.PostAsync("api/character", new StringContent(requestBody, Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var responseContent = JsonSerializer.Deserialize<CreateCharacterDraftResponse>(responseString);
            Assert.NotEqual(Guid.Empty, responseContent.DraftId);
        }

        [Fact]
        public async Task CharacterController_OnPostingInvalidCreateCharacterDraftRequest_ReturnsBadRequest()
        {
            CreateCharacterDraftRequest request = null;
            var requestBody = JsonSerializer.Serialize(request);

            var response = await client.PostAsync("api/character", new StringContent(requestBody, Encoding.UTF8, "application/json"));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}