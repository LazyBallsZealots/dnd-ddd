using Dnd.Ddd.Model.Character.ValueObjects.Characteristics.Values;

namespace Dnd.Ddd.Model.Character.ValueObjects.Race.CharacteristicBonus.Type
{
    internal sealed class ConstitutionBonus : CharacteristicBonus
    {
        private ConstitutionBonus(int characteristicModifierLevel)
            : base(characteristicModifierLevel)
        {
        }

        internal override string CharacteristicName => nameof(Constitution);

        public static ConstitutionBonus FromInteger(int modifier) => new ConstitutionBonus(modifier);
    }
}