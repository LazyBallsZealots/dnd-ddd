using System;

using Dnd.Ddd.Common.Guard;

namespace Dnd.Ddd.Model.Character.ValueObjects.AbilityScores.Values
{
    internal sealed class Wisdom : AbilityScore<Wisdom>
    {
        protected Wisdom()
        {
        }

        private Wisdom(int wisdomLevel)
            : base(wisdomLevel)
        {
        }

        public static Wisdom FromInteger(int wisdomLevel)
        {
            Guard.With<ArgumentOutOfRangeException>().Against(wisdomLevel < 1, nameof(wisdomLevel));

            return new Wisdom(wisdomLevel);
        }

        internal override Wisdom Raise(int abilityScoreImprovement) => FromInteger(AbilityScoreLevel + abilityScoreImprovement);
    }
}