using Dnd.Ddd.Model.Character.ValueObjects.Race.AbilityScoreBonuses;
using Dnd.Ddd.Model.Character.ValueObjects.Race.AbilityScoreBonuses.Type;
using Dnd.Ddd.Model.Character.ValueObjects.Race.Traits;
using Dnd.Ddd.Model.Character.ValueObjects.Race.Traits.Sizes;

namespace Dnd.Ddd.Model.Character.ValueObjects.Race.Main
{
    internal class Dragonborn : Race
    {
        internal override Speed Speed => Speed.FromInteger(30);

        internal override Size Size => Medium.New();

        private Dragonborn()
        {
        }

        internal override AbilityScoreBonusCollection AbilityScoreModifiers =>
            new AbilityScoreBonusCollection(
                new AbilityScoreBonus[]
                {
                    StrengthBonus.FromInteger(2),
                    CharismaBonus.FromInteger(1)
                });

        public static Dragonborn New() => new Dragonborn();
    }
}