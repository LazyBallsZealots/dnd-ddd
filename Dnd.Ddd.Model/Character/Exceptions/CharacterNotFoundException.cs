using System;

namespace Dnd.Ddd.Model.Character.Exceptions
{
    public class CharacterNotFoundException : Exception
    {
        public CharacterNotFoundException(Guid characterUiD)
            : base($"Character with UiD: {characterUiD} was not found in the repository!")
        {
        }
    }
}