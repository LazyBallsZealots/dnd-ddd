using System;
using System.Collections.Generic;

using Dnd.Ddd.Common.ModelFramework;

namespace Dnd.Ddd.Model.Character.ValueObjects.Feature
{
    public abstract class Feature : ValueObject<Feature>
    {
        internal abstract string FeatureName { get; }

        internal abstract string FeatureDescription { get; }

        protected override bool InternalEquals(Feature valueObject) => valueObject.GetType() == GetType();

        protected override int InternalGetHashCode() => HashCode.Combine(GetType(), FeatureName, FeatureDescription);
    }
}
