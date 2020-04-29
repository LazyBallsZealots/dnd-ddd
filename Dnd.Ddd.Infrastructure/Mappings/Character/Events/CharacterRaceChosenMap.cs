using Dnd.Ddd.Infrastructure.Database.Mappings.Events;
using Dnd.Ddd.Model.Character.DomainEvents.CharacterCreationEvents;

using NHibernate.Mapping.ByCode;

namespace Dnd.Ddd.Infrastructure.Database.Mappings.Character.Events
{
    public class CharacterRaceChosenMap : BaseDomainEventMap<CharacterRaceChosen>
    {
        public CharacterRaceChosenMap()
        {
            Table("CharacterRaceChosenEvents");
            Property(x => x.CharacterRace, map => map.Access(Accessor.ReadOnly));
            Property(x => x.CharacterUiD, map => map.Access(Accessor.ReadOnly));
        }
    }
}