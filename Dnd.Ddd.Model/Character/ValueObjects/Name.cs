using System;

using Dnd.Ddd.Common.Guard;
using Dnd.Ddd.Common.ModelFramework;

namespace Dnd.Ddd.Model.Character.ValueObjects
{
    internal class Name : ValueObject<Name>
    {
        private readonly string name;

        private Name(string name)
        {
            this.name = name;
        }

        public static Name FromString(string name)
        {
            Guard.With<ArgumentNullException>().Against(string.IsNullOrWhiteSpace(name), nameof(name));

            return new Name(name);
        }

        public override string ToString() => name;

        protected override bool InternalEquals(Name valueObject) => valueObject.name == name;

        protected override int InternalGetHashCode() => HashCode.Combine(GetType(), name);
    }
}