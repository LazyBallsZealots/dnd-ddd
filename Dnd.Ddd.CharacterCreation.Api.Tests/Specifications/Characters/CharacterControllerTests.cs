using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Dnd.Ddd.CharacterCreation.Api.Controllers.Character.CreateCharacterDraft;
using Dnd.Ddd.CharacterCreation.Api.Tests.Fixture;
using Dnd.Ddd.CharacterCreation.Api.Tests.TestsCollection.Names;
using Dnd.Ddd.Model.Character;

using Xunit;

namespace Dnd.Ddd.CharacterCreation.Api.Tests.Specifications.Characters
{
    [Collection(TestCollectionNames.IntegrationTestsCollection), Trait("Category", "Integration tests: controllers")]
    public class CharacterControllerTests : IDisposable
    {
        private const string ApiRoot = "api/character";

        private const string ContentType = "application/json";

        private readonly IntegrationTestsFixture fixture;

        private readonly HttpClient client;

        public CharacterControllerTests(IntegrationTestsFixture fixture)
        {
            this.fixture = fixture;
            client = fixture.CreateClient();
        }

        [Fact]
        public async Task CharacterController_OnPostingValidCreateCharacterDraftRequest_SavesNewCharacterDraft()
        {
            var playerId = Guid.NewGuid();
            var request = new CreateCharacterDraftRequest
            {
                PlayerId = playerId
            };
            var requestBody = JsonSerializer.Serialize(request);

            var response = await client.PostAsync(ApiRoot, new StringContent(requestBody, Encoding.UTF8, ContentType));

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var responseContent = JsonSerializer.Deserialize<CreateCharacterDraftResponse>(responseString);
            Assert.NotEqual(Guid.Empty, responseContent.DraftId);

            var savedCharacterDraftResponse = await client.GetAsync($"{ApiRoot}?characterId={responseContent.DraftId}");

            savedCharacterDraftResponse.EnsureSuccessStatusCode();
            var savedCharacterDraft = JsonSerializer.Deserialize<CharacterDraft>(await savedCharacterDraftResponse.Content.ReadAsStringAsync());
            Assert.NotNull(savedCharacterDraft);
        }

        [Fact]
        public async Task CharacterController_OnPostingInvalidCreateCharacterDraftRequest_ReturnsBadRequest()
        {
            var request = new CreateCharacterDraftRequest { PlayerId = Guid.Empty };
            var requestBody = JsonSerializer.Serialize(request);

            var response = await client.PostAsync(ApiRoot, new StringContent(requestBody, Encoding.UTF8, ContentType));
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        public void Dispose() => fixture.ClearDatabase();
    }
}