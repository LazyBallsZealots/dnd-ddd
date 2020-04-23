using System;

using Dnd.Ddd.Common.ModelFramework;

namespace Dnd.Ddd.Model.Character.DomainEvents
{
    public class CharacterNameChosen : BaseDomainEvent
    {
        public CharacterNameChosen(string characterName, Guid sagaUiD)
        {
            CharacterName = characterName;
            SagaUiD = sagaUiD;
        }

        protected CharacterNameChosen()
        {
        }

        public string CharacterName { get; }

        public Guid SagaUiD { get; }
    }
}