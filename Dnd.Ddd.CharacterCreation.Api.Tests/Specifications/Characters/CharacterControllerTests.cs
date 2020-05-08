using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Dnd.Ddd.CharacterCreation.Api.Controllers.Character.AddAbilities;
using Dnd.Ddd.CharacterCreation.Api.Controllers.Character.CreateCharacterDraft;
using Dnd.Ddd.CharacterCreation.Api.Controllers.Character.GetCharacter;
using Dnd.Ddd.CharacterCreation.Api.Tests.Fixture;
using Dnd.Ddd.CharacterCreation.Api.Tests.TestsCollection.Names;
using Dnd.Ddd.Model.Character;

using Xunit;

namespace Dnd.Ddd.CharacterCreation.Api.Tests.Specifications.Characters
{
    [Trait("Category", TestCategory)]
    [Collection(TestCollectionNames.IntegrationTestsCollection)]
    public class CharacterControllerTests : IDisposable
    {
        private const string TestCategory = "Integration tests: controllers";

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

        // not finished
        [Fact]
        public async Task CharacterController_OnRequestingAbilityRollOnValidCharacterDraft_UpdatesCharacterDraft()
        {
            var playerId = Guid.NewGuid();
            var createCharacterRequest = new CreateCharacterDraftRequest
            {
                PlayerId = playerId
            };

            var createCharacterRequestBody = JsonSerializer.Serialize(createCharacterRequest);
            var response = await client.PostAsync(ApiRoot, new StringContent(createCharacterRequestBody, Encoding.UTF8, ContentType));
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var responseContent = JsonSerializer.Deserialize<CreateCharacterDraftResponse>(responseString);


            var rollAbilitiesRequest = new RollAbilityScoresRequest
            {
                DraftId = responseContent.DraftId,
                Strength = 10,
                Wisdom = 15,
                Intelligence = 7,
                Charisma = 18,
                Constitution = 14,
                Dexterity = 15
            };

            var rollAbilitiesRequestBody = JsonSerializer.Serialize(rollAbilitiesRequest);
            var rollAbilitiesResponse = await client.PutAsync(ApiRoot, new StringContent(rollAbilitiesRequestBody, Encoding.UTF8, ContentType));
            response.EnsureSuccessStatusCode();

            var savedCharacterResponse = await client.GetAsync($"{ApiRoot}?characterId={responseContent.DraftId}");
            savedCharacterResponse.EnsureSuccessStatusCode();

        }

        public void Dispose() => fixture.ClearDatabase();

    }
}