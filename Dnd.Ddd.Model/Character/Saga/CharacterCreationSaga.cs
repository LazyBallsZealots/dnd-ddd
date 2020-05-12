using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Dnd.Ddd.Common.Infrastructure.Events;
using Dnd.Ddd.Common.Infrastructure.UnitOfWork;
using Dnd.Ddd.Common.ModelFramework;
using Dnd.Ddd.Model.Character.DomainEvents.CharacterCreationEvents;
using Dnd.Ddd.Model.Character.Exceptions;
using Dnd.Ddd.Model.Character.Repository;

namespace Dnd.Ddd.Model.Character.Saga
{
    public class CharacterCreationSaga : IDomainEventHandler<AbilityScoresRolled>,
                                         IDomainEventHandler<CharacterRaceChosen>,
                                         IDomainEventHandler<CharacterNameChosen>,
                                         IDisposable
    {
        private readonly ICharacterRepository characterRepository;

        private readonly IUnitOfWork unitOfWork;

        public CharacterCreationSaga(ICharacterRepository characterRepository, IUnitOfWork unitOfWork)
        {
            this.characterRepository = characterRepository;
            this.unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task Handle(AbilityScoresRolled notification, CancellationToken cancellationToken)
        {
            if (!(await characterRepository.GetAsync(notification.CharacterUiD, cancellationToken) is CharacterDraft characterDraft))
            {
                throw new CharacterNotFoundException(notification.CharacterUiD);
            }

            CheckSagaCompletion(characterDraft, notification);
        }

        public async Task Handle(CharacterNameChosen notification, CancellationToken cancellationToken)
        {
            if (!(await characterRepository.GetAsync(notification.CharacterUiD, cancellationToken) is CharacterDraft characterDraft))
            {
                throw new CharacterNotFoundException(notification.CharacterUiD);
            }

            CheckSagaCompletion(characterDraft, notification);
        }

        public async Task Handle(CharacterRaceChosen notification, CancellationToken cancellationToken)
        {
            if (!(await characterRepository.GetAsync(notification.CharacterUiD, cancellationToken) is CharacterDraft characterDraft))
            {
                throw new CharacterNotFoundException(notification.CharacterUiD);
            }

            CheckSagaCompletion(characterDraft, notification);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (unitOfWork is IDisposable disposableUnitOfWork)
            {
                disposableUnitOfWork.Dispose();
            }
        }

        private void CheckSagaCompletion(CharacterDraft draft, BaseDomainEvent domainEvent)
        {
            if (IsComplete(draft, domainEvent))
            {
                CreateCharacterFromDraft(draft);
            }
            else
            {
                unitOfWork.Rollback();
            }
        }

        private bool IsComplete(CharacterDraft character, BaseDomainEvent domainEvent)
        {
            var events = characterRepository.GetDomainEventsForCharacter(character.UiD).Append(domainEvent).ToList();

            return events.Any(e => e is AbilityScoresRolled) &&
                   events.Any(e => e is CharacterNameChosen) &&
                   events.Any(e => e is CharacterRaceChosen);
        }

        private void CreateCharacterFromDraft(CharacterDraft characterDraft)
        {
            var newCharacter = CompletedCharacter.FromDraft(characterDraft);
            characterRepository.Update(newCharacter);
            unitOfWork.Commit();
        }
    }
}