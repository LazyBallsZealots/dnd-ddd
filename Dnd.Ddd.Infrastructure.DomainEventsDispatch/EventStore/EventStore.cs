using System.Data;
using System.Threading;
using System.Threading.Tasks;

using Dnd.Ddd.Common.Infrastructure.Events;
using Dnd.Ddd.Common.ModelFramework;

using NHibernate;

namespace Dnd.Ddd.Infrastructure.EventBus.EventStore
{
    public class EventStore : IDomainEventHandler<BaseDomainEvent>
    {
        private readonly ISessionFactory sessionFactory;

        public EventStore(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public async Task Handle(BaseDomainEvent notification, CancellationToken cancellationToken)
        {
            if (sessionFactory.GetClassMetadata(notification.GetType()) == null)
            {
                return;
            }

            using var statelessSession = sessionFactory.OpenStatelessSession();
            using var transaction = statelessSession.BeginTransaction(IsolationLevel.ReadCommitted);

            await statelessSession.InsertAsync(notification, cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
    }
}