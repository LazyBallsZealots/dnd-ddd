using Dnd.Ddd.Infrastructure.Database.Common.Extensions;
using Dnd.Ddd.Infrastructure.Database.Middleware;

using NHibernate;
using NHibernate.Cfg;
using NHibernate.Event;

namespace Dnd.Ddd.Infrastructure.Database
{
    public class MsSqlAutofacModule : InfrastructureAutofacModule
    {
        private readonly string connectionString;

        public MsSqlAutofacModule(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override ISessionFactory CreateSessionFactory(Configuration configuration) => configuration.BuildSessionFactory();

        protected override Configuration BuildConfiguration(PostCommitEventListener eventListener)
        {
            var configuration = new Configuration().Configure(HibernateConfigFilePath).SetProperty(Environment.ConnectionString, connectionString).AddAssemblies(MappingAssemblies);

            configuration.EventListeners.PostCommitDeleteEventListeners = new IPostDeleteEventListener[] { eventListener };
            configuration.EventListeners.PostCommitInsertEventListeners = new IPostInsertEventListener[] { eventListener };
            configuration.EventListeners.PostCommitUpdateEventListeners = new IPostUpdateEventListener[] { eventListener };
            configuration.EventListeners.DeleteEventListeners = new IDeleteEventListener[] { new SoftDeleteEventListener() };

            return configuration;
        }
    }
}