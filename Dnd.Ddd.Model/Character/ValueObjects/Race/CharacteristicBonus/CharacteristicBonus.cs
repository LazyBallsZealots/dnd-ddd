using System;

using Dnd.Ddd.Common.ModelFramework;

namespace Dnd.Ddd.Model.Character.ValueObjects.Race.CharacteristicBonus
{
    internal abstract class CharacteristicBonus : ValueObject<CharacteristicBonus>
    {
        protected CharacteristicBonus(int characteristicModifierLevel)
        {
            CharacteristicModifierLevel = characteristicModifierLevel;
        }

        internal abstract string CharacteristicName { get; }

        internal int CharacteristicModifierLevel { get; }

        protected override bool InternalEquals(CharacteristicBonus valueObject) =>
            valueObject.GetType() == GetType() && valueObject.CharacteristicModifierLevel == CharacteristicModifierLevel;

        protected override int InternalGetHashCode() => HashCode.Combine(GetType(), CharacteristicModifierLevel);
    }
}