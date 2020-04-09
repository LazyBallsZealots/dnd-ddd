using Dnd.Ddd.Common.ModelFramework;

namespace Dnd.Ddd.Model.Character.ValueObjects.Characteristics
{
    internal abstract class Characteristic : ValueObject<Characteristic>
    {
        protected Characteristic(int characteristicLevel)
        {
            CharacteristicLevel = characteristicLevel;
            Modifier = CalculateModifier();
        }

        public int CharacteristicLevel { get; }

        public Modifier Modifier { get; }

        private Modifier CalculateModifier()
        {
            return Modifier.FromInteger(CharacteristicLevel);
        }

        protected override bool InternalEquals(Characteristic valueObject) => CharacteristicLevel == valueObject.CharacteristicLevel;

        protected override int InternalGetHashCode() => GetType().GetHashCode() ^ CharacteristicLevel.GetHashCode();
    }
}