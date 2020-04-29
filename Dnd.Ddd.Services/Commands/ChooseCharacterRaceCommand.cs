using System;

using Dnd.Ddd.Common.Infrastructure.Commands;

namespace Dnd.Ddd.Services.Commands
{
    internal class ChooseCharacterRaceCommand : BaseCommand
    {
        public Guid CharacterUiD { get; set; }

        public string Race { get; set; }
    }
}