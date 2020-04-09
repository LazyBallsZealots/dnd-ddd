using System;

using Dnd.Ddd.Common.Guard;

namespace Dnd.Ddd.Model.Character.ValueObjects.Characteristics.Values
{
    internal sealed class Intelligence : Characteristic
    {
        private Intelligence(int intelligenceLevel)
            : base(intelligenceLevel)
        {
        }

        public static Intelligence FromInteger(int intelligenceLevel)
        {
            Guard.With<ArgumentOutOfRangeException>().Against(intelligenceLevel < 1, nameof(intelligenceLevel));

            return new Intelligence(intelligenceLevel);
        }
    }
}