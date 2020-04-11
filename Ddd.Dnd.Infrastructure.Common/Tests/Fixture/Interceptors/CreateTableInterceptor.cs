using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using NHibernate;
using NHibernate.SqlCommand;

using ScriptAdjustments = Ddd.Dnd.Infrastructure.Common.Tests.Fixture.SqlScriptAdjustments.SqLiteScriptAdjustments;

namespace Ddd.Dnd.Infrastructure.Common.Tests.Fixture.Interceptors
{
    [ExcludeFromCodeCoverage]
    internal class CreateTableInterceptor : EmptyInterceptor
    {
        private static readonly IList<Func<string, string>> Adjustments = new List<Func<string, string>>
        {
            ScriptAdjustments.AdjustGetDateDefault,
            ScriptAdjustments.AdjustVarbinaryType,
            ScriptAdjustments.GetUidConstraint,
            ScriptAdjustments.AdjustNvarcharSqlType,
            ScriptAdjustments.AdjustVarcharSqlType,
        };

        public override SqlString OnPrepareStatement(SqlString sql)
        {
            var script = sql.ToString().Trim();

            return SqLiteDatabaseFixture.DisallowedExpressionsDuringSchemaDeploy.Any(script.Contains) ||
                   !script.StartsWith("create", StringComparison.InvariantCultureIgnoreCase) ?
                       sql :
                       new SqlString(Adjustments.Aggregate(script, (s, func) => func(s)));
        }
    }
}