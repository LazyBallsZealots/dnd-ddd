using Dnd.Ddd.Model.Character.ValueObjects;
using Dnd.Ddd.Model.Character.ValueObjects.Characteristics.Values;

namespace Dnd.Ddd.Model.Character.Builder.Implementation
{
    public class CharacterBuilder : ICharacterBuilder
    {
        private readonly Character character;

        public CharacterBuilder()
        {
            character = new Character();
        }

        public Character Build() => character;

        public ICharacterBuilder SetStrength(int level)
        {
            character.Strength = Strength.FromInteger(level);
            return this;
        }

        public ICharacterBuilder SetDexterity(int level)
        {
            character.Dexterity = Dexterity.FromInteger(level);
            return this;
        }

        public ICharacterBuilder SetConstitution(int level)
        {
            character.Constitution = Constitution.FromInteger(level);
            return this;
        }

        public ICharacterBuilder SetWisdom(int level)
        {
            character.Wisdom = Wisdom.FromInteger(level);
            return this;
        }

        public ICharacterBuilder SetIntelligence(int level)
        {
            character.Intelligence = Intelligence.FromInteger(level);
            return this;
        }

        public ICharacterBuilder SetCharisma(int level)
        {
            character.Charisma = Charisma.FromInteger(level);
            return this;
        }

        public ICharacterBuilder Named(string name)
        {
            character.Name = Name.FromString(name);
            return this;
        }
    }
}