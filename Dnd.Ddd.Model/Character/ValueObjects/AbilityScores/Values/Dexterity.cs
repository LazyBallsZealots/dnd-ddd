﻿using System;

using Dnd.Ddd.Common.Guard;

namespace Dnd.Ddd.Model.Character.ValueObjects.AbilityScores.Values
{
    internal class Dexterity : AbilityScore<Dexterity>
    {
        protected Dexterity()
        {
        }

        private Dexterity(int dexterityLevel)
            : base(dexterityLevel)
        {
        }

        public static Dexterity FromInteger(int dexterityLevel)
        {
            Guard.With<ArgumentOutOfRangeException>().Against(dexterityLevel < 1, nameof(dexterityLevel));

            return new Dexterity(dexterityLevel);
        }

        internal override Dexterity Raise(int abilityScoreImprovement) => FromInteger(AbilityScoreLevel + abilityScoreImprovement);
    }
}