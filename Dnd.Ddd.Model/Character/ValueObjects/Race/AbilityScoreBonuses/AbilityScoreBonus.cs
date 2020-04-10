using System;

using Dnd.Ddd.Common.ModelFramework;

namespace Dnd.Ddd.Model.Character.ValueObjects.Race.AbilityScoreBonuses
{
    internal abstract class AbilityScoreBonus : ValueObject<AbilityScoreBonus>
    {
        protected AbilityScoreBonus(int abilityScoreModifierLevel)
        {
            AbilityScoreModifierLevel = abilityScoreModifierLevel;
        }

        internal abstract string AbilityScoreName { get; }

        internal int AbilityScoreModifierLevel { get; }

        protected override bool InternalEquals(AbilityScoreBonus valueObject) =>
            valueObject.GetType() == GetType() && valueObject.AbilityScoreModifierLevel == AbilityScoreModifierLevel;

        protected override int InternalGetHashCode() => HashCode.Combine(GetType(), AbilityScoreModifierLevel);
    }
}