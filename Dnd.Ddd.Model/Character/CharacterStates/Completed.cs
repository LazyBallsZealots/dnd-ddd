using Dnd.Ddd.Model.Character.CharacterStates.Contract;
using Dnd.Ddd.Model.Character.Exceptions;

namespace Dnd.Ddd.Model.Character.CharacterStates
{
    internal class Completed : CharacterState
    {
        internal override void SetCharisma(Character character, int charisma) => throw new InvalidCharacterStateException(character.UiD);

        internal override void SetConstitution(Character character, int constitution) => throw new InvalidCharacterStateException(character.UiD);

        internal override void SetDexterity(Character character, int dexterity) => throw new InvalidCharacterStateException(character.UiD);

        internal override void SetIntelligence(Character character, int intelligence) => throw new InvalidCharacterStateException(character.UiD);

        internal override void SetName(Character character, string name) => throw new InvalidCharacterStateException(character.UiD);

        internal override void SetRace(Character character, string race) => throw new InvalidCharacterStateException(character.UiD);

        internal override void SetStrength(Character character, int strength) => throw new InvalidCharacterStateException(character.UiD);

        internal override void SetWisdom(Character character, int wisdom) => throw new InvalidCharacterStateException(character.UiD);

        internal override bool CanChangeState(Character character) => false;
    }
}
