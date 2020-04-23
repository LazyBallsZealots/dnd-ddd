using System;

using Dnd.Ddd.Common.ModelFramework;

namespace Dnd.Ddd.Model.Character.DomainEvents
{
    public class AbilityScoresRolled : BaseDomainEvent
    {
        protected AbilityScoresRolled()
        {
        }

        public AbilityScoresRolled(
            Guid sagaUiD,
            int strength,
            int dexterity,
            int constitution,
            int intelligence,
            int wisdom,
            int charisma)
        {
            Strength = strength;
            Dexterity = dexterity;
            Constitution = constitution;
            Intelligence = intelligence;
            Wisdom = wisdom;
            Charisma = charisma;
            SagaUiD = sagaUiD;
        }

        public int Strength { get; }

        public int Dexterity { get; }

        public int Constitution { get; }

        public int Intelligence { get; }

        public int Wisdom { get; }

        public int Charisma { get; }

        public Guid SagaUiD { get; }
    }
}