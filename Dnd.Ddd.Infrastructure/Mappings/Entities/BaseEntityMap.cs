using Dnd.Ddd.Common.ModelFramework;

using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Dnd.Ddd.Infrastructure.Database.Mappings.Entities
{
    public abstract class BaseEntityMap<TEntityType> : ClassMapping<TEntityType>
        where TEntityType : Entity
    {
        protected BaseEntityMap()
        {
            Id(x => x.UiD, mapper => mapper.Generator(Generators.Assigned));
            Property(x => x.IsDeleted);
        }
    }
}