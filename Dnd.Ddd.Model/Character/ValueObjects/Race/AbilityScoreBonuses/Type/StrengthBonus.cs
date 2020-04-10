using Dnd.Ddd.Model.Character.ValueObjects.AbilityScores.Values;

namespace Dnd.Ddd.Model.Character.ValueObjects.Race.AbilityScoreBonuses.Type
{
    internal sealed class StrengthBonus : AbilityScoreBonus
    {
        private StrengthBonus(int abilityScoreModifierLevel)
            : base(abilityScoreModifierLevel)
        {
        }

        internal override string AbilityScoreName => nameof(Strength);

        public static StrengthBonus FromInteger(int modifierLevel) => new StrengthBonus(modifierLevel);
    }
}