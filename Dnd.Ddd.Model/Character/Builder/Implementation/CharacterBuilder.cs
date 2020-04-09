﻿using Dnd.Ddd.Model.Character.ValueObjects;
using Dnd.Ddd.Model.Character.ValueObjects.Characteristics.Values;

namespace Dnd.Ddd.Model.Character.Builder.Implementation
{
    public class CharacterBuilder : ICharacterBuilder
    {
        private Strength strength;

        private Dexterity dexterity;

        private Constitution constitution;

        private Wisdom wisdom;

        private Intelligence intelligence;

        private Charisma charisma;

        private Name name;

        private Race.Race race;

        public CharacterBuilder()
        {
        }

        public Character Build()
        {
            var character = new Character
            {
                Name = name,
                Strength = strength,
                Charisma = charisma,
                Constitution = constitution,
                Dexterity = dexterity,
                Intelligence = intelligence,
                Wisdom = wisdom,
                Race = race
            };

            character.IncreaseAbilityScoresBasedOnRace();

            return character;
        }

        public ICharacterBuilder SetStrength(int level)
        {
            strength = Strength.FromInteger(level);
            return this;
        }

        public ICharacterBuilder SetDexterity(int level)
        {
            dexterity = Dexterity.FromInteger(level);
            return this;
        }

        public ICharacterBuilder SetConstitution(int level)
        {
            constitution = Constitution.FromInteger(level);
            return this;
        }

        public ICharacterBuilder SetWisdom(int level)
        {
            wisdom = Wisdom.FromInteger(level);
            return this;
        }

        public ICharacterBuilder SetIntelligence(int level)
        {
            intelligence = Intelligence.FromInteger(level);
            return this;
        }

        public ICharacterBuilder SetCharisma(int level)
        {
            charisma = Charisma.FromInteger(level);
            return this;
        }

        public ICharacterBuilder Named(string characterName)
        {
            name = Name.FromString(characterName);
            return this;
        }

        public ICharacterBuilder OfRace(Races characterRace)
        {
            race = Race.Race.FromEnumeration(characterRace);
            return this;
        }
    }
}