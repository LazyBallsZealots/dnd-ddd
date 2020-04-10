using System;

using Dnd.Ddd.Common.ModelFramework;

namespace Dnd.Ddd.Model.Character.ValueObjects.AbilityScores
{
    internal class Modifier : ValueObject<Modifier>
    {
        private readonly int modifierValue;

        private Modifier(int abilityScoreLevel)
        {
            modifierValue = abilityScoreLevel / 2 - 5;
        }

        public static Modifier FromInteger(int abilityScoreLevel) => new Modifier(abilityScoreLevel);

        public int ToInteger() => modifierValue;

        protected override bool InternalEquals(Modifier valueObject) => valueObject.modifierValue == modifierValue;

        protected override int InternalGetHashCode() => HashCode.Combine(GetType(), modifierValue);
    }
}