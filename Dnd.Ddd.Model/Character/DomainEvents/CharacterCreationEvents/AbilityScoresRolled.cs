using System;

namespace Dnd.Ddd.Model.Character.DomainEvents.CharacterCreationEvents
{
    public class AbilityScoresRolled : CharacterEvent
    {
        public AbilityScoresRolled(
            Guid characterUiD,
            int strength,
            int dexterity,
            int constitution,
            int intelligence,
            int wisdom,
            int charisma)
            : base(characterUiD)
        {
            Strength = strength;
            Dexterity = dexterity;
            Constitution = constitution;
            Intelligence = intelligence;
            Wisdom = wisdom;
            Charisma = charisma;
        }

        protected AbilityScoresRolled()
        {
        }

        public int Strength { get; }

        public int Dexterity { get; }

        public int Constitution { get; }

        public int Intelligence { get; }

        public int Wisdom { get; }

        public int Charisma { get; }
    }
}