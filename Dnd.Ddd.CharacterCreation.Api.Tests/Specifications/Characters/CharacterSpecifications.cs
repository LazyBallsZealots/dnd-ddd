using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Dnd.Ddd.CharacterCreation.Api.Controllers.Character.ChooseName;
using Dnd.Ddd.CharacterCreation.Api.Controllers.Character.ChooseRace;
using Dnd.Ddd.CharacterCreation.Api.Controllers.Character.CreateCharacterDraft;
using Dnd.Ddd.CharacterCreation.Api.Controllers.Character.RollAbilityScores;
using Dnd.Ddd.CharacterCreation.Api.Tests.Fixture;
using Dnd.Ddd.CharacterCreation.Api.Tests.TestsCollection.Names;
using Dnd.Ddd.Dtos;

using Xunit;

namespace Dnd.Ddd.CharacterCreation.Api.Tests.Specifications.Characters
{
    [Trait("Category", TestCategory), Collection(TestCollectionNames.IntegrationTestsCollection)]
    public class CharacterSpecifications : IDisposable
    {
        private const string TestCategory = "Specifications: Character";

        private const string ApiRoot = "api/character";

        private const string ContentType = "application/json";

        private readonly IntegrationTestsFixture fixture;

        private readonly HttpClient client;

        public CharacterSpecifications(IntegrationTestsFixture fixture)
        {
            this.fixture = fixture;
            client = fixture.CreateClient();
        }

        public void Dispose() => fixture.ClearDatabase();

