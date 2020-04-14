using Dnd.Ddd.Common.Dto.Events;

using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Dnd.Ddd.Infrastructure.Mappings.Events
{
    public abstract class BaseDomainEventDtoMap<TEventDto> : ClassMapping<TEventDto>
        where TEventDto : BaseDomainEventDto
    {
        protected BaseDomainEventDtoMap()
        {
            Id(x => x.Guid, mapper => mapper.Generator(Generators.Assigned));
            Property(x => x.OccuredOn);
        }
    }
}