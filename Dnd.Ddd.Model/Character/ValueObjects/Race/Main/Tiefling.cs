using Dnd.Ddd.Model.Character.ValueObjects.Race.CharacteristicBonus;
using Dnd.Ddd.Model.Character.ValueObjects.Race.CharacteristicBonus.Type;
using Dnd.Ddd.Model.Character.ValueObjects.Race.Traits;

namespace Dnd.Ddd.Model.Race.Main
{
    internal class Tiefling : Race
    {
        internal override Speed Speed => Speed.FromInteger(30);

        private Tiefling()
        {
        }

        public static Tiefling New() => new Tiefling();

        internal override CharacteristicBonusCollection CharacteristicModifiers => new CharacteristicBonusCollection(
            new CharacteristicBonus[]
            {
                CharismaBonus.FromInteger(2),
                IntelligenceBonus.FromInteger(1)
            });
    }
}