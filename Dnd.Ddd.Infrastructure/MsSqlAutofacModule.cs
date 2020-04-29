using System.Collections.Generic;
using System.Reflection;

using Dnd.Ddd.Common.Infrastructure.Events;
using Dnd.Ddd.Common.ModelFramework;
using Dnd.Ddd.Infrastructure.Database.Common.Extensions;
using Dnd.Ddd.Infrastructure.Database.Middleware;

using NHibernate;
using NHibernate.Cfg;
using NHibernate.Event;

namespace Dnd.Ddd.Infrastructure.Database
{
    public class MsSqlAutofacModule : InfrastructureAutofacModule
    {
        private static readonly IList<Assembly> MappingAssemblies = new List<Assembly>
        {
            Assembly.Load("Dnd.Ddd.Infrastructure.Database")
        };

        protected override ISessionFactory CreateSessionFactory(Configuration configuration) => configuration.BuildSessionFactory();

        protected override Configuration BuildConfiguration(PostCommitEventListener eventListener)
        {
            var configuration = new Configuration().Configure(HibernateConfigFilePath).AddAssemblies(MappingAssemblies);

            configuration.EventListeners.PostCommitDeleteEventListeners = new IPostDeleteEventListener[] { eventListener };
            configuration.EventListeners.PostCommitInsertEventListeners = new IPostInsertEventListener[] { eventListener };
            configuration.EventListeners.PostCommitUpdateEventListeners = new IPostUpdateEventListener[] { eventListener };
            configuration.EventListeners.DeleteEventListeners = new IDeleteEventListener[] { new SoftDeleteEventListener() };

            return configuration;
        }

        protected override IDomainEventHandler<BaseDomainEvent> CreateEventStore(ISessionFactory sessionFactory) =>
            new EventStore.EventStore(sessionFactory);
    }
}