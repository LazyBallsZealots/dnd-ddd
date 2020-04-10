using System;

using Dnd.Ddd.Common.Guard;

namespace Dnd.Ddd.Model.Character.ValueObjects.AbilityScores.Values
{
    internal class Constitution : AbilityScore<Constitution>
    {
        private Constitution(int abilityScoreLevel)
            : base(abilityScoreLevel)
        {
        }

        public static Constitution FromInteger(int constitutionLevel)
        {
            Guard.With<ArgumentOutOfRangeException>().Against(constitutionLevel < 1, nameof(constitutionLevel));

            return new Constitution(constitutionLevel);
        }

        internal override Constitution Raise(int abilityScoreImprovement) => FromInteger(AbilityScoreLevel + abilityScoreImprovement);
    }
}