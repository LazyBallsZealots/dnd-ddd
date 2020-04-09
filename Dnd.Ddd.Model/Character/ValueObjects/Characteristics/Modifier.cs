using System;

using Dnd.Ddd.Common.ModelFramework;

namespace Dnd.Ddd.Model.Character.ValueObjects.Characteristics
{
    internal class Modifier : ValueObject<Modifier>
    {
        private readonly int modifierValue;

        private Modifier(int characteristicLevel)
        {
            modifierValue = characteristicLevel / 2 - 5;
        }

        public static Modifier FromInteger(int characteristicLevel) => new Modifier(characteristicLevel);

        public int ToInteger() => modifierValue;

        protected override bool InternalEquals(Modifier valueObject) => valueObject.modifierValue == modifierValue;

        protected override int InternalGetHashCode() => HashCode.Combine(GetType(), modifierValue);
    }
}