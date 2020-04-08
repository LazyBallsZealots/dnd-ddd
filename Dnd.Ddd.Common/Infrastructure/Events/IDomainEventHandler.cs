using Dnd.Ddd.Common.ModelFramework;

using MediatR;

namespace Dnd.Ddd.Common.Infrastructure.Events
{
    /// <summary>
    ///     Generic contract for all domain event handlers.
    /// </summary>
    /// <typeparam name="TDomainEvent">Type of domain event to handle.</typeparam>
    public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
        where TDomainEvent : BaseDomainEvent
    {
    }
}