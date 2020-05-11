using System;

namespace Dnd.Ddd.Model.Character.DomainEvents.CharacterCreationEvents
{
    public class CharacterRaceChosen : CharacterEvent
    {
        public CharacterRaceChosen(string characterRace, Guid characterUiD)
            : base(characterUiD)
        {
            CharacterRace = characterRace;
        }

        protected CharacterRaceChosen()
        {
        }

        public string CharacterRace { get; }
    }
}