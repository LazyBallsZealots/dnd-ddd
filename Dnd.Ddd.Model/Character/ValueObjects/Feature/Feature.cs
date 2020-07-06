using System;

using Dnd.Ddd.Common.ModelFramework;

namespace Dnd.Ddd.Model.Character.ValueObjects.Feature
{
    internal abstract class Feature : ValueObject<Feature>
    {
        protected Feature(string name, string description)
        {
            FeatureName = name;
            FeatureDescription = description;
        }

        protected Feature()
        {
        }

        internal string FeatureName { get; private set; }

        internal string FeatureDescription { get; private set; }

        protected override bool InternalEquals(Feature valueObject) => valueObject.GetType() == GetType();

        protected override int InternalGetHashCode() => HashCode.Combine(GetType(), FeatureName, FeatureDescription);
    }
}
