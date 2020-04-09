using Dnd.Ddd.Model.Character.ValueObjects.Race.CharacteristicBonus;
using Dnd.Ddd.Model.Character.ValueObjects.Race.CharacteristicBonus.Type;

namespace Dnd.Ddd.Model.Race.Main
{
    internal class HalfOrc : Race
    {
        private HalfOrc()
        {
        }

        public static HalfOrc New() => new HalfOrc();

        internal override CharacteristicBonusCollection CharacteristicModifiers =>
            new CharacteristicBonusCollection(
                new CharacteristicBonus[]
                {
                    StrengthBonus.FromInteger(2),
                    ConstitutionBonus.FromInteger(1)
                });
    }
}