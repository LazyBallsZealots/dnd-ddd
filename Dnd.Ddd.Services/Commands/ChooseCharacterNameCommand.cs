﻿using System;

using Dnd.Ddd.Common.Infrastructure.Commands;

namespace Dnd.Ddd.Services.Commands
{
    internal class ChooseCharacterNameCommand : BaseCommand
    {
        public Guid CharacterUiD { get; set; }

        public string Name { get; set; }
    }
}