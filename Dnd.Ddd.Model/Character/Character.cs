using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

using Dnd.Ddd.Common.ModelFramework;
using Dnd.Ddd.Model.Character.ValueObjects;
using Dnd.Ddd.Model.Character.ValueObjects.AbilityScores.Values;
using Dnd.Ddd.Model.Character.ValueObjects.Race;
using Dnd.Ddd.Model.Character.ValueObjects.Race.AbilityScoreBonuses;
using Dtos;

[assembly: InternalsVisibleTo("Dnd.Ddd.Infrastructure.Database")]
[assembly: InternalsVisibleTo("Dnd.Ddd.Infrastructure.Tests")]
[assembly: InternalsVisibleTo("Dnd.Ddd.Model.Tests")]

namespace Dnd.Ddd.Model.Character
{
    public abstract class Character : Entity, IAggregateRoot
    {
        // TODO: refactor this to a different type
        private readonly IDictionary<string, Action<AbilityScoreBonus>> abilityScoreIncreases;

        protected Character()
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

        public CharacterDto ToDto()
        {
            var characterDto = new CharacterDto()
            {
                PlayerId = PlayerId.PlayerUiD,
                UiD = UiD,
                Charisma = Charisma?.ToInteger(),
                Constitution = Constitution?.ToInteger(),
                Dexterity = Dexterity?.ToInteger(),
                Intelligence = Intelligence?.ToInteger(),
                Wisdom = Wisdom?.ToInteger(),
                Strength = Strength?.ToInteger(),
                Race = Race?.ToString(),
                Name = Name?.ToString(),
                IsDeleted = IsDeleted,
                Version = Version,
                Stage = this is CharacterDraft ? typeof(CharacterDraft).Name : typeof(CompletedCharacter).Name
            };

            return characterDto;
        }

        internal PlayerId PlayerId { get; set; }

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