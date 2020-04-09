using Dnd.Ddd.Model.Character.ValueObjects.Characteristics.Values;

namespace Dnd.Ddd.Model.Character.ValueObjects.Race.CharacteristicBonus.Type
{
    internal sealed class WisdomBonus : CharacteristicBonus
    {
        private WisdomBonus(int characteristicModifierLevel)
            : base(characteristicModifierLevel)
        {
        }

        internal override string CharacteristicName => nameof(Wisdom);

        public static WisdomBonus FromInteger(int modifier) => new WisdomBonus(modifier);
    }
}