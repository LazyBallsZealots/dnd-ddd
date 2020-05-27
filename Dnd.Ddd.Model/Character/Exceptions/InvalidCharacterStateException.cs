using System;
using System.Collections.Generic;
using System.Text;

namespace Dnd.Ddd.Model.Character.Exceptions
{
    public class InvalidCharacterStateException : Exception
    {
        public InvalidCharacterStateException(Guid id) 
            : base($"Character with id {id} is in incorrect state")
        {
        }
    }
}
