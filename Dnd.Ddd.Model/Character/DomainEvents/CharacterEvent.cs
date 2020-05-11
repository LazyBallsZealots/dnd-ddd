using System;

using Dnd.Ddd.Common.ModelFramework;

namespace Dnd.Ddd.Model.Character.DomainEvents
{
    public abstract class CharacterEvent : BaseDomainEvent
    {
        protected CharacterEvent()
        {
        }

        protected CharacterEvent(Guid characterUiD)
        {
            CharacterUiD = characterUiD;
        }

        public virtual Guid CharacterUiD { get; }
    }
}