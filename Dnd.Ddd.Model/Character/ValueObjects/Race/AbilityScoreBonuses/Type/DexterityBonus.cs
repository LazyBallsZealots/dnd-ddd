using Dnd.Ddd.Model.Character.ValueObjects.AbilityScores.Values;

namespace Dnd.Ddd.Model.Character.ValueObjects.Race.AbilityScoreBonuses.Type
{
    internal sealed class DexterityBonus : AbilityScoreBonus
    {
        private DexterityBonus(int abilityScoreModifierLevel)
            : base(abilityScoreModifierLevel)
        {
        }

        internal override string AbilityScoreName => nameof(Dexterity);

        public static DexterityBonus FromInteger(int modifier) => new DexterityBonus(modifier);
    }
}