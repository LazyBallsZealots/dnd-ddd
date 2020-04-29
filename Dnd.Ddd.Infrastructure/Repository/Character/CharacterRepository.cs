using System;
using System.Collections.Generic;

using Dnd.Ddd.Common.ModelFramework;
using Dnd.Ddd.Model.Character.DomainEvents;
using Dnd.Ddd.Model.Character.Repository;

using NHibernate;

namespace Dnd.Ddd.Infrastructure.Database.Repository.Character
{
    internal class CharacterRepository : NHibernateRepository<Model.Character.Character>, ICharacterRepository
    {
        public CharacterRepository(ISession session)
            : base(session)
        {
        }

        public IEnumerable<BaseDomainEvent> GetDomainEventsForCharacter(Guid characterId) =>
            Session.QueryOver<CharacterEvent>().Where(e => e.CharacterUiD == characterId).List();
    }
}