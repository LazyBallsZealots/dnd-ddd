using System.Threading;
using System.Threading.Tasks;

using Dnd.Ddd.Common.Infrastructure.Events;
using Dnd.Ddd.Common.ModelFramework;

using NHibernate.Event;

namespace Dnd.Ddd.Infrastructure.Middleware
{
    public class PostCommitEventListener : IPostDeleteEventListener, IPostUpdateEventListener, IPostInsertEventListener
    {
        private readonly IDomainEventDispatcher eventDispatcher;

        public PostCommitEventListener(IDomainEventDispatcher eventDispatcher)
        {
            this.eventDispatcher = eventDispatcher;
        }

        public async Task OnPostDeleteAsync(PostDeleteEvent @event, CancellationToken cancellationToken) =>
            await PostCommitAsync(@event.Entity, cancellationToken);

        public void OnPostDelete(PostDeleteEvent @event) => PostCommit(@event.Entity);

        public async Task OnPostInsertAsync(PostInsertEvent @event, CancellationToken cancellationToken) =>
            await PostCommitAsync(@event.Entity, cancellationToken);

        public void OnPostInsert(PostInsertEvent @event) => PostCommit(@event.Entity);

        public async Task OnPostUpdateAsync(PostUpdateEvent @event, CancellationToken cancellationToken) =>
            await PostCommitAsync(@event.Entity, cancellationToken);

        public void OnPostUpdate(PostUpdateEvent @event) => PostCommit(@event.Entity);

        private async Task PostCommitAsync(object entity, CancellationToken cancellationToken)
        {
            if (!(entity is Entity domainEntity))
            {
                return;
            }

            await eventDispatcher.DispatchAsync(domainEntity.DomainEvents, cancellationToken);
        }

        private void PostCommit(object entity)
        {
            if (!(entity is Entity domainEntity))
            {
                return;
            }

            eventDispatcher.Dispatch(domainEntity.DomainEvents);
        }
    }
}