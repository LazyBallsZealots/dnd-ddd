using Dnd.Ddd.Infrastructure.Database.Mappings.Events;
using Dnd.Ddd.Model.Character.DomainEvents;

using NHibernate.Mapping.ByCode;

namespace Dnd.Ddd.Infrastructure.Database.Mappings.Character.Events
{
    public class AbilityScoresRolledMap : BaseDomainEventMap<AbilityScoresRolled>
    {
        public AbilityScoresRolledMap()
        {
            Lazy(false);
            Table("AbilityScoresRolledEvents");
            Property(x => x.SagaUiD, map => map.Access(Accessor.ReadOnly));
            Property(x => x.Strength, map => map.Access(Accessor.ReadOnly));
            Property(x => x.Dexterity, map => map.Access(Accessor.ReadOnly));
            Property(x => x.Constitution, map => map.Access(Accessor.ReadOnly));
            Property(x => x.Intelligence, map => map.Access(Accessor.ReadOnly));
            Property(x => x.Wisdom, map => map.Access(Accessor.ReadOnly));
            Property(x => x.Charisma, map => map.Access(Accessor.ReadOnly));
        }
    }
}