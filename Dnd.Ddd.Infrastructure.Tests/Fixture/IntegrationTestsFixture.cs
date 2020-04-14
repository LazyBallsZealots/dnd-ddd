using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;

using Autofac;

using Dnd.Ddd.Common.Infrastructure.UnitOfWork;
using Dnd.Ddd.Infrastructure.Common.Extensions;
using Dnd.Ddd.Infrastructure.DomainEventsDispatch;
using Dnd.Ddd.Infrastructure.Middleware;
using Dnd.Ddd.Infrastructure.Tests.Fixture.Interceptors;
using Dnd.Ddd.Infrastructure.Tests.Fixture.SqlScriptAdjustments;
using Dnd.Ddd.Infrastructure.UnitOfWork;

using NHibernate;
using NHibernate.Bytecode;
using NHibernate.Cfg;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Event;
using NHibernate.Tool.hbm2ddl;

using Environment = NHibernate.Cfg.Environment;

namespace Dnd.Ddd.Infrastructure.Tests.Fixture
{
    public class IntegrationTestsFixture : IDisposable
    {
        private const string DefaultConnectionString = "FullUri=file:memorydb.db?mode=memory&cache=shared";

        internal static readonly ICollection<string> DisallowedExpressionsDuringSchemaDeploy = new List<string>
        {
            "drop", "PRAGMA", "create index", "ALTER"
        };

        private static readonly IList<Assembly> MappingAssemblies = new List<Assembly>
        {
            Assembly.Load("Dnd.Ddd.Infrastructure")
        };

        private IDbConnection connection;

        public IntegrationTestsFixture()
        {
            var containerBuilder = new ContainerBuilder();

            connection = TestInfrastructureAutofacModule.CreateAndOpenSqLiteConnection();

            containerBuilder.RegisterModule(new TestInfrastructureAutofacModule(DefaultConnectionString, MappingAssemblies));
            containerBuilder.RegisterModule(new DomainEventDispatchAutofacModule());

            Container = containerBuilder.Build();

            GenerateDatabaseSchema();
        }

        internal IContainer Container { get; }

        internal IUnitOfWork UnitOfWork => new NHibernateUnitOfWork(Session);

        internal ISession Session => Container.Resolve<ISessionFactory>().OpenSession();

        public void Dispose()
        {
            Container?.Dispose();
            connection.Dispose();
            connection = null;
        }
        
        private void GenerateDatabaseSchema()
        {
            var generatedSchemaScripts = GenerateSchemaCreationScripts();

            using var schemaDeploySession = Container.Resolve<ISessionFactory>().WithOptions().Interceptor(new CreateTableInterceptor()).OpenSession();
            generatedSchemaScripts.ToList().ForEach(schemaScript => schemaDeploySession.CreateSQLQuery(schemaScript).ExecuteUpdate());
        }

        private IEnumerable<string> GenerateSchemaCreationScripts()
        {
            var generateSchemaScripts = new List<string>();

            new SchemaExport(Container.Resolve<Configuration>()).Execute(
                script =>
                {
                    if (script.TrimStart().StartsWith("create", StringComparison.CurrentCultureIgnoreCase) &&
                        !DisallowedExpressionsDuringSchemaDeploy.Any(script.TrimStart().StartsWith))
                    {
                        generateSchemaScripts.Add(script);
                        return;
                    }

                    if (!script.StartsWith("ALTER", StringComparison.CurrentCultureIgnoreCase))
                    {
                        return;
                    }

                    var tableName = SqLiteScriptAdjustments.GetTableNameFromAlterStatement(script);
                    var creationScript = generateSchemaScripts.First(statement => statement.Contains($"create table {tableName}"));
                    var newCreationScript = SqLiteScriptAdjustments.GetTableCreationScriptWithAlterStatement(creationScript, script);

                    generateSchemaScripts.Remove(creationScript);
                    generateSchemaScripts.Add(newCreationScript);
                },
                false,
                false,
                null);

            return generateSchemaScripts;
        }

        private class TestInfrastructureAutofacModule : InfrastructureAutofacModule
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

            public TestInfrastructureAutofacModule(string connectionString, IEnumerable<Assembly> mappingAssemblies)
            {
                baseConfiguration = BuildBaseNHibernateConfiguration(connectionString, mappingAssemblies);
            }

            protected override ISessionFactory CreateSessionFactory(Configuration configuration) => configuration.BuildSessionFactory();

            protected override Configuration BuildConfiguration(PostCommitEventListener eventListener)
            {
                var config = baseConfiguration;
                config.EventListeners.PostCommitDeleteEventListeners = new IPostDeleteEventListener[] { eventListener };
                config.EventListeners.PostCommitInsertEventListeners = new IPostInsertEventListener[] { eventListener };
                config.EventListeners.PostCommitUpdateEventListeners = new IPostUpdateEventListener[] { eventListener };

                return config;
            }

            public static IDbConnection CreateAndOpenSqLiteConnection()
            {
                var dbConnection = new SQLiteConnection(DefaultConnectionString);
                dbConnection.Open();
                return dbConnection;
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
}