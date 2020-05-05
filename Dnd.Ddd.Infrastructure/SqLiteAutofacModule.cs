using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Dnd.Ddd.Common.Infrastructure.Events;
using Dnd.Ddd.Common.ModelFramework;
using Dnd.Ddd.Infrastructure.Database.Common.Extensions;
using Dnd.Ddd.Infrastructure.Database.Middleware;

using NHibernate;
using NHibernate.Bytecode;
using NHibernate.Cfg;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Event;

using Environment = NHibernate.Cfg.Environment;

namespace Dnd.Ddd.Infrastructure.Database
{
    public class SqLiteAutofacModule : InfrastructureAutofacModule
    {
        private static readonly Dictionary<string, string> ConfigurationOptions = new Dictionary<string, string>
        {
            [Environment.ProxyFactoryFactoryClass] = typeof(StaticProxyFactoryFactory).AssemblyQualifiedName,
            [Environment.UseQueryCache] = "true",
            ["expiration"] = "3600",
            [Environment.BatchSize] = "2000",
            [Environment.ReleaseConnections] = "after_transaction",
            [Environment.PropertyUseReflectionOptimizer] = "true",
            [Environment.PropertyBytecodeProvider] = "lcg",
            [Environment.ShowSql] = "true"
        };

        private readonly Configuration baseConfiguration;

        public SqLiteAutofacModule()
        {
            baseConfiguration = BuildBaseNHibernateConfiguration("FullUri=file:memorydb.db?mode=memory&cache=shared", MappingAssemblies);
        }

        protected override ISessionFactory CreateSessionFactory(Configuration configuration) => configuration.BuildSessionFactory();

        protected override Configuration BuildConfiguration(PostCommitEventListener eventListener)
        {
            var config = baseConfiguration;
            config.EventListeners.PostCommitDeleteEventListeners = new IPostDeleteEventListener[] { eventListener };
            config.EventListeners.PostCommitInsertEventListeners = new IPostInsertEventListener[] { eventListener };
            config.EventListeners.PostCommitUpdateEventListeners = new IPostUpdateEventListener[] { eventListener };
            config.EventListeners.DeleteEventListeners = new IDeleteEventListener[] { new SoftDeleteEventListener() };

            return config;
        }

        protected override IDomainEventHandler<BaseDomainEvent> CreateEventStore(ISessionFactory sessionFactory) =>
            new FakeEventStore(sessionFactory);

        private static Configuration BuildBaseNHibernateConfiguration(string connectionString, IEnumerable<Assembly> mappingAssemblies) =>
            new Configuration().SetProperties(ConfigurationOptions)
                .DataBaseIntegration(
                    db =>
                    {
                        db.ConnectionString = connectionString;
                        db.Driver<SQLite20Driver>();
                        db.Dialect<SQLiteDialect>();
                        db.ConnectionReleaseMode = ConnectionReleaseMode.AfterTransaction;
                        db.ConnectionProvider<DriverConnectionProvider>();
                        db.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                        db.LogSqlInConsole = true;
                        db.LogFormattedSql = true;
                    })
                .AddAssemblies(mappingAssemblies);

        private class FakeEventStore : IDomainEventHandler<BaseDomainEvent>, IDisposable
        {
            private readonly ISessionFactory sessionFactory;

            public FakeEventStore(ISessionFactory sessionFactory)
            {
                this.sessionFactory = sessionFactory;
            }

            public void Dispose() => sessionFactory?.Dispose();

            public async Task Handle(BaseDomainEvent notification, CancellationToken cancellationToken)
            {
                if (sessionFactory.GetClassMetadata(notification.GetType()) == null)
                {
                    return;
                }

                using var statelessSession = sessionFactory.OpenStatelessSession();

                await statelessSession.InsertAsync(notification, cancellationToken);
            }
        }
    }
}