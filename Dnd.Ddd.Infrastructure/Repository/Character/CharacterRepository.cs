using System;
using System.Threading.Tasks;

using NHibernate;

namespace Dnd.Ddd.Infrastructure.Repository.Character
{
    internal class CharacterRepository : NHibernateRepository<Model.Character.Character>
    {
        public CharacterRepository(ISession session)
            : base(session)
        {
        }

        public void Save(Model.Character.Character aggregate) => throw new NotImplementedException();

        public Task SaveAsync(Model.Character.Character aggregate) => throw new NotImplementedException();

        public Model.Character.Character Get(Guid id) => throw new NotImplementedException();

        public Task<Model.Character.Character> GetAsync(Guid id) => throw new NotImplementedException();

        public void Update(Model.Character.Character aggregate) => throw new NotImplementedException();

        public Task UpdateAsync(Model.Character.Character aggregate) => throw new NotImplementedException();

        public void Delete(Model.Character.Character aggregate) => throw new NotImplementedException();

        public Task DeleteAsync(Model.Character.Character aggregate) => throw new NotImplementedException();
    }
}