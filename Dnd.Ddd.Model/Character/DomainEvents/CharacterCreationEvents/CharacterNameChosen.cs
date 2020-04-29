using System;

namespace Dnd.Ddd.Model.Character.DomainEvents.CharacterCreationEvents
{
    public class CharacterNameChosen : CharacterEvent
    {
        public CharacterNameChosen(string characterName, Guid characterUiD)
            : base(characterUiD)
        {
            CharacterName = characterName;
        }

        protected CharacterNameChosen()
        {
        }

        public string CharacterName { get; }
    }
}