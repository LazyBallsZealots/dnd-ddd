using Dnd.Ddd.Model.Character.ValueObjects.Race.AbilityScoreBonuses;
using Dnd.Ddd.Model.Character.ValueObjects.Race.AbilityScoreBonuses.Type;
using Dnd.Ddd.Model.Character.ValueObjects.Race.Traits;
using Dnd.Ddd.Model.Character.ValueObjects.Race.Traits.Sizes;

namespace Dnd.Ddd.Model.Character.ValueObjects.Race.Main
{
    internal class Dwarf : Race
    {
        private Dwarf()
        {
        }

        internal override Speed Speed => Speed.FromInteger(25);

        internal override Size Size => Small.New();

        internal override AbilityScoreBonusCollection AbilityScoreModifiers =>
            new AbilityScoreBonusCollection(
                new[]
                {
                    ConstitutionBonus.FromInteger(2)
                });

        public static Dwarf New() => new Dwarf();
    }
}