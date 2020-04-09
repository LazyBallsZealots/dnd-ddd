using System;

using Dnd.Ddd.Common.Guard;

namespace Dnd.Ddd.Model.Character.ValueObjects.Characteristics.Values
{
    internal sealed class Strength : Characteristic<Strength>
    {
        private Strength(int strengthLevel)
            : base(strengthLevel)
        {
        }

        public static Strength FromInteger(int strengthLevel)
        {
            Guard.With<ArgumentOutOfRangeException>().Against(strengthLevel < 1, nameof(strengthLevel));

            return new Strength(strengthLevel);
        }

        internal override Strength Raise(int abilityScoreImprovement) => FromInteger(CharacteristicLevel + abilityScoreImprovement);
    }
}