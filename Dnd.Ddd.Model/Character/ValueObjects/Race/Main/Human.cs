using Dnd.Ddd.Model.Character.ValueObjects.Race.AbilityScoreBonuses;
using Dnd.Ddd.Model.Character.ValueObjects.Race.AbilityScoreBonuses.Type;
using Dnd.Ddd.Model.Character.ValueObjects.Race.Traits;
using Dnd.Ddd.Model.Character.ValueObjects.Race.Traits.Sizes;

namespace Dnd.Ddd.Model.Character.ValueObjects.Race.Main
{
    internal class Human : Race
    {
        internal override Speed Speed => Speed.FromInteger(30);

        internal override Size Size => Medium.New();

        private Human()
        {
        }

        internal override AbilityScoreBonusCollection AbilityScoreModifiers =>
            new AbilityScoreBonusCollection(
                new AbilityScoreBonus[]
                {
                    StrengthBonus.FromInteger(1),
                    WisdomBonus.FromInteger(1),
                    ConstitutionBonus.FromInteger(1),
                    DexterityBonus.FromInteger(1),
                    CharismaBonus.FromInteger(1),
                    IntelligenceBonus.FromInteger(1)
                });

        public static Human New() => new Human();
    }
}