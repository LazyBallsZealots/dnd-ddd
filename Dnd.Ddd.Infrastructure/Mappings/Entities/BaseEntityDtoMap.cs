using Dnd.Ddd.Common.Dto.Entities;

using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Dnd.Ddd.Infrastructure.Mappings.Entities
{
    public abstract class BaseEntityDtoMap<TEntityType> : ClassMapping<TEntityType>
        where TEntityType : BaseEntityDto
    {
        protected BaseEntityDtoMap()
        {
            Id(x => x.UiD, mapper => mapper.Generator(Generators.Assigned));
            Property(x => x.Valid);
        }
    }
}