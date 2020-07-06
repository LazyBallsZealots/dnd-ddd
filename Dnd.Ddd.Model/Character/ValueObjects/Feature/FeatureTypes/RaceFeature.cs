using System;

using Dnd.Ddd.Common.Guard;

namespace Dnd.Ddd.Model.Character.ValueObjects.Feature.FeatureTypes
{
    internal class RaceFeature : Feature
    {
        private RaceFeature(string name, string description)
            : base(name, description)
        {
        }

        public static RaceFeature FromNameAndDescription(string name, string description)
        {
            Guard.With<ArgumentException>().Against(string.IsNullOrWhiteSpace(name), nameof(name));
            Guard.With<ArgumentException>().Against(string.IsNullOrWhiteSpace(description), nameof(description));

            return new RaceFeature(name, description);
        }
    }
}