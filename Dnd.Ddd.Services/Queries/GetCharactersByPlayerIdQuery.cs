using System;
using System.Collections.Generic;

using Dnd.Ddd.Common.Infrastructure.Queries;
using Dnd.Ddd.Model.Character;

namespace Dnd.Ddd.Services.Queries
{
    public class GetCharactersByPlayerIdQuery : BaseQuery<IList<Character>>
    {
        public GetCharactersByPlayerIdQuery(Guid playerId)
        {
            PlayerId = playerId;
        }

        public Guid PlayerId { get; }
    }
}
