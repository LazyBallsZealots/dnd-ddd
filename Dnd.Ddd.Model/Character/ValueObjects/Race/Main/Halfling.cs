using Dnd.Ddd.Model.Character.ValueObjects.Race.CharacteristicBonus;
using Dnd.Ddd.Model.Character.ValueObjects.Race.CharacteristicBonus.Type;
using Dnd.Ddd.Model.Character.ValueObjects.Race.Traits;

namespace Dnd.Ddd.Model.Race.Main
{
    internal class Halfling : Race
    {
        internal override Speed Speed => Speed.FromInteger(25);

        private Halfling()
        {
        }

        internal override CharacteristicBonusCollection CharacteristicModifiers =>
            new CharacteristicBonusCollection(
                new[]
                {
                    DexterityBonus.FromInteger(2)
                });

        public static Halfling New() => new Halfling();
    }
}