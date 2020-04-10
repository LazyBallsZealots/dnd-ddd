using System;

using Dnd.Ddd.Common.ModelFramework;
using Dnd.Ddd.Common.Guard;

namespace Dnd.Ddd.Model.Character.ValueObjects.Race.Traits
{
    internal sealed class Speed : ValueObject<Speed>
    {
        private readonly int speedValue;

        private Speed(int speed)
        {
            speedValue = speed;
        }

        public static Speed FromInteger(int speed)
        {
            Guard.With<ArgumentOutOfRangeException>().Against(speed < 0, nameof(speed));
            return new Speed(speed);
        }
        public int ToInteger() => speedValue;
        protected override bool InternalEquals(Speed valueObject) => valueObject.speedValue == speedValue;
        protected override int InternalGetHashCode() => HashCode.Combine(GetType(), speedValue);
    }
}
