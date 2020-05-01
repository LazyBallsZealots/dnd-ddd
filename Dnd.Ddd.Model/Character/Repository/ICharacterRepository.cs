using System;
using System.Collections.Generic;

using Dnd.Ddd.Common.ModelFramework;

namespace Dnd.Ddd.Model.Character.Repository
{
    public interface ICharacterRepository : IRepository<Character, Guid>
    {
        IEnumerable<BaseDomainEvent> GetDomainEventsForCharacter(Guid characterId);
    }
}