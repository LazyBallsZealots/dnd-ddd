using System;
using Dnd.Ddd.Common.Infrastructure.Queries;
using Dnd.Ddd.Model.Character;
using Dnd.Ddd.Dtos;

namespace Dnd.Ddd.Services.Queries
{
    public class GetCharacterByIdQuery : BaseQuery<CharacterDto>
    {
        public GetCharacterByIdQuery(Guid characterId)
        {
            CharacterId = characterId;
        }

        public Guid CharacterId { get; }
    }
}
