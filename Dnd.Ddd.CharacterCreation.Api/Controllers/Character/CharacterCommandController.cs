using System;

using Dnd.Ddd.CharacterCreation.Api.Controllers.Character.ChooseRace;
using Dnd.Ddd.CharacterCreation.Api.Controllers.Character.CreateCharacterDraft;
using Dnd.Ddd.CharacterCreation.Api.Controllers.Character.RollAbilityScores;
using Dnd.Ddd.Common.Infrastructure.Commands;
using Dnd.Ddd.Model.Character.Exceptions;
using Dnd.Ddd.Services.Commands;

using Microsoft.AspNetCore.Mvc;

namespace Dnd.Ddd.CharacterCreation.Api.Controllers.Character
{
    [Route("api/character"), ApiController]
    public class CharacterCommandController : ControllerBase
    {
        private readonly IIdResultCommandHandler<CreateCharacterDraftCommand> createDraftCommandHandler;

        private readonly IEmptyResultCommandHandler<RollAbilityScoresCommand> rollAbilitiesScoresHandler;

        private readonly IEmptyResultCommandHandler<ChooseCharacterRaceCommand> chooseCharacterRaceHandler;

        public CharacterCommandController(
            IIdResultCommandHandler<CreateCharacterDraftCommand> createDraftCommandHandler,
            IEmptyResultCommandHandler<RollAbilityScoresCommand> rollAbilitiesScoresHandler,
            IEmptyResultCommandHandler<ChooseCharacterRaceCommand> chooseCharacterRaceHandler)
        {
            this.createDraftCommandHandler = createDraftCommandHandler;
            this.rollAbilitiesScoresHandler = rollAbilitiesScoresHandler;
            this.chooseCharacterRaceHandler = chooseCharacterRaceHandler;
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CreateCharacterDraftResponse))]
        [ProducesResponseType(400, Type = typeof(string))]
        public IActionResult CreateDraft([FromBody] CreateCharacterDraftRequest request)
        {
            if (request == null || request.PlayerId == Guid.Empty)
            {
                return BadRequest("Not enough information provided to create CharacterDraft!");
            }

            var command = new CreateCharacterDraftCommand(request.PlayerId);
            var characterId = createDraftCommandHandler.Handle(command);

            var response = new CreateCharacterDraftResponse
            {
                DraftId = characterId
            };

            return StatusCode(201, response);
        }

        [HttpPut]
        [Route("abilityScores")]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(200)]
        public IActionResult AddAbilities([FromBody] RollAbilityScoresRequest request)
        {
            if (request == null || request.DraftId == Guid.Empty)
            {
                return BadRequest("Not enough information provided to roll ability scores");
            }

            var command = new RollAbilityScoresCommand
            {
                CharacterUiD = request.DraftId,
                Dexterity = request.Dexterity,
                Charisma = request.Charisma,
                Constitution = request.Constitution,
                Intelligence = request.Intelligence,
                Wisdom = request.Wisdom,
                Strength = request.Strength
            };

            IActionResult result;

            try
            {
                rollAbilitiesScoresHandler.Handle(command);
                result = Ok();
            }
            catch (InvalidOperationException ex)
            {
                result = BadRequest(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                result = BadRequest(ex.Message);
            }
            catch (CharacterNotFoundException ex)
            {
                result = NotFound(ex.Message);
            }

            return result;
        }

        [HttpPut]
        [Route("race")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        public IActionResult SetRace([FromBody] ChooseRaceRequest request)
        {
            if (request == null || request.DraftId == Guid.Empty)
            {
                return BadRequest("Not enough information provided to choose character race");
            }

            var command = new ChooseCharacterRaceCommand
            {
                CharacterUiD = request.DraftId,
                Race = request.Race
            };

            IActionResult result;
            try
            {
                chooseCharacterRaceHandler.Handle(command);
                result = Ok();
            }
            catch (CharacterNotFoundException e)
            {
                result = NotFound(e.Message);
            }
            catch (ArgumentException e)
            {
                result = BadRequest(e.Message);
            }

            return result;
        }
    }
}