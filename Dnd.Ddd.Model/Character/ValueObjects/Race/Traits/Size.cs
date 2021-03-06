﻿using Dnd.Ddd.Common.ModelFramework;

namespace Dnd.Ddd.Model.Character.ValueObjects.Race.Traits
{
    internal abstract class Size : ValueObject<Size>
    {
        public override string ToString() => GetType().Name;

        protected override bool InternalEquals(Size valueObject) => GetType() == valueObject.GetType();

        protected override int InternalGetHashCode() => GetType().GetHashCode();
    }
}