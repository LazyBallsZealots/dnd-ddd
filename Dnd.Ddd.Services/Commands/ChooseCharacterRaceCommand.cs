﻿using System;

using Dnd.Ddd.Common.Infrastructure.Commands;

namespace Dnd.Ddd.Services.Commands
{
    public class ChooseCharacterRaceCommand : BaseCommand
    {
        public Guid CharacterUiD { get; set; }

        public string Race { get; set; }
    }
}