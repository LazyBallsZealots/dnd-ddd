using System;
using System.Net.Http;
using Autofac;
using Autofac.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Mvc.Testing;
using NHibernate;

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
            var connectionString = lifetimeScope.Resolve<ISessionFactory>().OpenSession().Connection.ConnectionString;
            databaseManager = new DatabaseManager(lifetimeScope, connectionString);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void ClearDatabase() => databaseManager.ClearDatabase();

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