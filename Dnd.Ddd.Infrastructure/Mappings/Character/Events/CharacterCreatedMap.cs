using Dnd.Ddd.Infrastructure.Database.Mappings.Events;
using Dnd.Ddd.Model.Character.DomainEvents;

using NHibernate.Mapping.ByCode;

namespace Dnd.Ddd.Infrastructure.Database.Mappings.Character.Events
{
    public class CharacterCreatedMap : BaseDomainEventMap<CharacterCreated>
    {
        public CharacterCreatedMap()
        {
            Table("CharacterCreatedEvents");
            Property(x => x.CharacterUiD, map => map.Access(Accessor.ReadOnly));
            Property(x => x.CreatorId, map => map.Access(Accessor.ReadOnly));
        }
    }
}