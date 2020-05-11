using System;

using Dnd.Ddd.Common.ModelFramework;

namespace Dnd.Ddd.Model.Character.DomainEvents.CharacterCreationEvents
{
    public class CharacterDraftCreated : BaseDomainEvent
    {
        public CharacterDraftCreated(Guid creatorId)
        {
            CreatorId = creatorId;
        }

        protected CharacterDraftCreated()
        {
        }

        public Guid CreatorId { get; }
    }
}