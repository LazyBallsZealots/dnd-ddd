
using Dnd.Ddd.Common.Dto.Entities;

namespace Dnd.Ddd.Common.Dto.Character
{
    public class CharacterDto : BaseEntityDto
    {
        public virtual int StrengthValue { get; set; }

        public virtual int DexterityValue { get; set; }

        public virtual int ConstitutionValue { get; set; }

        public virtual int IntelligenceValue { get; set; }

        public virtual int WisdomValue { get; set; }

        public virtual int CharismaValue { get; set; }

        public virtual string CharacterName { get; set; }

        public virtual string RaceName { get; set; }
    }
}