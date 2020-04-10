using Dnd.Ddd.Model.Character.ValueObjects.AbilityScores.Values;

namespace Dnd.Ddd.Model.Character.ValueObjects.Race.AbilityScoreBonuses.Type
{
    internal sealed class IntelligenceBonus : AbilityScoreBonus
    {
        private IntelligenceBonus(int abilityScoreModifierLevel)
            : base(abilityScoreModifierLevel)
        {
        }

        internal override string AbilityScoreName => nameof(Intelligence);

        public static IntelligenceBonus FromInteger(int modifier) => new IntelligenceBonus(modifier);
    }
}