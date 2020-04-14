using System;
using System.Threading.Tasks;

using Autofac;

using Dnd.Ddd.Common.Dto.Character.Events;
using Dnd.Ddd.Common.Infrastructure.UnitOfWork;
using Dnd.Ddd.Infrastructure.Tests.Fixture;
using Dnd.Ddd.Infrastructure.Tests.TestsCollection.Names;

using FizzWare.NBuilder;

using NHibernate;

using Xunit;

namespace Dnd.Ddd.Infrastructure.Tests
{
    [Collection(TestCollectionNames.IntegrationTestsCollection), Trait("Category", TestCategory)]
    public class AbilityScoresRolledTests : IDisposable
    {
        private const string TestCategory = "AbilityScoresRolled tests";

        private readonly ISession session;

        private readonly IUnitOfWork unitOfWork;

        public AbilityScoresRolledTests(IntegrationTestsFixture fixture)
        {
            session = fixture.Session;
            unitOfWork = fixture.UnitOfWork;
        }

        [Fact]
        public async Task Event_CanBeStored()
        {
            var abilityScoresRolled = Builder<AbilityScoresRolledDto>.CreateNew().With(x => x.CharacterUiD = Guid.NewGuid()).Build();

            var eventId = await session.SaveAsync(abilityScoresRolled);

            Assert.NotNull(session.Get<AbilityScoresRolledDto>(eventId));
        }

        public void Dispose()
        {
            unitOfWork.Rollback();
            if (unitOfWork is IDisposable disposableResource)
            {
                disposableResource.Dispose();
            }
            
            var connection = session.Close();
            connection?.Close();
        }
    }
}