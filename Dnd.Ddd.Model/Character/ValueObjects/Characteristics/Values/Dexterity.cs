using System;

using Dnd.Ddd.Common.Guard;

namespace Dnd.Ddd.Model.Character.ValueObjects.Characteristics.Values
{
    internal sealed class Dexterity : Characteristic
    {
        private Dexterity(int dexterityLevel)
            : base(dexterityLevel)
        {
        }

        public static Dexterity FromInteger(int dexterityLevel)
        {
            Guard.With<ArgumentOutOfRangeException>().Against(dexterityLevel < 1, nameof(dexterityLevel));

            return new Dexterity(dexterityLevel);
        }
    }
}