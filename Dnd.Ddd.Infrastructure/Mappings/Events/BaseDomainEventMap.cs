using Dnd.Ddd.Common.ModelFramework;

using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Dnd.Ddd.Infrastructure.Database.Mappings.Events
{
    public abstract class BaseDomainEventMap<TEvent> : ClassMapping<TEvent>
        where TEvent : BaseDomainEvent
    {
        protected BaseDomainEventMap()
        {
            Lazy(false);
            Id(x => x.Guid, mapper => mapper.Generator(Generators.Assigned));
            Property(x => x.OccuredOn, map => map.Access(Accessor.ReadOnly));
        }
    }
}