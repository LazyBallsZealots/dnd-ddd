using System;
using Dnd.Ddd.Common.Infrastructure.Queries;
using Dnd.Ddd.Model.Character;

namespace Dnd.Ddd.Services.Queries
{
    public class GetCharacterByIdQuery : BaseQuery<Character>
    {
        public GetCharacterByIdQuery(Guid characterId)
        {
            CharacterId = characterId;
        }

        public Guid CharacterId { get; }
    }
}
