using System;

using Dnd.Ddd.Common.Guard;
using Dnd.Ddd.Common.Infrastructure.Commands;
using Dnd.Ddd.Model.Character;
using Dnd.Ddd.Model.Character.DomainEvents.CharacterCreationEvents;
using Dnd.Ddd.Model.Character.Repository;

namespace Dnd.Ddd.Services.Commands.Handlers
{
    internal class ChooseCharacterNameCommandHandler : IEmptyResultCommandHandler<ChooseCharacterNameCommand>
    {
        private readonly ICharacterRepository repository;

        public ChooseCharacterNameCommandHandler(ICharacterRepository repository)
        {
            this.repository = repository;
        }

        public void Handle(ChooseCharacterNameCommand command)
        {
            var character = repository.Get(command.CharacterUiD);
            Guard.With<InvalidOperationException>()
                .Against(
                    !(character is CharacterDraft),
                    $"Attempting to change name on a completed character with UiD: {command.CharacterUiD}!");

            ((CharacterDraft)character).SetName(command.Name);

            character.RegisterDomainEvent(new CharacterNameChosen(command.Name, command.CharacterUiD));

            repository.Update(character);
        }
    }
}