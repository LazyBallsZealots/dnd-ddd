using Dnd.Ddd.Common.Guard;
using Dnd.Ddd.Common.Infrastructure.Commands;
using Dnd.Ddd.Common.Infrastructure.UnitOfWork;

using Dnd.Ddd.Model.Character.DomainEvents.CharacterCreationEvents;
using Dnd.Ddd.Model.Character.Exceptions;
using Dnd.Ddd.Model.Character.Repository;

namespace Dnd.Ddd.Services.Commands.Handlers
{
    internal class ChooseCharacterRaceCommandHandler : IEmptyResultCommandHandler<ChooseCharacterRaceCommand>
    {
        private readonly ICharacterRepository repository;

        private readonly IUnitOfWork unitOfWork;

        public ChooseCharacterRaceCommandHandler(
            ICharacterRepository repository,
            IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public void Handle(ChooseCharacterRaceCommand command)
        {
            var character = repository.Get(command.CharacterUiD);
            Guard.With<CharacterNotFoundException>().Against(character is null, command.CharacterUiD);
            Guard.With<InvalidCharacterStateException>()
                .Against(
                    character.IsCompleted(),
                    command.CharacterUiD);

            character.SetRace(command.Race);

            character.RegisterDomainEvent(new CharacterRaceChosen(command.Race, command.CharacterUiD));

            repository.Update(character);

            unitOfWork.Commit();
        }
    }
}