using Dnd.Ddd.Model.Character.ValueObjects.Characteristics.Values;

namespace Dnd.Ddd.Model.Character.ValueObjects.Race.CharacteristicBonus.Type
{
    internal sealed class StrengthBonus : CharacteristicBonus
    {
        private StrengthBonus(int characteristicModifierLevel)
            : base(characteristicModifierLevel)
        {
        }

        internal override string CharacteristicName => nameof(Strength);

        public static StrengthBonus FromInteger(int modifierLevel) => new StrengthBonus(modifierLevel);
    }
}