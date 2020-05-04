using System.Text.Json;

using Dnd.Ddd.CharacterCreation.Api.Controllers.Character.CreateCharacterDraft;
using Dnd.Ddd.Common.Infrastructure.Commands;
using Dnd.Ddd.Services.Commands;

using Microsoft.AspNetCore.Mvc;

namespace Dnd.Ddd.CharacterCreation.Api.Controllers.Character
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly IIdResultCommandHandler<CreateCharacterDraftCommand> createDraftCommandHandler;

        public CharacterController(IIdResultCommandHandler<CreateCharacterDraftCommand> createDraftCommandHandler)
        {
            this.createDraftCommandHandler = createDraftCommandHandler;
        }

        [HttpPost]
        [Produces("application/json")]
        public IActionResult CreateDraft([FromBody] CreateCharacterDraftRequest request)
        {
            var command = new CreateCharacterDraftCommand(request.PlayerId);
            var characterId = createDraftCommandHandler.Handle(command);

            var response = new CreateCharacterDraftResponse
            {
                DraftId = characterId
            };

            var serializedResponse = JsonSerializer.Serialize(response);

            return Ok(serializedResponse);
        }
    }
}