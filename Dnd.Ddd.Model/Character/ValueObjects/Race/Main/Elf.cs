using Dnd.Ddd.Model.Character.ValueObjects.Race.AbilityScoreBonuses;
using Dnd.Ddd.Model.Character.ValueObjects.Race.AbilityScoreBonuses.Type;
using Dnd.Ddd.Model.Character.ValueObjects.Race.Traits;
using Dnd.Ddd.Model.Character.ValueObjects.Race.Traits.Sizes;

namespace Dnd.Ddd.Model.Character.ValueObjects.Race.Main
{
    internal class Elf : Race
    {
        internal override Speed Speed => Speed.FromInteger(30);

        internal override Size Size => Medium.New();

        private Elf()
        {
        }

        public static Elf New() => new Elf();

        internal override AbilityScoreBonusCollection AbilityScoreModifiers => new AbilityScoreBonusCollection(
            new[]
            {
                DexterityBonus.FromInteger(2)
            });
    }
}