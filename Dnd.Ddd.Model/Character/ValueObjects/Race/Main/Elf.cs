using Dnd.Ddd.Model.Character.ValueObjects.Race.CharacteristicBonus;
using Dnd.Ddd.Model.Character.ValueObjects.Race.CharacteristicBonus.Type;

namespace Dnd.Ddd.Model.Race.Main
{
    internal class Elf : Race
    {
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