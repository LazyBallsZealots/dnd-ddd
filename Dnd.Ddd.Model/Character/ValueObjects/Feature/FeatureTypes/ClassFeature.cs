using System;

using Dnd.Ddd.Common.Guard;

namespace Dnd.Ddd.Model.Character.ValueObjects.Feature.FeatureTypes
{
    internal class ClassFeature : Feature
    {
        private ClassFeature(string name, string description)
            : base(name, description)
        {
        }

        internal ClassFeature FromNameAndDescription(string name, string description)
        {
            Guard.With<ArgumentException>().Against(string.IsNullOrWhiteSpace(name), nameof(name));
            Guard.With<ArgumentException>().Against(string.IsNullOrWhiteSpace(description), nameof(description));

            return new ClassFeature(name, description);
        }
    }
}