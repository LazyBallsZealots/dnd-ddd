using Dnd.Ddd.Model.Character.ValueObjects.Race.CharacteristicBonus;
using Dnd.Ddd.Model.Character.ValueObjects.Race.CharacteristicBonus.Type;

namespace Dnd.Ddd.Model.Race.Main
{
    internal class Dragonborn : Race
    {
        private Dragonborn()
        {
        }

        internal override CharacteristicBonusCollection CharacteristicModifiers =>
            new CharacteristicBonusCollection(
                new CharacteristicBonus[]
                {
                    StrengthBonus.FromInteger(2),
                    CharismaBonus.FromInteger(1)
                });

        public static Dragonborn New() => new Dragonborn();
    }
}