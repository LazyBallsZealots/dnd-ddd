using System;

using Dnd.Ddd.Common.Guard;
using Dnd.Ddd.Common.Infrastructure.Commands;
using Dnd.Ddd.Model.Character;
using Dnd.Ddd.Model.Character.DomainEvents.CharacterCreationEvents;
using Dnd.Ddd.Model.Character.Repository;

namespace Dnd.Ddd.Services.Commands.Handlers
{
    internal class ChooseCharacterRaceCommandHandler : IEmptyResultCommandHandler<ChooseCharacterRaceCommand>
    {
        private readonly ICharacterRepository repository;

        public ChooseCharacterRaceCommandHandler(ICharacterRepository repository)
        {
            this.repository = repository;
        }

        public void Handle(ChooseCharacterRaceCommand command)
        {
            var character = repository.Get(command.CharacterUiD);
            Guard.With<InvalidOperationException>()
                .Against(
                    !(character is CharacterDraft),
                    $"Attempting to change race on a completed character with UiD: {command.CharacterUiD}!");

            ((CharacterDraft)character).SetRace(command.Race);

            character.RegisterDomainEvent(new CharacterRaceChosen(command.Race, command.CharacterUiD));

            repository.Update(character);
        }
    }
}