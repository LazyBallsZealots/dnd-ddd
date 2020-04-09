using Dnd.Ddd.Model.Character.ValueObjects.Race.CharacteristicBonus;
using Dnd.Ddd.Model.Character.ValueObjects.Race.CharacteristicBonus.Type;

namespace Dnd.Ddd.Model.Race.Main
{
    internal class Halfling : Race
    {
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