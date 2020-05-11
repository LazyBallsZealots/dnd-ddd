using System;

using Dnd.Ddd.Common.Guard;
using Dnd.Ddd.Model.Character.ValueObjects;
using Dnd.Ddd.Model.Character.ValueObjects.AbilityScores.Values;
using Dnd.Ddd.Model.Character.ValueObjects.Race;

namespace Dnd.Ddd.Model.Character
{
    public class CharacterDraft : Character
    {
        protected CharacterDraft()
        {
        }

        internal CharacterDraft(PlayerId playerId)
        {
            PlayerId = playerId;
        }

        public static CharacterDraft ForPlayer(Guid playerId)
        {
            Guard.With<ArgumentException>().Against(playerId.Equals(Guid.Empty), nameof(playerId));

            return new CharacterDraft(PlayerId.FromUiD(playerId));
        }

        public CharacterDraft SetStrength(int strength)
        {
            Strength = Strength.FromInteger(strength);
            return this;
        }

        public CharacterDraft SetDexterity(int dexterity)
        {
            Dexterity = Dexterity.FromInteger(dexterity);
            return this;
        }

        public CharacterDraft SetCharisma(int charisma)
        {
            Charisma = Charisma.FromInteger(charisma);
            return this;
        }

        public CharacterDraft SetWisdom(int wisdom)
        {
            Wisdom = Wisdom.FromInteger(wisdom);
            return this;
        }

        public CharacterDraft SetConstitution(int constitution)
        {
            Constitution = Constitution.FromInteger(constitution);
            return this;
        }

        public CharacterDraft SetIntelligence(int intelligence)
        {
            Intelligence = Intelligence.FromInteger(intelligence);
            return this;
        }

        public void SetRace(string race)
        {
            Guard.With<ArgumentOutOfRangeException>().Against(!Enum.TryParse(typeof(Races), race, out _), nameof(race));
            Race = Race.FromEnumeration(Enum.Parse<Races>(race));
        }

        public void SetName(string name) => Name = Name.FromString(name);
    }
}