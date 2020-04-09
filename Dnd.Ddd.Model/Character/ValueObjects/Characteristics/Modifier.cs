using System;
using System.Collections.Generic;
using System.Text;
using Dnd.Ddd.Common.ModelFramework;

namespace Dnd.Ddd.Model.Character.ValueObjects.Characteristics
{
    class Modifier : ValueObject<Modifier>
    {
        private Modifier(int characteristicLevel)
        {
            ModifierValue = characteristicLevel / 2 - 5;
        }

        public static Modifier FromInteger(int characteristicLevel)
        {
            return new Modifier(characteristicLevel);
        }

        public int ModifierValue { get; }

        protected override bool InternalEquals(Modifier valueObject) =>
            valueObject.ModifierValue == ModifierValue;

        protected override int InternalGetHashCode() =>
            HashCode.Combine(GetType(), ModifierValue);
    }
}
