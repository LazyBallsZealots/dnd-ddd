using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

using Autofac;

using Dnd.Ddd.CharacterCreation.Api.Tests.Fixture.Interceptors;
using Dnd.Ddd.CharacterCreation.Api.Tests.Fixture.SqlScriptAdjustments;

using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Dnd.Ddd.CharacterCreation.Api.Tests.Fixture
{
    public class DatabaseManager : IDisposable
    {
        internal static readonly ICollection<string> DisallowedExpressionsDuringSchemaDeploy = new List<string>
        {
            "drop", "PRAGMA", "create index", "ALTER"
        };

        private const string DefaultConnectionString = "FullUri=file:memorydb.db?mode=memory&cache=shared";

        private IDbConnection connection;

        public DatabaseManager(ILifetimeScope lifetimeScope)
        {
            using var nestedLifetimeScope = lifetimeScope.BeginLifetimeScope();

            connection = CreateAndOpenSqLiteConnection();

            GenerateDatabaseSchema(nestedLifetimeScope);
        }

        public void Dispose()
        {
            connection.Dispose();
            connection = null;
        }

        public void ClearDatabase()
        {
            var command = connection.CreateCommand();
            command.CommandText = "select name from sqlite_master where type = 'table'";
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var tableName = reader.GetString(0);
                var dropCommand = connection.CreateCommand();
                dropCommand.CommandText = $"delete from {tableName}";
                dropCommand.ExecuteNonQuery();
            }
        }

        private static IDbConnection CreateAndOpenSqLiteConnection()
        {
            var dbConnection = new SQLiteConnection(DefaultConnectionString);
            dbConnection.Open();
            return dbConnection;
        }

        private static IEnumerable<string> GenerateSchemaCreationScripts(ILifetimeScope lifetimeScope)
        {
            var generateSchemaScripts = new List<string>();

            new SchemaExport(lifetimeScope.Resolve<Configuration>()).Execute(
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

        private static void GenerateDatabaseSchema(ILifetimeScope lifetimeScope)
        {
            var generatedSchemaScripts = GenerateSchemaCreationScripts(lifetimeScope);

            using var schemaDeploySession = lifetimeScope.Resolve<ISessionFactory>()
                .WithOptions()
                .Interceptor(new CreateTableInterceptor())
                .OpenSession();
            generatedSchemaScripts.ToList().ForEach(schemaScript => schemaDeploySession.CreateSQLQuery(schemaScript).ExecuteUpdate());
        }
    }
}