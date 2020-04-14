using System;
using System.Threading.Tasks;

using Dnd.Ddd.Common.ModelFramework;

using NHibernate;

namespace Dnd.Ddd.Infrastructure.Repository
{
    internal abstract class NHibernateRepository<TAggregateType> : IRepository<TAggregateType, Guid>
        where TAggregateType : Entity, IAggregateRoot
    {
        protected NHibernateRepository(ISession session)
        {
            Session = session;
        }

        protected ISession Session { get; }

        public Guid Save(TAggregateType aggregate) => (Guid)Session.Save(aggregate);

        public async Task<Guid> SaveAsync(TAggregateType aggregate) => (Guid)await Session.SaveAsync(aggregate);

        public TAggregateType Get(Guid id) => Session.Get<TAggregateType>(id);

        public async Task<TAggregateType> GetAsync(Guid id) => await Session.GetAsync<TAggregateType>(id);

        public void Update(TAggregateType aggregate) => Session.Update(aggregate);

        public async Task UpdateAsync(TAggregateType aggregate) => await Session.UpdateAsync(aggregate);

        public void Delete(TAggregateType aggregate) => Session.Delete(aggregate);

        public async Task DeleteAsync(TAggregateType aggregate) => await Session.DeleteAsync(aggregate);
    }
}