using System;

using Dnd.Ddd.Common.Guard;

namespace Dnd.Ddd.Model.Character.ValueObjects.AbilityScores.Values
{
    internal class Charisma : AbilityScore<Charisma>
    {
        protected Charisma()
        {
        }

        private Charisma(int abilityScoreLevel)
            : base(abilityScoreLevel)
        {
        }

        public static Charisma FromInteger(int charismaLevel)
        {
            Guard.With<ArgumentOutOfRangeException>().Against(charismaLevel < 1, nameof(charismaLevel));

            return new Charisma(charismaLevel);
        }

        internal override Charisma Raise(int abilityScoreImprovement) => FromInteger(AbilityScoreLevel + abilityScoreImprovement);
    }
}