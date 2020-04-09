using Dnd.Ddd.Model.Character.ValueObjects.Characteristics.Values;

namespace Dnd.Ddd.Model.Character.ValueObjects.Race.CharacteristicBonus.Type
{
    internal sealed class CharismaBonus : CharacteristicBonus
    {
        private CharismaBonus(int characteristicModifierLevel)
            : base(characteristicModifierLevel)
        {
        }

        internal override string CharacteristicName => nameof(Charisma);

        public static CharismaBonus FromInteger(int modifier) => new CharismaBonus(modifier);
    }
}