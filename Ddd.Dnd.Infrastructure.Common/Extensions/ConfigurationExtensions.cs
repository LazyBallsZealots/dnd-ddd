using System;
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
            foreach (var assembly in assemblies.Where(AssemblyContainsMappingTypes))
            {
                var mapper = new ModelMapper();
                mapper.AddMappings(GetConformistMappingTypes(assembly));
                configuration.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());
            }

            return configuration;
        }
                
        private static bool AssemblyContainsMappingTypes(Assembly assembly) =>
            GetConformistMappingTypes(assembly).Any();

        private static IEnumerable<Type> GetConformistMappingTypes(Assembly assembly) =>
            assembly.GetExportedTypes()
                .Where(
                    type => type.GetBaseTypes()
                        .Any(
                            baseType => baseType != null &&
                                        baseType.IsGenericType &&
                                        baseType.GetGenericTypeDefinition() == typeof(IPropertyContainerMapper<>)))
                .ToList();
    }
}