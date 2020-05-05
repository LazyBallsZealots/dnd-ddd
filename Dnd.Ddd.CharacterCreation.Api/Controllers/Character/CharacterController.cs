using System;

using Dnd.Ddd.CharacterCreation.Api.Controllers.Character.CreateCharacterDraft;
using Dnd.Ddd.Common.Infrastructure.Commands;
using Dnd.Ddd.Services.Commands;

using Microsoft.AspNetCore.Mvc;

namespace Dnd.Ddd.CharacterCreation.Api.Controllers.Character
{
    [Route("api/[controller]"), ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly IIdResultCommandHandler<CreateCharacterDraftCommand> createDraftCommandHandler;

        public CharacterController(IIdResultCommandHandler<CreateCharacterDraftCommand> createDraftCommandHandler)
        {
            this.createDraftCommandHandler = createDraftCommandHandler;
        }

        [HttpPost, ProducesResponseType(201, Type = typeof(CreateCharacterDraftResponse)), ProducesResponseType(400, Type = typeof(string))]
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
    }
}