using Dnd.Ddd.Model.Character.ValueObjects.Race.AbilityScoreBonuses;
using Dnd.Ddd.Model.Character.ValueObjects.Race.AbilityScoreBonuses.Type;
using Dnd.Ddd.Model.Character.ValueObjects.Race.Traits;
using Dnd.Ddd.Model.Character.ValueObjects.Race.Traits.Sizes;

namespace Dnd.Ddd.Model.Character.ValueObjects.Race.Main
{
    internal class HalfOrc : Race
    {
        private HalfOrc()
        {
        }

        internal override Speed Speed => Speed.FromInteger(30);

        internal override Size Size => Medium.New();

        internal override AbilityScoreBonusCollection AbilityScoreModifiers =>
            new AbilityScoreBonusCollection(
                new AbilityScoreBonus[]
                {
                    StrengthBonus.FromInteger(2),
                    ConstitutionBonus.FromInteger(1)
                });

        public static HalfOrc New() => new HalfOrc();
    }
}