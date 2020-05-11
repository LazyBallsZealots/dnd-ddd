using System;
using System.Collections.Generic;

using Dnd.Ddd.Common.ModelFramework;

namespace Dnd.Ddd.Model.Character.Repository
{
    public interface ICharacterRepository : IRepository<Character, Guid>
    {
        IEnumerable<Character> GetByPlayerId(Guid playerId);

        IEnumerable<BaseDomainEvent> GetDomainEventsForCharacter(Guid characterId);
    }
}