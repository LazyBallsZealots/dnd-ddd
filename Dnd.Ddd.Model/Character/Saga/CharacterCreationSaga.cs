using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dnd.Ddd.Common.Guard;
using Dnd.Ddd.Common.Infrastructure.Events;
using Dnd.Ddd.Common.Infrastructure.UnitOfWork;
using Dnd.Ddd.Common.ModelFramework;
using Dnd.Ddd.Model.Character.CharacterStates;
using Dnd.Ddd.Model.Character.DomainEvents;
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
            await HandleNotification(notification, cancellationToken);
        }

        public async Task Handle(CharacterNameChosen notification, CancellationToken cancellationToken)
        {
            await HandleNotification(notification, cancellationToken);
        }

        public async Task Handle(CharacterRaceChosen notification, CancellationToken cancellationToken)
        {
            await HandleNotification(notification, cancellationToken);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (unitOfWork is IDisposable disposableUnitOfWork)
            {
                disposableUnitOfWork.Dispose();
            }
        }

        private async Task HandleNotification(CharacterEvent notification, CancellationToken cancellationToken)
        {
            var character = await GetCharacter(notification, cancellationToken);
            Guard.With<InvalidCharacterStateException>().Against(character.State is Completed, notification.CharacterUiD);
            CheckSagaCompletion(character, notification);
        }

        private async Task<Character> GetCharacter(CharacterEvent notification, CancellationToken cancellation)
        {
            var character = await characterRepository.GetAsync(notification.CharacterUiD, cancellation) ?? 
                throw new CharacterNotFoundException(notification.CharacterUiD);

            return character;
        }

        private void CheckSagaCompletion(Character draft, BaseDomainEvent domainEvent)
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

        private bool IsComplete(Character character, BaseDomainEvent domainEvent)
        {
            var events = characterRepository.GetDomainEventsForCharacter(character.UiD).Append(domainEvent).ToList();

            return events.Any(e => e is AbilityScoresRolled) &&
                   events.Any(e => e is CharacterNameChosen) &&
                   events.Any(e => e is CharacterRaceChosen);
        }

        private void CreateCharacterFromDraft(Character characterDraft)
        {
            characterDraft.Complete();
            characterRepository.Update(characterDraft);
            unitOfWork.Commit();
        }
    }
}