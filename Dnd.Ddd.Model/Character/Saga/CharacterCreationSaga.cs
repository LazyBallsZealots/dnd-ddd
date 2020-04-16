using System;
using System.Threading.Tasks;

using Dnd.Ddd.Model.Character.Builder.Implementation;
using Dnd.Ddd.Model.Character.DomainEvents;
using Dnd.Ddd.Model.Character.Repository;

namespace Dnd.Ddd.Model.Character.Saga
{
    public class CharacterCreationSaga : Common.ModelFramework.Saga
    {
        public CharacterCreationSaga(Guid creatorId)
        {
            CreatorId = creatorId;
        }

        protected CharacterCreationSaga()
        {
        }

        public virtual Guid CreatorId { get; }

        public virtual AbilityScoresRolled AbilityScoresRolled { get; protected set; }

        public virtual CharacterRaceChosen CharacterRaceChosen { get; protected set; }

        public virtual CharacterNameChosen CharacterNameChosen { get; protected set; }

        public override bool IsComplete => AbilityScoresRolled != null && CharacterRaceChosen != null && CharacterNameChosen != null;

        protected virtual ICharacterRepository CharacterRepository { get; set; }

        public virtual CharacterCreationSaga InitializeRepository(ICharacterRepository repository)
        {
            CharacterRepository = repository;
            return this;
        }

        public virtual void NameCharacter(string name)
        {
            CharacterNameChosen = new CharacterNameChosen(name, UiD);
            RegisterDomainEvent(CharacterNameChosen);
            CheckForCompletion();
        }

        public virtual void SetCharacterRace(string race)
        {
            CharacterRaceChosen = new CharacterRaceChosen(race, UiD);
            RegisterDomainEvent(CharacterRaceChosen);
            CheckForCompletion();
        }

        public virtual void RollAbilityScores(
            int strength,
            int dexterity,
            int constitution,
            int intelligence,
            int wisdom,
            int charisma)
        {
            AbilityScoresRolled = new AbilityScoresRolled(UiD, strength, dexterity, constitution, intelligence, wisdom, charisma);
            RegisterDomainEvent(AbilityScoresRolled);
            CheckForCompletion();
        }

        protected override void Complete()
        {
            var newCharacter = BuildCharacter();
            newCharacter.RegisterDomainEvent(new CharacterCreated(newCharacter.UiD, CreatorId));
            CharacterRepository.Save(newCharacter);
        }

        private Character BuildCharacter() =>
            new CharacterBuilder().SetStrength(AbilityScoresRolled.Strength)
                .SetDexterity(AbilityScoresRolled.Dexterity)
                .SetConstitution(AbilityScoresRolled.Constitution)
                .SetIntelligence(AbilityScoresRolled.Intelligence)
                .SetWisdom(AbilityScoresRolled.Wisdom)
                .SetCharisma(AbilityScoresRolled.Charisma)
                .Named(CharacterNameChosen.CharacterName)
                .OfRace(Enum.Parse<Races>(CharacterRaceChosen.CharacterRace))
                .Build();
    }
}