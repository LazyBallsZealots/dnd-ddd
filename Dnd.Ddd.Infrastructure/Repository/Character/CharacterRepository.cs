using System;
using System.Collections.Generic;

using Dnd.Ddd.Common.Infrastructure.UnitOfWork;
using Dnd.Ddd.Common.ModelFramework;
using Dnd.Ddd.Model.Character.DomainEvents;
using Dnd.Ddd.Model.Character.Repository;
using Dnd.Ddd.Model.Character.ValueObjects;

namespace Dnd.Ddd.Infrastructure.Database.Repository.Character
{
    internal class CharacterRepository : NHibernateRepository<Model.Character.Character>, ICharacterRepository
    {
        public CharacterRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IEnumerable<Model.Character.Character> GetByPlayerId(Guid playerId) =>
            Session.QueryOver<Model.Character.Character>().Where(c => c.PlayerId == PlayerId.FromUiD(playerId)).Future();

        public IEnumerable<BaseDomainEvent> GetDomainEventsForCharacter(Guid characterId) =>
            Session.QueryOver<CharacterEvent>().Where(e => e.CharacterUiD == characterId).List();
    }
}