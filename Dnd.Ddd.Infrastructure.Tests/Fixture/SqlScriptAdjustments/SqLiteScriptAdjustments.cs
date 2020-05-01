using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;

namespace Dnd.Ddd.CharacterCreation.Api.Tests.Fixture.SqlScriptAdjustments
{
    [ExcludeFromCodeCoverage]
    internal static class SqLiteScriptAdjustments
    {
        private const string PseudoGuidGenerationFunction =
            @"(hex(randomblob(4)) || '-' || hex( randomblob(2)) || '-' || '4' || substr(hex(randomblob(2)), 2) || '-' || substr('AB89', 1 + (abs(random()) % 4), 1) || substr(hex(randomblob(2)), 2) || '-' || hex(randomblob(6)))";

        public static string GetUidConstraint(string createStatement)
        {
            while (createStatement.Contains("UNIQUEIDENTIFIER"))
            {
                createStatement = createStatement.Replace("UNIQUEIDENTIFIER", "BLOB").Replace("NEWID()", PseudoGuidGenerationFunction);
            }

            return createStatement;
        }

        public static string AdjustNvarcharSqlType(string createStatement)
        {
            while (createStatement.ToLower(CultureInfo.CurrentCulture).Contains("nvarchar"))
            {
                var index = createStatement.IndexOf("nvarchar(", StringComparison.InvariantCultureIgnoreCase);
                var closingBracketIndex = createStatement.IndexOf(')', index);
                createStatement = createStatement.Remove(index, closingBracketIndex - index + 1).Insert(index, "TEXT");
            }

            return createStatement;
        }

        public static string AdjustVarcharSqlType(string createStatement)
        {
            while (createStatement.ToLower(CultureInfo.CurrentCulture).Contains("varchar"))
            {
                var index = createStatement.IndexOf("varchar(", StringComparison.InvariantCultureIgnoreCase);
                var closingBracketIndex = createStatement.IndexOf(')', index);
                createStatement = createStatement.Remove(index, closingBracketIndex - index + 1).Insert(index, "TEXT");
            }

            return createStatement;
        }

        public static string GetTableNameFromAlterStatement(string alterStatement) => alterStatement.Trim().Split(' ')[2];

        public static string GetTableCreationScriptWithAlterStatement(string creationScript, string alterStatement)
        {
            var trimmedAlterStatement = alterStatement.Trim();

            var columnName = GetColumnName(trimmedAlterStatement);
            var constraintType = GetConstraintType(trimmedAlterStatement);
            var defaultValue = GetDefaultValue(trimmedAlterStatement);

            var columnDefinition = GetColumnDefinition(creationScript, columnName);
            var newColumnDefinition = GetColumnDefinitionWithConstraint(columnDefinition, constraintType, defaultValue);

            return creationScript.Replace(columnDefinition, newColumnDefinition);
        }

        public static string AdjustVarbinaryType(string createStatement)
        {
            while (createStatement.ToLower(CultureInfo.CurrentCulture).Contains("varbinary"))
            {
                var index = createStatement.ToLower(CultureInfo.CurrentCulture)
                    .IndexOf("varbinary", StringComparison.InvariantCultureIgnoreCase);
                var closingBracketIndex = createStatement.IndexOf(')', index);
                createStatement = createStatement.Remove(index, closingBracketIndex - index + 1).Insert(index, "BLOB");
            }

            return createStatement;
        }

        public static string AdjustGetDateDefault(string creationScript)
        {
            while (creationScript.ToLower(CultureInfo.CurrentCulture).Contains("getdate()"))
            {
                var index = creationScript.ToLower(CultureInfo.CurrentCulture)
                    .IndexOf("getdate()", StringComparison.InvariantCultureIgnoreCase);

                creationScript = creationScript.Remove(index, "getdate()".Length).Insert(index, "(datetime('now', 'localtime'))");
            }

            return creationScript;
        }

        private static string GetColumnDefinition(string creationScript, string columnName)
        {
            var columnNameIndex = creationScript.IndexOf(columnName, StringComparison.Ordinal);
            var endColumnDefinitionIndex = creationScript.IndexOf(',', creationScript.IndexOf(columnName, StringComparison.Ordinal));

            var columnDefinition = creationScript.Substring(columnNameIndex, endColumnDefinitionIndex - columnNameIndex);
            return columnDefinition;
        }

        private static string GetDefaultValue(string trimmedScript) => trimmedScript.Split(' ')[7];

        private static string GetConstraintType(string trimmedScript) => trimmedScript.Split(' ')[6];

        private static string GetColumnName(string trimmedScript) => trimmedScript.Split(' ').Last().Trim(';');

        private static string GetColumnDefinitionWithConstraint(string columnDefinition, string constraintType, string defaultValue) =>
            columnDefinition.Replace("not null", $"{constraintType} {defaultValue}");
    }
}