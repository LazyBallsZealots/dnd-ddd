using System;

using Dnd.Ddd.Common.ModelFramework;

namespace Dnd.Ddd.Model.Character.ValueObjects.AbilityScores
{
    internal abstract class AbilityScore<TAbilityScore> : ValueObject<AbilityScore<TAbilityScore>>
        where TAbilityScore : AbilityScore<TAbilityScore>
    {
        protected AbilityScore()
        {
        }

        protected AbilityScore(int abilityScoreLevel)
        {
            AbilityScoreLevel = abilityScoreLevel;
        }

        public Modifier Modifier => Modifier.FromInteger(AbilityScoreLevel);

        protected internal int AbilityScoreLevel { get; private set; }

        public int ToInteger() => AbilityScoreLevel;

        internal abstract TAbilityScore Raise(int abilityScoreImprovement);

        protected override bool InternalEquals(AbilityScore<TAbilityScore> valueObject) =>
            GetType() == valueObject.GetType() && AbilityScoreLevel == valueObject.AbilityScoreLevel;

        protected override int InternalGetHashCode() => HashCode.Combine(GetType(), AbilityScoreLevel);
    }
}