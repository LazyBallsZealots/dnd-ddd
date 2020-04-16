using System;
using System.Linq;
using System.Threading.Tasks;

using Autofac;

using Dnd.Ddd.Common.Infrastructure.UnitOfWork;
using Dnd.Ddd.Infrastructure.Tests.Fixture;
using Dnd.Ddd.Infrastructure.Tests.TestsCollection.Names;
using Dnd.Ddd.Model.Character;
using Dnd.Ddd.Model.Character.Repository;
using Dnd.Ddd.Model.Character.Saga;

using NHibernate;
using NHibernate.Criterion;

using Xunit;

namespace Dnd.Ddd.Infrastructure.Tests.Saga
{
    [Collection(TestCollectionNames.IntegrationTestsCollection)]
    public class CharacterCreationSagaTests : IDisposable
    {
        private readonly ICharacterRepository characterRepository;

        private readonly ICharacterCreationSagaRepository sagaRepository;

        private readonly IUnitOfWork unitOfWork;

        private readonly ISession session;

        public CharacterCreationSagaTests(IntegrationTestsFixture fixture)
        {
            characterRepository = fixture.LifetimeScope.Resolve<ICharacterRepository>();
            sagaRepository = fixture.LifetimeScope.Resolve<ICharacterCreationSagaRepository>();
            session = fixture.Session;
            unitOfWork = fixture.UnitOfWork;
        }

        public void Dispose()
        {
            try
            {
                unitOfWork.Rollback();
            }
            finally
            {
                if (unitOfWork is IDisposable disposableUnitOfWork)
                {
                    disposableUnitOfWork.Dispose();
                }
            }
        }

        // TODO: finish this spec
        [Fact]
        public void CharacterCreationSaga_OnCompletion_SavesNewCharacter()
        {
            const string CharacterName = "I really hope this works...";
            var characterCreationSaga = new CharacterCreationSaga(Guid.NewGuid());

            var sagaId = sagaRepository.Save(characterCreationSaga);
            characterCreationSaga = characterCreationSaga.InitializeRepository(characterRepository);

            characterCreationSaga.RollAbilityScores(10, 10, 10, 10, 10, 10);
            characterCreationSaga.NameCharacter(CharacterName);
            characterCreationSaga.SetCharacterRace("Elf");

            sagaRepository.Update(characterCreationSaga);

            var character = session.QueryOver<Character>().List().FirstOrDefault();

            Assert.NotNull(character);
        }

        [Fact]
        public void CharacterCreationSaga_OnDeletion_MarksSagaAsDeleted()
        {
            var characterCreationSaga = new CharacterCreationSaga(Guid.NewGuid());
            var sagaId = sagaRepository.Save(characterCreationSaga);
            sagaRepository.Delete(characterCreationSaga);
            Assert.True(sagaRepository.Get(sagaId).IsDeleted);
        }
    }
}