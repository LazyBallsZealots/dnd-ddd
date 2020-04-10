using Dnd.Ddd.Common.Dto.Character.Events;
using Dnd.Ddd.Infrastructure.Mappings.Events;

namespace Dnd.Ddd.Infrastructure.Mappings.Character.Events
{
    public class AbilityScoresRolledDtoMap : BaseDomainEventDtoMap<AbilityScoresRolledDto>
    {
        public AbilityScoresRolledDtoMap()
        {
            Table("AbilityScoresRolledEvents");
            Property(x => x.CharacterUiD);
            Property(x => x.Strength);
            Property(x => x.Dexterity);
            Property(x => x.Constitution);
            Property(x => x.Intelligence);
            Property(x => x.Wisdom);
            Property(x => x.Charisma);
        }
    }
}