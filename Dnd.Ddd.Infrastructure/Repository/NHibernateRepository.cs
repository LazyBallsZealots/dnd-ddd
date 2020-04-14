using System;
using System.Threading.Tasks;

using Dnd.Ddd.Common.ModelFramework;

using NHibernate;

namespace Dnd.Ddd.Infrastructure.Repository
{
    internal class NHibernateRepository<TAggregateType> : IRepository<TAggregateType, Guid>
        where TAggregateType : Entity, IAggregateRoot
    {
        protected NHibernateRepository(ISession session)
        {
            Session = session;
        }

        protected ISession Session { get; }

        public Guid Save(TAggregateType aggregate) => (Guid)Session.Save(aggregate);

        public Task<Guid> SaveAsync(TAggregateType aggregate) => throw new NotImplementedException();

        public TAggregateType Get(Guid id) => throw new NotImplementedException();

        public Task<TAggregateType> GetAsync(Guid id) => throw new NotImplementedException();

        public void Update(TAggregateType aggregate) => throw new NotImplementedException();

        public Task UpdateAsync(TAggregateType aggregate) => throw new NotImplementedException();

        public void Delete(TAggregateType aggregate) => throw new NotImplementedException();

        public Task DeleteAsync(TAggregateType aggregate) => throw new NotImplementedException();
    }
}