using System;

using Dnd.Ddd.Common.Guard;

namespace Dnd.Ddd.Model.Character.ValueObjects.Characteristics.Values
{
    internal class Charisma : Characteristic<Charisma>
    {
        private Charisma(int characteristicLevel)
            : base(characteristicLevel)
        {
        }

        public static Charisma FromInteger(int charismaLevel)
        {
            Guard.With<ArgumentOutOfRangeException>().Against(charismaLevel < 1, nameof(charismaLevel));

            return new Charisma(charismaLevel);
        }

        internal override Charisma Raise(int abilityScoreImprovement) => FromInteger(CharacteristicLevel + abilityScoreImprovement);
    }
}