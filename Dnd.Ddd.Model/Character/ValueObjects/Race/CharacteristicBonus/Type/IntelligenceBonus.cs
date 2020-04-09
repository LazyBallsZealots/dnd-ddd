using Dnd.Ddd.Model.Character.ValueObjects.Characteristics.Values;

namespace Dnd.Ddd.Model.Character.ValueObjects.Race.CharacteristicBonus.Type
{
    internal sealed class IntelligenceBonus : CharacteristicBonus
    {
        private IntelligenceBonus(int characteristicModifierLevel)
            : base(characteristicModifierLevel)
        {
        }

        internal override string CharacteristicName => nameof(Intelligence);

        public static IntelligenceBonus FromInteger(int modifier) => new IntelligenceBonus(modifier);
    }
}