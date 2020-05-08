using System;
using Dnd.Ddd.CharacterCreation.Api.Controllers.Character.AddAbilities;
using Dnd.Ddd.CharacterCreation.Api.Controllers.Character.CreateCharacterDraft;
using Dnd.Ddd.Common.Infrastructure.Commands;
using Dnd.Ddd.Common.Infrastructure.Queries;
using Dnd.Ddd.Services.Commands;
using Dnd.Ddd.Services.Queries;
using Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Dnd.Ddd.CharacterCreation.Api.Controllers.Character
{
    [Route("api/[controller]"), ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly IIdResultCommandHandler<CreateCharacterDraftCommand> createDraftCommandHandler;

        private readonly IQueryHandler<GetCharacterByIdQuery, CharacterDto> getByIdQueryHandler;

        private readonly IEmptyResultCommandHandler<RollAbilityScoresCommand> rollAbilitiesScoresHandler;

        public CharacterController(
            IIdResultCommandHandler<CreateCharacterDraftCommand> createDraftCommandHandler,
            IQueryHandler<GetCharacterByIdQuery, CharacterDto> getByIdQueryHandler,
            IEmptyResultCommandHandler<RollAbilityScoresCommand> rollAbilitiesScoresHandler)
        {
            this.createDraftCommandHandler = createDraftCommandHandler;
            this.getByIdQueryHandler = getByIdQueryHandler;
            this.rollAbilitiesScoresHandler = rollAbilitiesScoresHandler;
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

        [HttpGet]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(200, Type = typeof(Model.Character.Character))]
        public IActionResult GetById([FromQuery] Guid characterId)
        {
            if (characterId == Guid.Empty)
            {
                return BadRequest("Not enough information provided to query Character!");
            }

            var query = new GetCharacterByIdQuery(characterId);

            return getByIdQueryHandler.Handle(query) is CharacterDto character ?
                (IActionResult)Ok(character) :
                NotFound($"Character with provided Id: {characterId} was not found!");
        }

        [HttpPut]
        [ProducesResponseType(400, Type = typeof(string))]
        [ProducesResponseType(404, Type = typeof(string))]
        [ProducesResponseType(200, Type = typeof(string))]
        public IActionResult AddAbilities([FromBody] RollAbilityScoresRequest request)
        {
            if (request == null || request.DraftId == Guid.Empty)
                return BadRequest("Not enough information provided to roll ability scores");

            var command = new RollAbilityScoresCommand() 
            {
                CharacterUiD = request.DraftId, 
                Dexterity = request.Dexterity,
                Charisma = request.Charisma,
                Constitution = request.Constitution,
                Intelligence = request.Intelligence,
                Wisdom = request.Wisdom,
                Strength = request.Strength
            };

            try
            {
                rollAbilitiesScoresHandler.Handle(command);
            }

            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }

            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }

            return StatusCode(200);
        }
    }
}