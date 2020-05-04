using Autofac;

using Dnd.Ddd.Infrastructure.DomainEventsDispatch;
using Dnd.Ddd.Services;

using Microsoft.Extensions.Configuration;

namespace Dnd.Ddd.CharacterCreation.Api.Tests.Fixture
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public void ConfigureTestContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule(new TestInfrastructureAutofacModule(DatabaseManager.DefaultConnectionString, DatabaseManager.MappingAssemblies));
            containerBuilder.RegisterModule(new DomainEventDispatchAutofacModule());
            containerBuilder.RegisterModule(new DomainServicesAutofacModule());
        }
    }
}
