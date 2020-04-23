using System;
using System.Collections.Generic;

using Dnd.Ddd.Common.ModelFramework;

namespace Dnd.Ddd.Common.Dto.Entities
{
    public abstract class BaseEntityDto
    {
        protected BaseEntityDto()
        {
            DomainEvents = new List<BaseDomainEvent>();
        }

        public virtual Guid UiD { get; set; }

        public virtual bool Valid { get; set; }

        public virtual IList<BaseDomainEvent> DomainEvents { get; protected set; }
    }
}