using System;
using System.Collections.Generic;

using Dnd.Ddd.Common.Guard;
using Dnd.Ddd.Common.ModelFramework;
using Dnd.Ddd.Model.Character.CharacterStates;
using Dnd.Ddd.Model.Character.CharacterStates.Collections;
using Dnd.Ddd.Model.Character.CharacterStates.Contract;
using Dnd.Ddd.Model.Character.ValueObjects;
using Dnd.Ddd.Model.Character.ValueObjects.AbilityScores.Values;
using Dnd.Ddd.Model.Character.ValueObjects.Feature;
using Dnd.Ddd.Model.Character.ValueObjects.Race;
using Dnd.Ddd.Model.Character.ValueObjects.Race.AbilityScoreBonuses;

namespace Dnd.Ddd.Model.Character
{
    public class Character : Entity, IAggregateRoot
    {
        private readonly AbilityScoreIncreasesCollection abilityScoreIncreases;

        protected Character()
        {
            abilityScoreIncreases = new AbilityScoreIncreasesCollection(this);
        }

        internal Character(PlayerId playerId)
            : this()
        {
            PlayerId = playerId;
        }

        internal ICollection<Feature> Features { get; private set; }

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
            CheckCharacterCompletion();
            return this;
        }

        public Character SetDexterity(int dexterity)
        {
            State.SetDexterity(this, dexterity);
            CheckCharacterCompletion();
            return this;
        }

        public Character SetCharisma(int charisma)
        {
            State.SetCharisma(this, charisma);
            CheckCharacterCompletion();
            return this;
        }

        public Character SetWisdom(int wisdom)
        {
            State.SetWisdom(this, wisdom);
            CheckCharacterCompletion();
            return this;
        }

        public Character SetConstitution(int constitution)
        {
            State.SetConstitution(this, constitution);
            CheckCharacterCompletion();
            return this;
        }

        public Character SetIntelligence(int intelligence)
        {
            State.SetIntelligence(this, intelligence);
            CheckCharacterCompletion();
            return this;
        }

        public void SetRace(string race)
        {
            Guard.With<ArgumentOutOfRangeException>().Against(!Enum.TryParse(typeof(Races), race, out _), nameof(race));
            State.SetRace(this, race);
            CheckCharacterCompletion();
        }

        public void SetName(string name)
        {
            State.SetName(this, name);
            CheckCharacterCompletion();
        }

        public void Complete()
        {
            IncreaseAbilityScoresBasedOnRace();

            State = new Completed();
        }

        protected internal void IncreaseAbilityScoresBasedOnRace()
        {
            foreach (var raceAbilityScoreModifier in Race.AbilityScoreModifiers)
            {
                abilityScoreIncreases.IncreaseAbilityScore(raceAbilityScoreModifier);
            }
        }

        private void CheckCharacterCompletion()
        {
            if (State.CanChangeState(this))
            {
                Complete();
            }
        }
    }
}