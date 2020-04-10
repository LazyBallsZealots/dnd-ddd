using System;
using System.Collections.Generic;

using Dnd.Ddd.Common.ModelFramework;
using Dnd.Ddd.Model.Character.ValueObjects;
using Dnd.Ddd.Model.Character.ValueObjects.AbilityScores.Values;
using Dnd.Ddd.Model.Character.ValueObjects.Race;
using Dnd.Ddd.Model.Character.ValueObjects.Race.AbilityScoreBonuses;
using Dnd.Ddd.Model.Character.ValueObjects.Race.Traits;

namespace Dnd.Ddd.Model.Character
{
    public class Character : Entity, IAggregateRoot
    {
        // TODO: refactor this to a different type
        private readonly IDictionary<string, Action<AbilityScoreBonus>> abilityScoreIncreases;

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

        public virtual int StrengthValue => Strength.ToInteger();

        public virtual int DexterityValue => Dexterity.ToInteger();

        public virtual int ConstitutionValue => Constitution.ToInteger();

        public virtual int CharismaValue => Charisma.ToInteger();

        public virtual int IntelligenceValue => Intelligence.ToInteger();

        public virtual int WisdomValue => Wisdom.ToInteger();

        public virtual int StrengthModifierValue => Strength.Modifier.ToInteger();

        public virtual int DexterityModifierValue => Dexterity.Modifier.ToInteger();

        public virtual int ConstitutionModifierValue => Constitution.Modifier.ToInteger();

        public virtual int CharismaModifierValue => Charisma.Modifier.ToInteger();

        public virtual int IntelligenceModifierValue => Intelligence.Modifier.ToInteger();

        public virtual int WisdomModifierValue => Wisdom.Modifier.ToInteger();

        public virtual string CharacterName => Name.ToString();

        public virtual int SpeedValue => Race.Speed.ToInteger();

        public virtual string SizeName => Race.Size.SizeName;

        internal Name Name { get; set; }

        internal Strength Strength { get; set; }

        internal Dexterity Dexterity { get; set; }

        internal Constitution Constitution { get; set; }

        internal Charisma Charisma { get; set; }

        internal Intelligence Intelligence { get; set; }

        internal Wisdom Wisdom { get; set; }

        internal Race Race { get; set; }

        internal Size Size => Race.Size;

        internal void IncreaseAbilityScoresBasedOnRace()
        {
            foreach (var raceAbilityScoreModifier in Race.AbilityScoreModifiers)
            {
                abilityScoreIncreases[raceAbilityScoreModifier.AbilityScoreName](raceAbilityScoreModifier);
            }
        }
    }
}