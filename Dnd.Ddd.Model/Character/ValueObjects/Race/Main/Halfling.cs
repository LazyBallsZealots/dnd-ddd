using Dnd.Ddd.Model.Character.ValueObjects.Race.AbilityScoreBonuses;
using Dnd.Ddd.Model.Character.ValueObjects.Race.AbilityScoreBonuses.Type;
using Dnd.Ddd.Model.Character.ValueObjects.Race.Traits;
using Dnd.Ddd.Model.Character.ValueObjects.Race.Traits.Sizes;

namespace Dnd.Ddd.Model.Character.ValueObjects.Race.Main
{
    internal class Halfling : Race
    {
        internal override Speed Speed => Speed.FromInteger(25);

        internal override Size Size => Small.New();

        private Halfling()
        {
        }

        internal override AbilityScoreBonusCollection AbilityScoreModifiers =>
            new AbilityScoreBonusCollection(
                new[]
                {
                    DexterityBonus.FromInteger(2)
                });

        public static Halfling New() => new Halfling();
    }
}