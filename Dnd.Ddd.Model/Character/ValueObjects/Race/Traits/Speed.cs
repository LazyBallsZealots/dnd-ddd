using System;
using System.Collections.Generic;
using System.Text;
using Dnd.Ddd.Common.ModelFramework;

namespace Dnd.Ddd.Model.Character.ValueObjects.Race.Traits
{
    internal sealed class Speed : ValueObject<Speed>
    {
        private readonly int speedValue;

        private Speed(int speed)
        {
            speedValue = speed;
        }

        public static Speed FromInteger(int speed) => new Speed(speed);
        public int ToInteger() => speedValue;
        protected override bool InternalEquals(Speed valueObject) => valueObject.speedValue == speedValue;
        protected override int InternalGetHashCode() => HashCode.Combine(GetType(), speedValue);
    }
}
