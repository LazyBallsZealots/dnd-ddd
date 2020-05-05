using System;
using System.Net.Http;

using Autofac.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Mvc.Testing;

namespace Dnd.Ddd.CharacterCreation.Api.Tests.Fixture
{
    public class IntegrationTestsFixture : IDisposable
    {
        private readonly WebApplicationFactory<Startup> webApplicationFactory;

        private readonly DatabaseManager databaseManager;

        public IntegrationTestsFixture()
        {
            webApplicationFactory = new TestWebApplicationFactory();
            var lifetimeScope = webApplicationFactory.Services.GetAutofacRoot();
            databaseManager = new DatabaseManager(lifetimeScope);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal HttpClient CreateClient() =>
            webApplicationFactory.CreateClient(
                new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false
                });

        protected virtual void Dispose(bool disposing)
        {
            webApplicationFactory.Dispose();
            databaseManager.Dispose();
        }
    }
}