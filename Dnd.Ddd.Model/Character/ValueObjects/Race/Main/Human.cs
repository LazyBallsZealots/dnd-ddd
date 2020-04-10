using Dnd.Ddd.Model.Character.ValueObjects.Race.CharacteristicBonus;
using Dnd.Ddd.Model.Character.ValueObjects.Race.CharacteristicBonus.Type;
using Dnd.Ddd.Model.Character.ValueObjects.Race.Traits;

namespace Dnd.Ddd.Model.Race.Main
{
    internal class Human : Race
    {
        internal override Speed Speed => Speed.FromInteger(30);

        private Human()
        {
        }

        internal override CharacteristicBonusCollection CharacteristicModifiers =>
            new CharacteristicBonusCollection(
                new CharacteristicBonus[]
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