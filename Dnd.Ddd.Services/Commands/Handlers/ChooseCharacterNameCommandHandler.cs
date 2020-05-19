using System;
using Dnd.Ddd.Common.Guard;
using Dnd.Ddd.Common.Infrastructure.Commands;
using Dnd.Ddd.Common.Infrastructure.UnitOfWork;
using Dnd.Ddd.Model.Character;
using Dnd.Ddd.Model.Character.DomainEvents.CharacterCreationEvents;
using Dnd.Ddd.Model.Character.Exceptions;
using Dnd.Ddd.Model.Character.Repository;

namespace Dnd.Ddd.Services.Commands.Handlers
{
    internal class ChooseCharacterNameCommandHandler : IEmptyResultCommandHandler<ChooseCharacterNameCommand>
    {
        private readonly ICharacterRepository repository;

        private readonly IUnitOfWork unitOfWork;

        public ChooseCharacterNameCommandHandler(
            ICharacterRepository repository,
            IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public void Handle(ChooseCharacterNameCommand command)
        {
            var character = repository.Get(command.CharacterUiD);

            Guard.With<CharacterNotFoundException>()
                .Against(
                    character == null,
                    command.CharacterUiD);

            Guard.With<InvalidOperationException>()
                .Against(
                    character.IsCompleted(),
                    $"Attempting to change name on a completed character with UiD: {command.CharacterUiD}!");

            character.SetName(command.Name);

            character.RegisterDomainEvent(new CharacterNameChosen(command.Name, command.CharacterUiD));

            repository.Update(character);

            unitOfWork.Commit();
        }
    }
}