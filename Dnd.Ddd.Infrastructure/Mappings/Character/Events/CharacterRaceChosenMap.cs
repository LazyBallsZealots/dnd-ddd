using Dnd.Ddd.Infrastructure.Mappings.Events;
using Dnd.Ddd.Model.Character.DomainEvents;

using NHibernate.Mapping.ByCode;

namespace Dnd.Ddd.Infrastructure.Mappings.Character.Events
{
    public class CharacterRaceChosenMap : BaseDomainEventMap<CharacterRaceChosen>
    {
        public CharacterRaceChosenMap()
        {
            Table("CharacterRaceChosenEvents");
            Property(x => x.CharacterRace, map => map.Access(Accessor.ReadOnly));
            Property(x => x.SagaUiD, map => map.Access(Accessor.ReadOnly));
        }
    }
}