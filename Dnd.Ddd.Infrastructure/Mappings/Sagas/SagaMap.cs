using Dnd.Ddd.Common.ModelFramework;

using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Dnd.Ddd.Infrastructure.Database.Mappings.Sagas
{
    public abstract class SagaMap<TSaga> : ClassMapping<TSaga>
        where TSaga : Saga
    {
        protected SagaMap()
        {
            Id(x => x.UiD, map => map.Generator(Generators.Assigned));
            Property(x => x.IsComplete, map => map.Access(Accessor.ReadOnly));
        }
    }
}