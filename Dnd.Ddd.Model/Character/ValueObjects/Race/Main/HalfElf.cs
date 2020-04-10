using Dnd.Ddd.Model.Character.ValueObjects.Race.AbilityScoreBonuses;
using Dnd.Ddd.Model.Character.ValueObjects.Race.AbilityScoreBonuses.Type;
using Dnd.Ddd.Model.Character.ValueObjects.Race.Traits;
using Dnd.Ddd.Model.Character.ValueObjects.Race.Traits.Sizes;

namespace Dnd.Ddd.Model.Character.ValueObjects.Race.Main
{
    internal class HalfElf : Race
    {
        internal override Speed Speed => Speed.FromInteger(30);

        internal override Size Size => Medium.New();

        private HalfElf()
        {
        }

        internal override AbilityScoreBonusCollection AbilityScoreModifiers =>
            new AbilityScoreBonusCollection(
                new[]
                {
                    CharismaBonus.FromInteger(2)
                });

        public static HalfElf New() => new HalfElf();
    }
}