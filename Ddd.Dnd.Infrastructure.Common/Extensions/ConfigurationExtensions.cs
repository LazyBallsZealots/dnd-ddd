using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;

namespace Dnd.Ddd.Infrastructure.Database.Common.Extensions
{
    public static class ConfigurationExtensions
    {
        public static Configuration AddAssemblies(this Configuration configuration, IEnumerable<Assembly> assemblies)
        {
            foreach (var assembly in assemblies)
            {
                var conformistMappingsByCode = assembly.GetExportedTypes()
                    .Where(
                        type => type.GetBaseTypes()
                            .Any(
                                baseType => baseType != null &&
                                            baseType.IsGenericType &&
                                            baseType.GetGenericTypeDefinition() == typeof(IPropertyContainerMapper<>)))
                    .ToList();

                if (!conformistMappingsByCode.Any())
                {
                    continue;
                }

                var mapper = new ModelMapper();
                mapper.AddMappings(conformistMappingsByCode);
                configuration.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());
            }

            return configuration;
        }
    }
}