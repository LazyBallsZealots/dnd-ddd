using System;

using Dnd.Ddd.Common.Infrastructure.Commands;
using Dnd.Ddd.Model.Character;
using Dnd.Ddd.Model.Character.DomainEvents.CharacterCreationEvents;
using Dnd.Ddd.Model.Character.Repository;

namespace Dnd.Ddd.Services.Commands.Handlers
{
    internal class CreateChatacterDraftCommandHandler : IIdResultCommandHandler<CreateCharacterDraftCommand>
    {
        private readonly ICharacterRepository repository;

        public CreateChatacterDraftCommandHandler(ICharacterRepository repository)
        {
            this.repository = repository;
        }

        public Guid Handle(CreateCharacterDraftCommand command)
        {
            var characterDraft = CharacterDraft.ForPlayer(command.PlayerId);

            characterDraft.RegisterDomainEvent(new CharacterDraftCreated(command.PlayerId));

            return repository.Save(characterDraft);
        }
    }
}