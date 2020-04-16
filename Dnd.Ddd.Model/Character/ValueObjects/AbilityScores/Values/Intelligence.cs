using System;

using Dnd.Ddd.Common.Guard;

namespace Dnd.Ddd.Model.Character.ValueObjects.AbilityScores.Values
{
    internal sealed class Intelligence : AbilityScore<Intelligence>
    {
        protected Intelligence()
        {
        }

        private Intelligence(int intelligenceLevel)
            : base(intelligenceLevel)
        {
        }

        public static Intelligence FromInteger(int intelligenceLevel)
        {
            Guard.With<ArgumentOutOfRangeException>().Against(intelligenceLevel < 1, nameof(intelligenceLevel));

            return new Intelligence(intelligenceLevel);
        }

        internal override Intelligence Raise(int abilityScoreImprovement) => FromInteger(AbilityScoreLevel + abilityScoreImprovement);
    }
}