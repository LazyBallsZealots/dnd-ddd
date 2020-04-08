using Dnd.Ddd.Common.ModelFramework;

namespace Dnd.Ddd.Model.Character.Builder
{
    public interface ICharacterBuilder : IBuilder<Character>
    {
        ICharacterBuilder SetStrength(int level);

        ICharacterBuilder SetDexterity(int level);

        ICharacterBuilder SetConstitution(int level);

        ICharacterBuilder SetWisdom(int level);

        ICharacterBuilder SetIntelligence(int level);

        ICharacterBuilder SetCharisma(int level);

        ICharacterBuilder Named(string name);
    }
}