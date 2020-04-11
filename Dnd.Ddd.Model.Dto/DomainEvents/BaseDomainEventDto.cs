using System;

namespace Dnd.Ddd.Common.Dto.DomainEvents
{
    public abstract class BaseDomainEventDto
    {
        /// <summary>
        ///     Unique identifier of a domain event.
        /// </summary>
        public virtual Guid Guid { get; set; }

        /// <summary>
        ///     Date of event occurence.
        /// </summary>
        public virtual DateTime OccuredOn { get; set; }
    }
}