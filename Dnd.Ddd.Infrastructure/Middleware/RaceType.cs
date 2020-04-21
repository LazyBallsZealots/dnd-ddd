using System;
using System.Data;
using System.Data.Common;

using Dnd.Ddd.Model;
using Dnd.Ddd.Model.Character.ValueObjects.Race;

using NHibernate;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;

namespace Dnd.Ddd.Infrastructure.Database.Middleware
{
    internal class RaceType : IUserType
    {
        public SqlType[] SqlTypes => new[] { NHibernateUtil.String.SqlType };

        public Type ReturnedType => typeof(Race);

        public bool IsMutable => false;

        public new bool Equals(object x, object y) => ReferenceEquals(x, y) || !(x == null || y == null) || x.Equals(y);

        public int GetHashCode(object x) => x.GetHashCode();

        public object NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner)
        {
            var raceNameObject = NHibernateUtil.String.NullSafeGet(rs, names, session);

            if (raceNameObject is string raceName &&
                !string.IsNullOrWhiteSpace(raceName) &&
                Enum.TryParse(typeof(Races), raceName, false, out var raceObject) &&
                raceObject is Races race)
            {
                return Race.FromEnumeration(race);
            }

            return null;
        }

        public void NullSafeSet(DbCommand cmd, object value, int index, ISessionImplementor session)
        {
            var parameter = (IDataParameter)cmd.Parameters[index];
            if (value is Race race)
            {
                parameter.Value = race.RaceName;
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