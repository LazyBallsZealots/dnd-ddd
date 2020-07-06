using System;

namespace Dnd.Ddd.CharacterCreation.Api.Controllers.Character.RollAbilityScores
{
    public class RollAbilityScoresRequest
    {
        public Guid DraftId { get; set; }

        public int Strength { get; set; }

        public int Wisdom { get; set; }

        public int Dexterity { get; set; }

        public int Intelligence { get; set; }

        public int Charisma { get; set; }

        public int Constitution { get; set; }
    }
}