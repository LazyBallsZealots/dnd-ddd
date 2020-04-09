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

        public virtual int StrengthValue => Strength.ToInteger();

        public virtual int DexterityValue => Dexterity.ToInteger();

        public virtual int ConstitutionValue => Constitution.ToInteger();

        public virtual int CharismaValue => Charisma.ToInteger();

        public virtual int IntelligenceValue => Intelligence.ToInteger();

        public virtual int WisdomValue => Wisdom.ToInteger();

        public virtual int StrengthModifierValue => Strength.Modifier.ToInteger();

        public virtual int DexterityModifierValue => Dexterity.Modifier.ToInteger();

        public virtual int ConstitutionModifierValue => Constitution.Modifier.ToInteger();

        public virtual int CharismaModifierValue => Charisma.Modifier.ToInteger();

        public virtual int IntelligenceModifierValue => Intelligence.Modifier.ToInteger();

        public virtual int WisdomModifierValue => Wisdom.Modifier.ToInteger();

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