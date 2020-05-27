using System;
using System.Collections.Generic;

using Dnd.Ddd.Common.Guard;


namespace Dnd.Ddd.Model.Character.CharacterStates.Contract
{
    internal abstract class CharacterState
    {
        private static readonly IDictionary<CharacterStates, CharacterState> StatesInstances = new Dictionary<CharacterStates, CharacterState>
        {
            [CharacterStates.Completed] = new Completed(),
            [CharacterStates.Draft] = new Draft()
        };

        public static CharacterState FromEnumeration(CharacterStates state)
        {
            Guard.With<ArgumentOutOfRangeException>().Against(!StatesInstances.ContainsKey(state), nameof(state));
            return StatesInstances[state];
        }

        internal abstract void SetStrength(Character character, int strength);

        internal abstract void SetDexterity(Character character, int dexterity);

        internal abstract void SetWisdom(Character character, int wisdom);

        internal abstract void SetCharisma(Character character, int charisma);

        internal abstract void SetIntelligence(Character character, int intelligence);

        internal abstract void SetConstitution(Character character, int constitution);

        internal abstract void SetRace(Character character, string race);

        internal abstract void SetName(Character character, string name);
    }
}
