using System.Collections.Generic;
using System.Reflection;

using Ddd.Dnd.Infrastructure.Common.Tests.Fixture;

namespace Dnd.Ddd.Infrastructure.Tests.Fixture
{
    public class ModelDatabaseFixture : SqLiteDatabaseFixture
    {
        private static readonly IList<Assembly> MappingAssemblies = new List<Assembly>
        {
            Assembly.Load("Dnd.Ddd.Infrastructure")
        };

        public ModelDatabaseFixture()
            : base(DefaultConnectionString, MappingAssemblies)
        {
        }
    }
}