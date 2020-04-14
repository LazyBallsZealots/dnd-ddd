using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Dnd.Ddd.Common.ModelFramework;

namespace Dnd.Ddd.Common.Infrastructure.Events
{
    /// <summary>
    ///     Base contract for entities dispatching domain events.
    /// </summary>
    public interface IDomainEventDispatcher
    {
        /// <summary>
        ///     Dispatches provided <paramref name="domainEvents" /> to registered event handlers.
        /// </summary>
        /// <param name="domainEvents">Events to dispatch.</param>
        void Dispatch(IEnumerable<BaseDomainEvent> domainEvents);

        /// <summary>
        ///     Asynchronously dispatches provided <paramref name="domainEvents" /> to registered event handlers.
        /// </summary>
        /// <param name="domainEvents">Events to dispatch.</param>
        /// <param name="cancellationToken">Cancellation token used to cancel asynchronous operation.</param>
        /// <returns><see cref="Task" /> representing result of an asynchronous event dispatch operation.</returns>
        Task DispatchAsync(IEnumerable<BaseDomainEvent> domainEvents, CancellationToken cancellationToken);
    }
}