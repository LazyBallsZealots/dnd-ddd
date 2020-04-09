using Dnd.Ddd.Common.ModelFramework;

namespace Dnd.Ddd.Model.Character.ValueObjects.Characteristics
{
    internal abstract class Characteristic<TCharacteristic> : ValueObject<Characteristic<TCharacteristic>>
        where TCharacteristic : Characteristic<TCharacteristic>
    {
        protected Characteristic(int characteristicLevel)
        {
            CharacteristicLevel = characteristicLevel;
        }

        public Modifier Modifier => Modifier.FromInteger(CharacteristicLevel);

        protected int CharacteristicLevel { get; }

        public int ToInteger() => CharacteristicLevel;

        internal abstract TCharacteristic Raise(int abilityScoreImprovement);

        protected override bool InternalEquals(Characteristic<TCharacteristic> valueObject) => CharacteristicLevel == valueObject.CharacteristicLevel;

        protected override int InternalGetHashCode() => GetType().GetHashCode() ^ CharacteristicLevel.GetHashCode();
    }
}