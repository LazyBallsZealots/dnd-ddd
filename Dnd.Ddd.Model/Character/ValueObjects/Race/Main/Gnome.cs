using Dnd.Ddd.Model.Character.ValueObjects.Race.CharacteristicBonus;
using Dnd.Ddd.Model.Character.ValueObjects.Race.CharacteristicBonus.Type;
using Dnd.Ddd.Model.Character.ValueObjects.Race.Traits;

namespace Dnd.Ddd.Model.Race.Main
{
    internal class Gnome : Race
    {
        internal override Speed Speed => Speed.FromInteger(25);

        private Gnome()
        {
        }

        internal override CharacteristicBonusCollection CharacteristicModifiers =>
            new CharacteristicBonusCollection(
                new[]
                {
                    IntelligenceBonus.FromInteger(2)
                });

        public static Gnome New() => new Gnome();
    }
}