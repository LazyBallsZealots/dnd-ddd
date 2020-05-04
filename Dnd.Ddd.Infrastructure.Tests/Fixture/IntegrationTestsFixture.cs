using Autofac;
using Autofac.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;

namespace Dnd.Ddd.CharacterCreation.Api.Tests.Fixture
{
    public class IntegrationTestsFixture : WebApplicationFactory<Startup>
    {
        public ILifetimeScope Container => Services.GetAutofacRoot();

        protected override IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .UseEnvironment("Development") // TODO: class with constants
                .ConfigureWebHostDefaults(webHostBuilder => webHostBuilder.UseStartup<Startup>().UseTestServer());
    }
}