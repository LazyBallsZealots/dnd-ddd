using Dnd.Ddd.Common.Dto.Character;
using Dnd.Ddd.Infrastructure.Mappings.Entities;

namespace Dnd.Ddd.Infrastructure.Mappings.Character
{
    public class CharacterDtoMap : BaseEntityDtoMap<CharacterDto>
    {
        public CharacterDtoMap()
        {
            Table("Characters");
            Property(x => x.StrengthValue);
            Property(x => x.DexterityValue);
            Property(x => x.WisdomValue);
            Property(x => x.ConstitutionValue);
            Property(x => x.IntelligenceValue);
            Property(x => x.CharismaValue);
            Property(x => x.RaceName);
            Property(x => x.CharacterName);
        }
    }
}