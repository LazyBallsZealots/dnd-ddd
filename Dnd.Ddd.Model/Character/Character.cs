using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Dnd.Ddd.Common.Guard;
using Dnd.Ddd.Common.ModelFramework;
using Dnd.Ddd.Model.Character.CharacterStates;
using Dnd.Ddd.Model.Character.CharacterStates.Contract;
using Dnd.Ddd.Model.Character.Exceptions;
using Dnd.Ddd.Model.Character.ValueObjects;
using Dnd.Ddd.Model.Character.ValueObjects.AbilityScores.Values;
using Dnd.Ddd.Model.Character.ValueObjects.Race;
using Dnd.Ddd.Model.Character.ValueObjects.Race.AbilityScoreBonuses;

[assembly: InternalsVisibleTo("Dnd.Ddd.Infrastructure.Database")]
[assembly: InternalsVisibleTo("Dnd.Ddd.Infrastructure.Tests")]
[assembly: InternalsVisibleTo("Dnd.Ddd.Model.Tests")]
[assembly: InternalsVisibleTo("Dnd.Ddd.Dtos")]

namespace Dnd.Ddd.Model.Character
{
    public class Character : Entity, IAggregateRoot
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

        internal Character(PlayerId playerId) 
            : this()
        {
            PlayerId = playerId;
        }

        internal CharacterState State { get; private set; }

        internal PlayerId PlayerId { get; set; }

        internal Name Name { get; set; }

        internal Strength Strength { get; set; }

        internal Dexterity Dexterity { get; set; }

        internal Constitution Constitution { get; set; }

        internal Charisma Charisma { get; set; }

        internal Intelligence Intelligence { get; set; }

        internal Wisdom Wisdom { get; set; }

        internal Race Race { get; set; }

        public static Character ForPlayer(Guid playerId)
        {
            Guard.With<ArgumentException>().Against(playerId.Equals(Guid.Empty), nameof(playerId));

            return new Character(PlayerId.FromUiD(playerId))
            {
                State = new Draft()
            };
        }

        public bool IsCompleted() => State is Completed;

        public Character SetStrength(int strength)
        {
            State.SetStrength(this, strength);
            return this;
        }

        public Character SetDexterity(int dexterity)
        {
            State.SetDexterity(this, dexterity);
            return this;
        }

        public Character SetCharisma(int charisma)
        {
            State.SetCharisma(this, charisma);
            return this;
        }

        public Character SetWisdom(int wisdom)
        {
            State.SetWisdom(this, wisdom);
            return this;
        }

        public Character SetConstitution(int constitution)
        {
            State.SetConstitution(this, constitution);
            return this;
        }

        public Character SetIntelligence(int intelligence)
        {
            State.SetIntelligence(this, intelligence);
            return this;
        }

        public void SetRace(string race)
        {
            Guard.With<ArgumentOutOfRangeException>().Against(!Enum.TryParse(typeof(Races), race, out _), nameof(race));
            State.SetRace(this, race);
        }

        public void SetName(string name) => State.SetName(this, name);

        public void Complete()
        {
            Guard.With<ArgumentNullException>().Against(Strength == null, nameof(Strength));
            Guard.With<ArgumentNullException>().Against(Dexterity == null, nameof(Dexterity));
            Guard.With<ArgumentNullException>().Against(Constitution == null, nameof(Constitution));
            Guard.With<ArgumentNullException>().Against(Wisdom == null, nameof(Wisdom));
            Guard.With<ArgumentNullException>().Against(Intelligence == null, nameof(Intelligence));
            Guard.With<ArgumentNullException>().Against(Charisma == null, nameof(Charisma));
            Guard.With<ArgumentNullException>().Against(Name == null, nameof(Name));
            Guard.With<ArgumentNullException>().Against(Race == null, nameof(Race));
            Guard.With<InvalidCharacterStateException>().Against(IsCompleted(), UiD);

            IncreaseAbilityScoresBasedOnRace();

            State = new Completed();
        }

        protected internal void IncreaseAbilityScoresBasedOnRace()
        {
            foreach (var raceAbilityScoreModifier in Race.AbilityScoreModifiers)
            {
                abilityScoreIncreases[raceAbilityScoreModifier.AbilityScoreName](raceAbilityScoreModifier);
            }
        }
    }
}