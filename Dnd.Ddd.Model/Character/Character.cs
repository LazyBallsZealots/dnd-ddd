using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Dnd.Ddd.Common.ModelFramework;
using Dnd.Ddd.Model.Character.ValueObjects;
using Dnd.Ddd.Model.Character.ValueObjects.AbilityScores.Values;
using Dnd.Ddd.Model.Character.ValueObjects.Race;
using Dnd.Ddd.Model.Character.ValueObjects.Race.AbilityScoreBonuses;
using Dnd.Ddd.Model.Character.ValueObjects.Race.Traits;

[assembly: InternalsVisibleTo("Dnd.Ddd.Infrastructure.Database")]
[assembly: InternalsVisibleTo("Dnd.Ddd.Infrastructure.Tests")]

namespace Dnd.Ddd.Model.Character
{
    public class Character : Entity, IAggregateRoot
    {
        // TODO: refactor this to a different type
        private readonly IDictionary<string, Action<AbilityScoreBonus>> abilityScoreIncreases;

        private string name;

        private int strength;

        private int dexterity;

        private int constitution;

        private int intelligence;

        private int wisdom;

        private int charisma;

        private string race;

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

        public int StrengthValue => Strength.ToInteger();

        public int DexterityValue => Dexterity.ToInteger();

        public int ConstitutionValue => Constitution.ToInteger();

        public int CharismaValue => Charisma.ToInteger();

        public int IntelligenceValue => Intelligence.ToInteger();

        public int WisdomValue => Wisdom.ToInteger();

        public string CharacterName => Name.ToString();

        public string RaceName => Race.ToString();

        public int SpeedValue => Race.Speed.ToInteger();

        public string SizeName => Race.Size.SizeName;

        internal CreatorId Creator
        {
            get => CreatorId.FromUiD(creatorId);
            set => creatorId = value.ToUiD();
        }

        internal Name Name
        {
            get => Name.FromString(name);
            set => name = value.ToString();
        }

        internal Strength Strength
        {
            get => Strength.FromInteger(strength);
            set => strength = value.ToInteger();
        }

        internal Dexterity Dexterity
        {
            get => Dexterity.FromInteger(dexterity);
            set => dexterity = value.ToInteger();
        }

        internal Constitution Constitution
        {
            get => Constitution.FromInteger(constitution);
            set => constitution = value.ToInteger();
        }

        internal Charisma Charisma
        {
            get => Charisma.FromInteger(charisma);
            set => charisma = value.ToInteger();
        }

        internal Intelligence Intelligence
        {
            get => Intelligence.FromInteger(intelligence);
            set => intelligence = value.ToInteger();
        }

        internal Wisdom Wisdom
        {
            get => Wisdom.FromInteger(wisdom);
            set => wisdom = value.ToInteger();
        }

        internal Race Race
        {
            get => Race.FromEnumeration(Enum.Parse<Races>(race));
            set => race = value.ToString();
        }

        internal Size Size => Race.Size;

        protected internal void IncreaseAbilityScoresBasedOnRace()
        {
            foreach (var raceAbilityScoreModifier in Race.AbilityScoreModifiers)
            {
                abilityScoreIncreases[raceAbilityScoreModifier.AbilityScoreName](raceAbilityScoreModifier);
            }
        }
    }
}