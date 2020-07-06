using System;
using System.Collections.Generic;

using Dnd.Ddd.Common.ModelFramework;

namespace Dnd.Ddd.Model.Character.ValueObjects.Feature
{
    public abstract class Feature : ValueObject<Feature>
    {
        internal string FeatureName { get; set; }

        internal string FeatureDescription { get; set; }

        protected override bool InternalEquals(Feature valueObject) => valueObject.GetType() == GetType();

        protected override int InternalGetHashCode() => HashCode.Combine(GetType(), FeatureName, FeatureDescription);
    }
}
