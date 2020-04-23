using System;
using System.Collections.Generic;

using Dnd.Ddd.Common.ModelFramework;
using Dnd.Ddd.Model.Character.Saga;

namespace Dnd.Ddd.Model.Character.Repository
{
    public interface ICharacterCreationSagaRepository : IRepository<CharacterCreationSaga, Guid>
    {
        IEnumerable<CharacterCreationSaga> GetCharacterCreationSagasByCreatorId(Guid creatorId);
    }
}