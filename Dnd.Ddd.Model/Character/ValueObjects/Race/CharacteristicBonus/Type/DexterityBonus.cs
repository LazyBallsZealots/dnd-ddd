using Dnd.Ddd.Model.Character.ValueObjects.Characteristics.Values;

namespace Dnd.Ddd.Model.Character.ValueObjects.Race.CharacteristicBonus.Type
{
    internal sealed class DexterityBonus : CharacteristicBonus
    {
        private DexterityBonus(int characteristicModifierLevel)
            : base(characteristicModifierLevel)
        {
        }

        internal override string CharacteristicName => nameof(Dexterity);

        public static DexterityBonus FromInteger(int modifier) => new DexterityBonus(modifier);
    }
}