using System;

using Dnd.Ddd.Common.ModelFramework;

namespace Dnd.Ddd.Model.Character.DomainEvents
{
    public class CharacterCreated : BaseDomainEvent
    {
        protected CharacterCreated()
        {
        }

        public CharacterCreated(Guid characterUiD, Guid creatorId)
        {
            CharacterUiD = characterUiD;
            CreatorId = creatorId;
        }

        public Guid CharacterUiD { get; }

        public Guid CreatorId { get; }
    }
}