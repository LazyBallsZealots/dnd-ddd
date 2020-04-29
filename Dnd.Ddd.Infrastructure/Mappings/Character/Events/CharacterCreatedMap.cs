using Dnd.Ddd.Infrastructure.Database.Mappings.Events;
using Dnd.Ddd.Model.Character.DomainEvents.CharacterCreationEvents;

using NHibernate.Mapping.ByCode;

namespace Dnd.Ddd.Infrastructure.Database.Mappings.Character.Events
{
    public class CharacterCreatedMap : BaseDomainEventMap<CharacterDraftCreated>
    {
        public CharacterCreatedMap()
        {
            Table("CharacterCreatedEvents");
            Property(x => x.CreatorId, map => map.Access(Accessor.ReadOnly));
        }
    }
}