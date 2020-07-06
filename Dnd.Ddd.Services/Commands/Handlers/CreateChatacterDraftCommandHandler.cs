using System;

using Dnd.Ddd.Common.Infrastructure.Commands;
using Dnd.Ddd.Common.Infrastructure.UnitOfWork;
using Dnd.Ddd.Model.Character;
using Dnd.Ddd.Model.Character.DomainEvents.CharacterCreationEvents;
using Dnd.Ddd.Model.Character.Repository;

namespace Dnd.Ddd.Services.Commands.Handlers
{
    internal class CreateChatacterDraftCommandHandler : IIdResultCommandHandler<CreateCharacterDraftCommand>
    {
        private readonly ICharacterRepository repository;

        private readonly IUnitOfWork unitOfWork;

        public CreateChatacterDraftCommandHandler(ICharacterRepository repository, IUnitOfWork unitOfWork)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }

        public Guid Handle(CreateCharacterDraftCommand command)
        {
            var characterDraft = Character.ForPlayer(command.PlayerId);

            characterDraft.RegisterDomainEvent(new CharacterDraftCreated(command.PlayerId));

            var characterDraftId = repository.Save(characterDraft);

            unitOfWork.Commit();

            return characterDraftId;
        }
    }
}