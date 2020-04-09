using Dnd.Ddd.Common.ModelFramework;

namespace Dnd.Ddd.Model.Character.ValueObjects.Characteristics
{
    internal abstract class Characteristic : ValueObject<Characteristic>
    {
        private readonly int characteristicLevel;

        protected Characteristic(int characteristicLevel)
        {
            this.characteristicLevel = characteristicLevel;
        }

        public int ToInteger() => characteristicLevel;

        public Modifier Modifier => Modifier.FromInteger(characteristicLevel);

        protected override bool InternalEquals(Characteristic valueObject) => characteristicLevel == valueObject.characteristicLevel;

        protected override int InternalGetHashCode() => GetType().GetHashCode() ^ characteristicLevel.GetHashCode();
    }
}