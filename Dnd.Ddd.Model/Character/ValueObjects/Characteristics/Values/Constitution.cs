using System;

using Dnd.Ddd.Common.Guard;

namespace Dnd.Ddd.Model.Character.ValueObjects.Characteristics.Values
{
    internal class Constitution : Characteristic
    {
        private Constitution(int characteristicLevel)
            : base(characteristicLevel)
        {
        }

        public static Constitution FromInteger(int constitutionLevel)
        {
            Guard.With<ArgumentOutOfRangeException>().Against(constitutionLevel < 1, nameof(constitutionLevel));

            return new Constitution(constitutionLevel);
        }
    }
}