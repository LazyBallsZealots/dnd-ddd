using System;

using Autofac;

using Dnd.Ddd.Common.Infrastructure.UnitOfWork;
using Dnd.Ddd.Infrastructure.Tests.Fixture;
using Dnd.Ddd.Infrastructure.Tests.TestsCollection.Names;
using Dnd.Ddd.Model;
using Dnd.Ddd.Model.Character;
using Dnd.Ddd.Model.Character.Repository;
using Dnd.Ddd.Model.Character.Saga;
using Dnd.Ddd.Model.Character.ValueObjects;
using Dnd.Ddd.Model.Character.ValueObjects.Race;

using NHibernate;

using Xunit;

namespace Dnd.Ddd.Infrastructure.Tests.Saga
{
    [Collection(TestCollectionNames.IntegrationTestsCollection), Trait("Category", TestCategory)]
    public class CharacterCreationSagaTests : IDisposable
    {
        private const string TestCategory = "Integration tests: CharacterCreationSaga";

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

            var character = session.QueryOver<Character>()
                .Where(c => c.Name == Name.FromString(CharacterName))
                .And(c => c.Race == Race.FromEnumeration(Races.Elf))
                .SingleOrDefault();

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