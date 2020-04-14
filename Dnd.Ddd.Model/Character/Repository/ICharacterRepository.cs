using System;

using Dnd.Ddd.Common.ModelFramework;

namespace Dnd.Ddd.Model.Character.Repository
{
    public interface ICharacterRepository : IRepository<Character, Guid>
    {
    }
}