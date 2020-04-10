using Dnd.Ddd.Model.Character.ValueObjects.AbilityScores.Values;

namespace Dnd.Ddd.Model.Character.ValueObjects.Race.AbilityScoreBonuses.Type
{
    internal sealed class CharismaBonus : AbilityScoreBonus
    {
        private CharismaBonus(int abilityScoreModifierLevel)
            : base(abilityScoreModifierLevel)
        {
        }

        internal override string AbilityScoreName => nameof(Charisma);

        public static CharismaBonus FromInteger(int modifier) => new CharismaBonus(modifier);
    }
}