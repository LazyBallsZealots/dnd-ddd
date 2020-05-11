using System;

using Dnd.Ddd.Common.Infrastructure.Commands;

namespace Dnd.Ddd.Services.Commands
{
    public class CreateCharacterDraftCommand : BaseCommand
    {
        public CreateCharacterDraftCommand(Guid playerId)
        {
            PlayerId = playerId;
        }

        public Guid PlayerId { get; }
    }
}