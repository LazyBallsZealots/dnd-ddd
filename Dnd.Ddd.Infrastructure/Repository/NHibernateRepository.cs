using System;
using System.Threading;
using System.Threading.Tasks;

using Dnd.Ddd.Common.Infrastructure.UnitOfWork;
using Dnd.Ddd.Common.ModelFramework;
using Dnd.Ddd.Infrastructure.Database.UnitOfWork;

using NHibernate;

namespace Dnd.Ddd.Infrastructure.Database.Repository
{
    internal abstract class NHibernateRepository<TAggregateType> : IRepository<TAggregateType, Guid>
        where TAggregateType : Entity, IAggregateRoot
    {
        protected NHibernateRepository(IUnitOfWork unitOfWork)
        {
            if (unitOfWork is NHibernateUnitOfWork nHibernateUnitOfWork)
            {
                Session = nHibernateUnitOfWork.Session;
            }
        }

        protected ISession Session { get; }

        public Guid Save(TAggregateType aggregate) => (Guid)Session.Save(aggregate);

        public async Task<Guid> SaveAsync(TAggregateType aggregate, CancellationToken token = default) =>
            (Guid)await Session.SaveAsync(aggregate, token);

        public TAggregateType Get(Guid id) => Session.Get<TAggregateType>(id);

        public async Task<TAggregateType> GetAsync(Guid id, CancellationToken token = default) =>
            await Session.GetAsync<TAggregateType>(id, token);

        public void Update(TAggregateType aggregate) => Session.Update(aggregate);

        public async Task UpdateAsync(TAggregateType aggregate, CancellationToken token = default) =>
            await Session.UpdateAsync(aggregate, token);

        public void Delete(TAggregateType aggregate) => Session.Delete(aggregate);

        public async Task DeleteAsync(TAggregateType aggregate, CancellationToken token = default) =>
            await Session.DeleteAsync(aggregate, token);
    }
}