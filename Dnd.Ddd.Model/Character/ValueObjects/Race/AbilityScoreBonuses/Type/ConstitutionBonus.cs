using Dnd.Ddd.Model.Character.ValueObjects.AbilityScores.Values;

namespace Dnd.Ddd.Model.Character.ValueObjects.Race.AbilityScoreBonuses.Type
{
    internal sealed class ConstitutionBonus : AbilityScoreBonus
    {
        private ConstitutionBonus(int abilityScoreModifierLevel)
            : base(abilityScoreModifierLevel)
        {
        }

        internal override string AbilityScoreName => nameof(Constitution);

        public static ConstitutionBonus FromInteger(int modifier) => new ConstitutionBonus(modifier);
    }
}