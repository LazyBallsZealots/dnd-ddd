using Dnd.Ddd.Model.Character.ValueObjects.Race.CharacteristicBonus;
using Dnd.Ddd.Model.Character.ValueObjects.Race.CharacteristicBonus.Type;
using Dnd.Ddd.Model.Character.ValueObjects.Race.Traits;

namespace Dnd.Ddd.Model.Race.Main
{
    internal class Elf : Race
    {
        internal override Speed Speed => Speed.FromInteger(30);
        private Elf()
        {
        }

        public static Elf New() => new Elf();

        internal override CharacteristicBonusCollection CharacteristicModifiers => new CharacteristicBonusCollection(
            new[]
            {
                DexterityBonus.FromInteger(2)
            });
    }
}