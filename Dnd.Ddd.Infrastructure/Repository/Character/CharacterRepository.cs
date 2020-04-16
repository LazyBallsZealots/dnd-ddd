﻿using Dnd.Ddd.Model.Character.Repository;

using NHibernate;

namespace Dnd.Ddd.Infrastructure.Database.Repository.Character
{
    internal class CharacterRepository : NHibernateRepository<Model.Character.Character>, ICharacterRepository
    {
        public CharacterRepository(ISession session)
            : base(session)
        {
        }
    }
}