        [Fact]
        public async Task CharacterController_OnPostingValidCreateCharacterDraftRequest_SavesNewCharacterDraft()
        {
            var requestBody = JsonSerializer.Serialize(new CreateCharacterDraftRequest { PlayerId = Guid.NewGuid() });
            var responseContent = await GetPostResponse<CreateCharacterDraftResponse>(ApiRoot, requestBody);

            Assert.NotEqual(Guid.Empty, responseContent.DraftId);

            var savedCharacterDraft = await GetCharacterDto(responseContent.DraftId);

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

        [Fact]
        public async Task CharacterController_OnRequestingAbilityRollOnValidCharacterDraft_UpdatesCharacterDraft()
        {
            var requestBody = JsonSerializer.Serialize(new CreateCharacterDraftRequest { PlayerId = Guid.NewGuid() });
            var responseContent = await GetPostResponse<CreateCharacterDraftResponse>(ApiRoot, requestBody);

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

            var putResponse = await client.PutAsync(
                                  $"{ApiRoot}/abilityScores",
                                  new StringContent(rollAbilitiesRequestBody, Encoding.UTF8, ContentType));

            putResponse.EnsureSuccessStatusCode();

            var savedCharacterDto = await GetCharacterDto(responseContent.DraftId);

            Assert.Equal(rollAbilitiesRequest.Strength, savedCharacterDto.Strength);
            Assert.Equal(rollAbilitiesRequest.Dexterity, savedCharacterDto.Dexterity);
            Assert.Equal(rollAbilitiesRequest.Charisma, savedCharacterDto.Charisma);
            Assert.Equal(rollAbilitiesRequest.Constitution, savedCharacterDto.Constitution);
            Assert.Equal(rollAbilitiesRequest.Wisdom, savedCharacterDto.Wisdom);
            Assert.Equal(rollAbilitiesRequest.Intelligence, savedCharacterDto.Intelligence);
        }

        [Fact]
        public async Task CharacterController_OnRequestingAbilitiesRollOnNonExistingDraft_ReturnsNotFound()
        {
            var rollAbilitiesRequest = new RollAbilityScoresRequest { DraftId = Guid.NewGuid() };
            var rollAbilitiesRequestBody = JsonSerializer.Serialize(rollAbilitiesRequest);
            var response = await client.PutAsync(
                               $"{ApiRoot}/abilityScores",
                               new StringContent(rollAbilitiesRequestBody, Encoding.UTF8, ContentType));

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CharacterController_OnRequestingAbilitiesRollOnWithEmptyId_ReturnsBadRequest()
        {
            var rollAbilitiesRequest = new RollAbilityScoresRequest { DraftId = Guid.Empty };
            var rollAbilitiesRequestBody = JsonSerializer.Serialize(rollAbilitiesRequest);
            var response = await client.PutAsync(
                               $"{ApiRoot}/abilityScores",
                               new StringContent(rollAbilitiesRequestBody, Encoding.UTF8, ContentType));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CharacterController_OnRequestingAbilitiesRollOnInvalidRoll_ReturnsBadRequest()
        {
            var requestBody = JsonSerializer.Serialize(new CreateCharacterDraftRequest { PlayerId = Guid.NewGuid() });
            var responseContent = await GetPostResponse<CreateCharacterDraftResponse>(ApiRoot, requestBody);

            var rollAbilitiesRequestBody = JsonSerializer.Serialize(new RollAbilityScoresRequest { DraftId = responseContent.DraftId });
            var rollAbilitiesResponse = await client.PutAsync(
                                            $"{ApiRoot}/abilityScores",
                                            new StringContent(rollAbilitiesRequestBody, Encoding.UTF8, ContentType));

            Assert.Equal(HttpStatusCode.BadRequest, rollAbilitiesResponse.StatusCode);
        }

        [Fact]
        public async Task CharacterController_OnRequestingSettingRaceOnValidCharacterDraft_SetsCharactersRace()
        {
            const string ChosenRace = "Dwarf";

            var requestBody = JsonSerializer.Serialize(new CreateCharacterDraftRequest { PlayerId = Guid.NewGuid() });
            var responseContent = await GetPostResponse<CreateCharacterDraftResponse>(ApiRoot, requestBody);

            var chooseRaceRequest =
                JsonSerializer.Serialize(new ChooseRaceRequest { DraftId = responseContent.DraftId, Race = ChosenRace });
            var chooseRaceResponse = await client.PutAsync(
                                         $"{ApiRoot}/race",
                                         new StringContent(chooseRaceRequest, Encoding.UTF8, ContentType));

            chooseRaceResponse.EnsureSuccessStatusCode();

            var characterDto = await GetCharacterDto(responseContent.DraftId);

            Assert.Equal(ChosenRace, characterDto.Race);
        }

        [Fact]
        public async Task CharacterController_OnRequestingSettingRaceOnNonExistentCharacter_ReturnsNotFound()
        {
            const string ChosenRace = "Dwarf";
            var chooseRaceRequest = JsonSerializer.Serialize(new ChooseRaceRequest { DraftId = Guid.NewGuid(), Race = ChosenRace });
            var chooseRaceResponse = await client.PutAsync(
                                         $"{ApiRoot}/race",
                                         new StringContent(chooseRaceRequest, Encoding.UTF8, ContentType));

            Assert.Equal(HttpStatusCode.NotFound, chooseRaceResponse.StatusCode);
        }

        [Fact]
        public async Task CharacterController_OnRequestingSettingRaceWithNotRegisteredRace_ReturnsBadRequest()
        {
            const string ChosenRace = "Exception thrower";
            var requestBody = JsonSerializer.Serialize(new CreateCharacterDraftRequest { PlayerId = Guid.NewGuid() });
            var responseContent = await GetPostResponse<CreateCharacterDraftResponse>(ApiRoot, requestBody);

            var chooseRaceRequest =
                JsonSerializer.Serialize(new ChooseRaceRequest { DraftId = responseContent.DraftId, Race = ChosenRace });
            var chooseRaceResponse = await client.PutAsync(
                                         $"{ApiRoot}/race",
                                         new StringContent(chooseRaceRequest, Encoding.UTF8, ContentType));

            Assert.Equal(HttpStatusCode.BadRequest, chooseRaceResponse.StatusCode);
        }

        [Fact]
        public async Task CharacterController_OnRequestingSettingNameOnValidCharacterDraft_SetsCharactersName()
        {
            const string chosenName = "Beczka";

            var requestBody = JsonSerializer.Serialize(new CreateCharacterDraftRequest { PlayerId = Guid.NewGuid() });
            var responseContent = await GetPostResponse<CreateCharacterDraftResponse>(ApiRoot, requestBody);

            var chooseNameRequest =
                JsonSerializer.Serialize(new ChooseNameRequest { DraftId = responseContent.DraftId, Name = chosenName });
            var chooseNameResponse = await client.PutAsync(
                                         $"{ApiRoot}/name",
                                         new StringContent(chooseNameRequest, Encoding.UTF8, ContentType));

            chooseNameResponse.EnsureSuccessStatusCode();

            var characterDto = await GetCharacterDto(responseContent.DraftId);

            Assert.Equal(chosenName, characterDto.Name);
        }


        [Fact]
        public async Task CharacterController_OnRequestingSettingNameOnNonExistentCharacter_ReturnsNotFound()
        {
            const string chosenName = "Beczka";
            var chooseNameRequest = JsonSerializer.Serialize(new ChooseNameRequest { DraftId = Guid.NewGuid(), Name = chosenName });
            var chooseNameResponse = await client.PutAsync(
                                         $"{ApiRoot}/name",
                                         new StringContent(chooseNameRequest, Encoding.UTF8, ContentType));

            Assert.Equal(HttpStatusCode.NotFound, chooseNameResponse.StatusCode);
        }

        [Fact]
        public async Task CharacterController_OnRequestingSettingEmptyName_ReturnsBadRequest()
        {
            const string chosenName = "";

            var requestBody = JsonSerializer.Serialize(new CreateCharacterDraftRequest { PlayerId = Guid.NewGuid() });
            var responseContent = await GetPostResponse<CreateCharacterDraftResponse>(ApiRoot, requestBody);

            var chooseNameRequest = JsonSerializer.Serialize(new ChooseNameRequest { DraftId = responseContent.DraftId, Name = chosenName });
            var chooseNameResponse = await client.PutAsync(
                                         $"{ApiRoot}/name",
                                         new StringContent(chooseNameRequest, Encoding.UTF8, ContentType));

            Assert.Equal(HttpStatusCode.BadRequest, chooseNameResponse.StatusCode);
        }


        [Fact]
        public async Task CharacterController_AfterCompletingCharacterDetails_CompletesCharacter()
        {
            var requestBody = JsonSerializer.Serialize(new CreateCharacterDraftRequest { PlayerId = Guid.NewGuid() });
            var responseContent = await GetPostResponse<CreateCharacterDraftResponse>(ApiRoot, requestBody);

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
            var chooseRaceRequest = JsonSerializer.Serialize(new ChooseRaceRequest { DraftId = responseContent.DraftId, Race = "Dwarf" });
            var chooseNameRequest = JsonSerializer.Serialize(new ChooseNameRequest { DraftId = responseContent.DraftId, Name = "Beczka" });

            var abilityScoresResponse = await client.PutAsync(
                               $"{ApiRoot}/abilityScores",
                               new StringContent(rollAbilitiesRequestBody, Encoding.UTF8, ContentType));
            abilityScoresResponse.EnsureSuccessStatusCode();

            var raceChosenResponse = await client.PutAsync(
                               $"{ApiRoot}/race",
                               new StringContent(chooseRaceRequest, Encoding.UTF8, ContentType));
            raceChosenResponse.EnsureSuccessStatusCode();

            var nameChosenResponse = await client.PutAsync(
                       $"{ApiRoot}/name",
                       new StringContent(chooseNameRequest, Encoding.UTF8, ContentType));
            nameChosenResponse.EnsureSuccessStatusCode();

            var characterDto = await GetCharacterDto(responseContent.DraftId);

            Assert.Equal("Completed", characterDto.Stage);
        }

        private async Task<TResponse> GetPostResponse<TResponse>(string url, string request)
        {
            var response = await client.PostAsync(url, new StringContent(request, Encoding.UTF8, ContentType));
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponse>(responseString);
        }

        private async Task<CharacterDto> GetCharacterDto(Guid characterUiD)
        {
            var savedCharacterResponse = await client.GetAsync($"{ApiRoot}?characterId={characterUiD}");
            savedCharacterResponse.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<CharacterDto>(await savedCharacterResponse.Content.ReadAsStringAsync());
        }
    }
}