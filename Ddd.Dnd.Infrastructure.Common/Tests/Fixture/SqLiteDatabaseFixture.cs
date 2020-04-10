using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

using Ddd.Dnd.Infrastructure.Common.Extensions;
using Ddd.Dnd.Infrastructure.Common.Tests.Fixture.Interceptors;

using NHibernate;
using NHibernate.Bytecode;
using NHibernate.Cfg;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;

using Environment = NHibernate.Cfg.Environment;
using ScriptAdjustments = Ddd.Dnd.Infrastructure.Common.Tests.Fixture.SqlScriptAdjustments.SqLiteScriptAdjustments;

namespace Ddd.Dnd.Infrastructure.Common.Tests.Fixture
{
    /// <summary>
    ///     SQLite database fixture used for integration tests
    /// </summary>
    [ExcludeFromCodeCoverage]
    public abstract class SqLiteDatabaseFixture : IDisposable
    {
        public const string DefaultConnectionString = "FullUri=file:memorydb.db?mode=memory&cache=shared";

        internal static readonly ICollection<string> DisallowedExpressionsDuringSchemaDeploy = new List<string>
        {
            "drop", "PRAGMA", "create index", "ALTER"
        };

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

        private readonly string connectionString;

        private IDbConnection connection;

        private Configuration configuration;

        protected SqLiteDatabaseFixture(string connectionString, IEnumerable<Assembly> mappingAssemblies)
        {
            this.connectionString = connectionString;
            configuration = BuildNHibernateConfiguration(mappingAssemblies);

            connection = CreateAndOpenSqLiteConnection();

            GenerateDatabaseSchema();
        }

        protected SqLiteDatabaseFixture(string connectionString, params Assembly[] mappingAssemblies)
            : this(connectionString, mappingAssemblies.AsEnumerable())
        {
        }

        public ISessionFactory SessionFactory { get; private set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            new SchemaExport(configuration).Drop(false, true);

            if (!disposing)
                return;

            connection.Close();
            connection.Dispose();
            connection = null;
            SessionFactory.Dispose();
            SessionFactory = null;
            configuration = null;
        }

        private IDbConnection CreateAndOpenSqLiteConnection()
        {
            var dbConnection = new SQLiteConnection(connectionString);
            dbConnection.Open();
            return dbConnection;
        }

        private Configuration BuildNHibernateConfiguration(IEnumerable<Assembly> mappingAssemblies) =>
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

        private void GenerateDatabaseSchema()
        {
            var generatedSchemaScripts = GenerateSchemaCreationScripts();

            SessionFactory = configuration.BuildSessionFactory();

            using var schemaDeploySession = SessionFactory.WithOptions().Interceptor(new CreateTableInterceptor()).OpenSession();
            generatedSchemaScripts.ToList()
                .ForEach(schemaScript =>
                    schemaDeploySession.CreateSQLQuery(schemaScript)
                        .ExecuteUpdate());
        }

        private IEnumerable<string> GenerateSchemaCreationScripts()
        {
            var generateSchemaScripts = new List<string>();

            new SchemaExport(configuration).Execute(
                script =>
                {
                    if (script.TrimStart().StartsWith("create", StringComparison.CurrentCultureIgnoreCase) &&
                        !DisallowedExpressionsDuringSchemaDeploy.Any(script.TrimStart().StartsWith))
                    {
                        generateSchemaScripts.Add(script);
                        return;
                    }

                    if (!script.StartsWith("ALTER", StringComparison.CurrentCultureIgnoreCase))
                        return;

                    var tableName = ScriptAdjustments.GetTableNameFromAlterStatement(script);
                    var creationScript = generateSchemaScripts.First(statement => statement.Contains($"create table {tableName}"));
                    var newCreationScript = ScriptAdjustments.GetTableCreationScriptWithAlterStatement(creationScript, script);

                    generateSchemaScripts.Remove(creationScript);
                    generateSchemaScripts.Add(newCreationScript);
                },
                false,
                false,
                null);

            return generateSchemaScripts;
        }
    }
}