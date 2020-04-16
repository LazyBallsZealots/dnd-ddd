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
            OptimisticLock(OptimisticLockMode.Version);
            DynamicUpdate(true);
            Id(x => x.UiD, mapper => mapper.Generator(Generators.Assigned));
            Version(
                x => x.Version,
                versionMapping =>
                {
                    versionMapping.Generated(VersionGeneration.Never);
                    versionMapping.UnsavedValue(0L);
                });
            Property(x => x.IsDeleted);
        }
    }
}