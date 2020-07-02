using System;

using Dnd.Ddd.Common.Guard;
using Dnd.Ddd.Model.Character.CharacterStates.Contract;
using Dnd.Ddd.Model.Character.ValueObjects;
using Dnd.Ddd.Model.Character.ValueObjects.AbilityScores.Values;
using Dnd.Ddd.Model.Character.ValueObjects.Race;

namespace Dnd.Ddd.Model.Character.CharacterStates
{
    internal class Draft : CharacterState
    {
        internal override void SetCharisma(Character character, int charisma) => character.Charisma = Charisma.FromInteger(charisma);

        internal override void SetConstitution(Character character, int constitution) => character.Constitution = Constitution.FromInteger(constitution);

        internal override void SetDexterity(Character character, int dexterity) => character.Dexterity = Dexterity.FromInteger(dexterity);

        internal override void SetIntelligence(Character character, int intelligence) => character.Intelligence = Intelligence.FromInteger(intelligence);

        internal override void SetStrength(Character character, int strength) => character.Strength = Strength.FromInteger(strength);

        internal override void SetWisdom(Character character, int wisdom) => character.Wisdom = Wisdom.FromInteger(wisdom);

        internal override void SetName(Character character, string name) => character.Name = Name.FromString(name);

        internal override void SetRace(Character character, string race)
        {
            Guard.With<ArgumentOutOfRangeException>().Against(!Enum.TryParse(typeof(Races), race, out _), nameof(race));
            character.Race = Race.FromEnumeration(Enum.Parse<Races>(race));
        }

        internal override bool CanChangeState(Character character)
        {
            return character.Strength != null &&
                character.Dexterity != null &&
                character.Constitution != null &&
                character.Intelligence != null &&
                character.Wisdom != null &&
                character.Charisma != null &&
                character.Name != null &&
                character.Race != null;
        }
    }
}
