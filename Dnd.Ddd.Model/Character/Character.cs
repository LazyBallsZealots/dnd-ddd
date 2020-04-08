using Dnd.Ddd.Common.ModelFramework;
using Dnd.Ddd.Model.Character.ValueObjects;
using Dnd.Ddd.Model.Character.ValueObjects.Characteristics.Values;

namespace Dnd.Ddd.Model.Character
{
    public class Character : Entity, IAggregateRoot
    {
        internal Character()
        {
        }

        public virtual bool Valid { get; protected set; }

        public virtual int StrengthValue => Strength.CharacteristicLevel;

        public virtual int DexterityValue => Dexterity.CharacteristicLevel;

        public virtual int ConstitutionValue => Constitution.CharacteristicLevel;

        public virtual int CharismaValue => Charisma.CharacteristicLevel;

        public virtual int IntelligenceValue => Intelligence.CharacteristicLevel;

        public virtual int WisdomValue => Wisdom.CharacteristicLevel;

        public virtual string CharacterName => Name.ToString();

        internal Name Name { get; set; }

        internal Strength Strength { get; set; }

        internal Dexterity Dexterity { get; set; }

        internal Constitution Constitution { get; set; }

        internal Charisma Charisma { get; set; }

        internal Intelligence Intelligence { get; set; }

        internal Wisdom Wisdom { get; set; }
    }
}