using System.Collections.Generic;
using System.Reflection;

using Dnd.Ddd.Infrastructure.Database.Common.Extensions;
using Dnd.Ddd.Infrastructure.Database.Middleware;

using NHibernate;
using NHibernate.Bytecode;
using NHibernate.Cfg;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Event;

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
    }
}