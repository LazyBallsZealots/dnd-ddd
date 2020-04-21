using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Dnd.Ddd.Common.ModelFramework;
using Dnd.Ddd.Model.Character.ValueObjects;
using Dnd.Ddd.Model.Character.ValueObjects.AbilityScores.Values;
using Dnd.Ddd.Model.Character.ValueObjects.Race;
using Dnd.Ddd.Model.Character.ValueObjects.Race.AbilityScoreBonuses;

[assembly: InternalsVisibleTo("Dnd.Ddd.Infrastructure.Database")]
[assembly: InternalsVisibleTo("Dnd.Ddd.Infrastructure.Tests")]
[assembly: InternalsVisibleTo("Dnd.Ddd.Model.Tests")]

namespace Dnd.Ddd.Model.Character
{
    public class Character : Entity, IAggregateRoot
    {
        // TODO: refactor this to a different type
        private readonly IDictionary<string, Action<AbilityScoreBonus>> abilityScoreIncreases;

        private Guid creatorId;

        internal Character()
        {
            abilityScoreIncreases = new Dictionary<string, Action<AbilityScoreBonus>>
            {
                [nameof(Strength)] = bonus => Strength = Strength.Raise(bonus.AbilityScoreModifierLevel),
                [nameof(Dexterity)] = bonus => Dexterity = Dexterity.Raise(bonus.AbilityScoreModifierLevel),
                [nameof(Constitution)] = bonus => Constitution = Constitution.Raise(bonus.AbilityScoreModifierLevel),
                [nameof(Intelligence)] = bonus => Intelligence = Intelligence.Raise(bonus.AbilityScoreModifierLevel),
                [nameof(Wisdom)] = bonus => Wisdom = Wisdom.Raise(bonus.AbilityScoreModifierLevel),
                [nameof(Charisma)] = bonus => Charisma = Charisma.Raise(bonus.AbilityScoreModifierLevel)
            };
        }

        internal CreatorId Creator
        {
            get => CreatorId.FromUiD(creatorId);
            set => creatorId = value.ToUiD();
        }

        internal Name Name { get; set; }

        internal Strength Strength { get; set; }

        internal Dexterity Dexterity { get; set; }

        internal Constitution Constitution { get; set; }

        internal Charisma Charisma { get; set; }

        internal Intelligence Intelligence { get; set; }

        internal Wisdom Wisdom { get; set; }

        internal Race Race { get; set; }

        protected internal void IncreaseAbilityScoresBasedOnRace()
        {
            foreach (var raceAbilityScoreModifier in Race.AbilityScoreModifiers)
            {
                abilityScoreIncreases[raceAbilityScoreModifier.AbilityScoreName](raceAbilityScoreModifier);
            }
        }
    }
}