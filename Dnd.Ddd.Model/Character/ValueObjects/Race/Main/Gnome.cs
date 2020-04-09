using Dnd.Ddd.Model.Character.ValueObjects.Race.CharacteristicBonus;
using Dnd.Ddd.Model.Character.ValueObjects.Race.CharacteristicBonus.Type;

namespace Dnd.Ddd.Model.Race.Main
{
    internal class Gnome : Race
    {
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