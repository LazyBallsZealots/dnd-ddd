using System;

using Dnd.Ddd.Common.Guard;

namespace Dnd.Ddd.Model.Character.ValueObjects.AbilityScores.Values
{
    internal sealed class Strength : AbilityScore<Strength>
    {
        protected Strength()
        {
        }

        private Strength(int strengthLevel)
            : base(strengthLevel)
        {
        }

        public static Strength FromInteger(int strengthLevel)
        {
            Guard.With<ArgumentOutOfRangeException>().Against(strengthLevel < 1, nameof(strengthLevel));

            return new Strength(strengthLevel);
        }

        internal override Strength Raise(int abilityScoreImprovement) => FromInteger(AbilityScoreLevel + abilityScoreImprovement);
    }
}