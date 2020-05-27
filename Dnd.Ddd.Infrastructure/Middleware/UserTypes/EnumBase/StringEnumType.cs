using System;
using System.Data;
using System.Data.Common;

using NHibernate;
using NHibernate.Engine;
using NHibernate.SqlCommand;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;

namespace Dnd.Ddd.Infrastructure.Database.Middleware.UserTypes.EnumBase
{
    internal abstract class StringEnumType<TEnumType, TResultType> : IUserType
        where TEnumType : struct
        where TResultType : class
    {
        private readonly Func<TEnumType, TResultType> factoryMethod;

        private readonly Func<TResultType, string> valueSelector;

        protected StringEnumType(Func<TEnumType, TResultType> factoryMethod, Func<TResultType, string> valueSelector)
        {
            this.factoryMethod = factoryMethod;
            this.valueSelector = valueSelector;
        }

        public SqlType[] SqlTypes => new[] { NHibernateUtil.String.SqlType };

        public Type ReturnedType => typeof(TResultType);

        public abstract bool IsMutable { get; }

        public new bool Equals(object x, object y) => (x == null && y == null) ^ !(x == null || y == null) ^ x?.Equals(y) ?? false;

        public int GetHashCode(object x) => x.GetHashCode();

        public object NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner) =>
            NHibernateUtil.String.NullSafeGet(rs, names, session) is string enumName &&
            !string.IsNullOrWhiteSpace(enumName) &&
            Enum.TryParse(typeof(TEnumType), enumName, false, out var enumObject) &&
            enumObject is TEnumType enumMember ?
                (object)factoryMethod(enumMember) :
                null;

        public void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
        {
            var parameter = (IDataParameter)cmd.Parameters[index];
            if (value is TResultType resultType)
            {
                parameter.Value = valueSelector(resultType);
            }
            else
            {
                parameter.Value = DBNull.Value;
            }
        }

        public object DeepCopy(object value) => value;

        public object Replace(object original, object target, object owner) => original;

        public object Assemble(object cached, object owner) => DeepCopy(cached);

        public object Disassemble(object value) => DeepCopy(value);
    }
}
