using Dnd.Ddd.Model.Character.ValueObjects.AbilityScores.Values;

namespace Dnd.Ddd.Model.Character.ValueObjects.Race.AbilityScoreBonuses.Type
{
    internal sealed class WisdomBonus : AbilityScoreBonus
    {
        private WisdomBonus(int abilityScoreModifierLevel)
            : base(abilityScoreModifierLevel)
        {
        }

        internal override string AbilityScoreName => nameof(Wisdom);

        public static WisdomBonus FromInteger(int modifier) => new WisdomBonus(modifier);
    }
}