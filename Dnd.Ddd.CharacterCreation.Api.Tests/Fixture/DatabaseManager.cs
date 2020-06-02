using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;

using Autofac;

using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Dnd.Ddd.CharacterCreation.Api.Tests.Fixture
{
    internal sealed class DatabaseManager : IDisposable
    {
        private const string LocalDbInstanceConnectionString = @"Server=(localdb)\DndDdd";

        private const string DefaultConnectionString = "Server=(localdb)\\DndDdd;Database=TestDndCharacterCreation;Integrated Security=SSPI;";

        private const int DbCreationTimeoutInMilliseconds = 15000;

        private readonly IDbConnection connection;

        public DatabaseManager(ILifetimeScope lifetimeScope)
        {
            using var nestedLifetimeScope = lifetimeScope.BeginLifetimeScope();

            CreateDatabaseIfNecessary();

            using var session = nestedLifetimeScope.Resolve<ISession>();

            connection = CreateAndOpenSqlConnection(session.Connection.ConnectionString);

            GenerateDatabaseSchema(nestedLifetimeScope);
        }

        public void Dispose() => connection.Dispose();

        public void ClearDatabase()
        {
            using var command = connection.CreateCommand();
            command.CommandText = "exec sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'; " +
                "exec sp_msforeachtable 'DELETE FROM ?';" +
                "exec sp_msforeachtable 'ALTER TABLE ? CHECK CONSTRAINT ALL'";
            _ = command.ExecuteNonQuery();
        }

        private static IDbConnection CreateAndOpenSqlConnection(string connectionString)
        {
            var dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();
            return dbConnection;
        }

        private void CreateDatabaseIfNecessary()
        {
            try
            {
                using var dbConnection = CreateAndOpenSqlConnection(DefaultConnectionString);
            }
            catch (SqlException)
            {
                using var localDbConnection = CreateAndOpenSqlConnection(LocalDbInstanceConnectionString);
                using var command = localDbConnection.CreateCommand();
                command.CommandText = "create database TestDndCharacterCreation;";
                _ = command.ExecuteNonQuery();

                WaitForDatabaseToBeCreated();
            }
        }

        private static void WaitForDatabaseToBeCreated()
        {
            for (var i = 0; i < DbCreationTimeoutInMilliseconds; i++)
            {
                try
                {
                    using var dbConnection = CreateAndOpenSqlConnection(DefaultConnectionString);
                    break;
                }
                catch (SqlException)
                {
                    Thread.Sleep(1);
                }
            }
        }

        private static IEnumerable<string> GenerateSchemaCreationScripts(ILifetimeScope lifetimeScope)
        {
            var generateSchemaScripts = new List<string>();

            new SchemaExport(lifetimeScope.Resolve<Configuration>()).Execute(
                script => generateSchemaScripts.Add(script),
                false,
                false,
                null);

            return generateSchemaScripts;
        }

        private static void GenerateDatabaseSchema(ILifetimeScope lifetimeScope)
        {
            var generatedSchemaScripts = GenerateSchemaCreationScripts(lifetimeScope);

            using var schemaDeploySession = lifetimeScope.Resolve<ISessionFactory>().OpenSession();

            generatedSchemaScripts.ToList()
                .ForEach(schemaScript => schemaDeploySession
                    .CreateSQLQuery(schemaScript)
                    .ExecuteUpdate());
        }
    }
}