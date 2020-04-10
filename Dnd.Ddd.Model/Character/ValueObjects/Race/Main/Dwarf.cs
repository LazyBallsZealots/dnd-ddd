using Dnd.Ddd.Model.Character.ValueObjects.Race.CharacteristicBonus;
using Dnd.Ddd.Model.Character.ValueObjects.Race.CharacteristicBonus.Type;
using Dnd.Ddd.Model.Character.ValueObjects.Race.Traits;

namespace Dnd.Ddd.Model.Race.Main
{
    internal class Dwarf : Race
    {
        internal override Speed Speed => Speed.FromInteger(25);

        private Dwarf()
        {
        }

        internal override CharacteristicBonusCollection CharacteristicModifiers =>
            new CharacteristicBonusCollection(
                new[]
                {
                    ConstitutionBonus.FromInteger(2)
                });

        public static Dwarf New() => new Dwarf();
    }
}