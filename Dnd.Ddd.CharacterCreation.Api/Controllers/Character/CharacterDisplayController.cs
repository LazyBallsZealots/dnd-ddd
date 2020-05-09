using System;

using Dnd.Ddd.Common.Infrastructure.Queries;
using Dnd.Ddd.Dtos;
using Dnd.Ddd.Services.Queries;

using Microsoft.AspNetCore.Mvc;

namespace Dnd.Ddd.CharacterCreation.Api.Controllers.Character
{
    [Route("api/character"), ApiController]
    public class CharacterDisplayController : ControllerBase
    {
        private readonly IQueryHandler<GetCharacterByIdQuery, CharacterDto> getByIdQueryHandler;

        public CharacterDisplayController(IQueryHandler<GetCharacterByIdQuery, CharacterDto> getByIdQueryHandler)
        {
            this.getByIdQueryHandler = getByIdQueryHandler;
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
    }
}
