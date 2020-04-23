using System;

using Dnd.Ddd.Common.Guard;
using Dnd.Ddd.Common.ModelFramework;

namespace Dnd.Ddd.Model.Character.ValueObjects
{
    internal class Name : ValueObject<Name>
    {
        protected Name()
        {
        }

        private Name(string name)
        {
            CharacterName = name;
        }

        protected internal string CharacterName { get; private set; }

        public static Name FromString(string name)
        {
            Guard.With<ArgumentNullException>().Against(string.IsNullOrWhiteSpace(name), nameof(name));

            return new Name(name);
        }

        public override string ToString() => CharacterName;

        protected override bool InternalEquals(Name valueObject) => valueObject.CharacterName == CharacterName;

        protected override int InternalGetHashCode() => HashCode.Combine(GetType(), CharacterName);
    }
}