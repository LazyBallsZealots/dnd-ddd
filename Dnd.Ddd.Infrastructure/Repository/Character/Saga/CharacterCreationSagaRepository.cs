using System;
using System.Collections.Generic;

using Dnd.Ddd.Model.Character.Repository;
using Dnd.Ddd.Model.Character.Saga;

using NHibernate;

namespace Dnd.Ddd.Infrastructure.Repository.Character.Saga
{
    internal class CharacterCreationSagaRepository : NHibernateRepository<CharacterCreationSaga>, ICharacterCreationSagaRepository
    {
        public CharacterCreationSagaRepository(ISession session)
            : base(session)
        {
        }

        public IEnumerable<CharacterCreationSaga> GetCharacterCreationSagasByCreatorId(Guid creatorId) =>
            Session.QueryOver<CharacterCreationSaga>()
                .Where(saga => saga.CreatorId == creatorId)
                .And(saga => !saga.IsDeleted)
                .List();
    }
}