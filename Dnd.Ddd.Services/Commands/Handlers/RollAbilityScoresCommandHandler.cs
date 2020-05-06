using System;

using Dnd.Ddd.Common.Guard;
using Dnd.Ddd.Common.Infrastructure.Commands;
using Dnd.Ddd.Common.Infrastructure.UnitOfWork;
using Dnd.Ddd.Model.Character;
using Dnd.Ddd.Model.Character.DomainEvents.CharacterCreationEvents;
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
            Guard.With<InvalidOperationException>()
                .Against(
                    !(character is CharacterDraft),
                    $"Attempting to roll ability scores on completed character with UiD: {command.CharacterUiD}!");

            var characterWithRolledAbilityScores = ((CharacterDraft)character).SetStrength(command.Strength)
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