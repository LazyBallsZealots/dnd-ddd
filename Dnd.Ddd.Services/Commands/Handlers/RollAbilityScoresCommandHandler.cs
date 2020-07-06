
using Dnd.Ddd.Common.Guard;
using Dnd.Ddd.Common.Infrastructure.Commands;
using Dnd.Ddd.Common.Infrastructure.UnitOfWork;
using Dnd.Ddd.Model.Character.DomainEvents.CharacterCreationEvents;
using Dnd.Ddd.Model.Character.Exceptions;
using Dnd.Ddd.Model.Character.Repository;

namespace Dnd.Ddd.Services.Commands.Handlers
{
    internal class RollAbilityScoresCommandHandler : IEmptyResultCommandHandler<RollAbilityScoresCommand>
    {
        private readonly ICharacterRepository repository;

        private readonly IUnitOfWork unitOfWork;

        public RollAbilityScoresCommandHandler(ICharacterRepository repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public void Handle(RollAbilityScoresCommand command)
        {
            var character = repository.Get(command.CharacterUiD);

            Guard.With<CharacterNotFoundException>()
                .Against(
                    character == null,
                    command.CharacterUiD);

            Guard.With<InvalidCharacterStateException>()
                .Against(
                    character.IsCompleted(),
                    command.CharacterUiD);

            var characterWithRolledAbilityScores = character.SetStrength(command.Strength)
                .SetDexterity(command.Dexterity)
                .SetCharisma(command.Charisma)
                .SetWisdom(command.Wisdom)
                .SetConstitution(command.Constitution)
                .SetIntelligence(command.Intelligence);

            characterWithRolledAbilityScores.RegisterDomainEvent(
                new AbilityScoresRolled(
                    command.CharacterUiD,
                    command.Strength,
                    command.Dexterity,
                    command.Constitution,
                    command.Intelligence,
                    command.Wisdom,
                    command.Charisma));

            repository.Update(characterWithRolledAbilityScores);

            unitOfWork.Commit();
        }
    }
}