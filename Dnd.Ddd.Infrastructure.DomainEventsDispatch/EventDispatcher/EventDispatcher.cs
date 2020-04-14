using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Dnd.Ddd.Common.Infrastructure.Events;
using Dnd.Ddd.Common.ModelFramework;

using MediatR;

namespace Dnd.Ddd.Infrastructure.DomainEventsDispatch.EventDispatcher
{
    internal class EventDispatcher : IDomainEventDispatcher
    {
        private readonly IMediator mediator;

        public EventDispatcher(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public void Dispatch(IEnumerable<BaseDomainEvent> domainEvents)
        {
            foreach (var domainEvent in domainEvents)
            {
                mediator.Publish(domainEvent).Wait();
            }
        }

        public async Task DispatchAsync(IEnumerable<BaseDomainEvent> domainEvents, CancellationToken cancellationToken)
        {
            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent, cancellationToken);
            }
        }
    }
}