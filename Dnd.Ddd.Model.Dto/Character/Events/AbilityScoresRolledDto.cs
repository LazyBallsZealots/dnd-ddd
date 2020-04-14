using System;

using Dnd.Ddd.Common.Dto.Events;

namespace Dnd.Ddd.Common.Dto.Character.Events
{
    public class AbilityScoresRolledDto : BaseDomainEventDto
    {
        protected AbilityScoresRolledDto()
        {
        }

        public virtual int Strength { get; set; }

        public virtual int Dexterity { get; set; }

        public virtual int Constitution { get; set; }

        public virtual int Intelligence { get; set; }

        public virtual int Wisdom { get; set; }

        public virtual int Charisma { get; set; }

        public virtual Guid CharacterUiD { get; set; }

        public AbilityScoresRolledDto(
            Guid characterUiD,
            int strength,
            int dexterity,
            int constitution,
            int intelligence,
            int wisdom,
            int charisma)
        {
            CharacterUiD = characterUiD;
            Strength = strength;
            Dexterity = dexterity;
            Constitution = constitution;
            Intelligence = intelligence;
            Wisdom = wisdom;
            Charisma = charisma;
        }
    }
}