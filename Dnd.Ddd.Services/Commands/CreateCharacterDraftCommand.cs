using System;

using Dnd.Ddd.Common.Infrastructure.Commands;

namespace Dnd.Ddd.Services.Commands
{
    public class CreateCharacterDraftCommand : BaseCommand
    {
        public Guid PlayerId { get; set; }
    }
}