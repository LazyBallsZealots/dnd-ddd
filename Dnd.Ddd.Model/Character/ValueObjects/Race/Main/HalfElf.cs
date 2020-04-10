using Dnd.Ddd.Model.Character.ValueObjects.Race.CharacteristicBonus;
using Dnd.Ddd.Model.Character.ValueObjects.Race.CharacteristicBonus.Type;
using Dnd.Ddd.Model.Character.ValueObjects.Race.Traits;

namespace Dnd.Ddd.Model.Race.Main
{
    internal class HalfElf : Race
    {
        internal override Speed Speed => Speed.FromInteger(30);

        private HalfElf()
        {
        }

        internal override CharacteristicBonusCollection CharacteristicModifiers =>
            new CharacteristicBonusCollection(
                new[]
                {
                    CharismaBonus.FromInteger(2)
                });

        public static HalfElf New() => new HalfElf();
    }
}