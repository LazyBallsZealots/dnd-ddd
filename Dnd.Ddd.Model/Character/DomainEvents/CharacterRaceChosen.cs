using System;

using Dnd.Ddd.Common.ModelFramework;

namespace Dnd.Ddd.Model.Character.DomainEvents
{
    public class CharacterRaceChosen : BaseDomainEvent
    {
        public CharacterRaceChosen(string characterRace, Guid sagaUiD)
        {
            CharacterRace = characterRace;
            SagaUiD = sagaUiD;
        }

        protected CharacterRaceChosen()
        {
        }

        public string CharacterRace { get; }

        public Guid SagaUiD { get; }
    }
}