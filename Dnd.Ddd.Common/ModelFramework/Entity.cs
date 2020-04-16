using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Dnd.Ddd.Infrastructure.Database")]
[assembly: InternalsVisibleTo("Dnd.Ddd.Infrastructure.Tests")]

namespace Dnd.Ddd.Common.ModelFramework
{
    /// <summary>
    ///     Base class for all entities distinguished by surrogate identifier.
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        ///     Collection of domain events (which are published upon data access operations).
        /// </summary>
        private readonly ICollection<BaseDomainEvent> domainEvents;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Entity" /> class.
        /// </summary>
        protected Entity()
        {
            domainEvents = new List<BaseDomainEvent>();
            UiD = Guid.NewGuid();
            Version = 0;
        }

        /// <summary>
        ///     Collection of domain events to be emitted.
        /// </summary>
        public virtual IReadOnlyCollection<BaseDomainEvent> DomainEvents => domainEvents.ToList().AsReadOnly();

        /// <summary>
        ///     Unique surrogate entity identifier.
        /// </summary>
        public virtual Guid UiD { get; protected set; }

        protected internal virtual bool IsDeleted { get; set; }

        protected internal virtual long Version { get; protected set; }

        public static bool operator ==(Entity first, Entity second)
        {
            if (first is null && second is null)
            {
                return true;
            }

            if (first is null || second is null)
            {
                return false;
            }

            return ReferenceEquals(first, second) || first.Equals(second);
        }

        public static bool operator !=(Entity first, Entity second) => !(first == second);

        public virtual void RegisterDomainEvent(BaseDomainEvent @event) => domainEvents.Add(@event);

        public override bool Equals(object obj) => obj is Entity entity && obj.GetType() == GetType() && entity.UiD == UiD;

        public override int GetHashCode() => UiD.GetHashCode() ^ GetType().GetHashCode();
    }
}