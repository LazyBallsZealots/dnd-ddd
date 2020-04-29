using System;

using Dnd.Ddd.Common.Infrastructure.Commands;

namespace Dnd.Ddd.Services.Commands
{
    public class RollAbilityScoresCommand : BaseCommand
    {
        public Guid CharacterUiD { get; set; }

        public int Dexterity { get; set; }

        public int Charisma { get; set; }

        public int Wisdom { get; set; }

        public int Constitution { get; set; }

        public int Intelligence { get; set; }

        public int Strength { get; set; }
    }
}