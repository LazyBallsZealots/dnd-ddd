using System;
using System.Threading.Tasks;

using Dnd.Ddd.Common.Dto.Character.Events;
using Dnd.Ddd.Infrastructure.Tests.Fixture;
using Dnd.Ddd.Infrastructure.Tests.TestsCollection.Names;
using Dnd.Ddd.Model.Character.DomainEvents;

using FizzWare.NBuilder;

using NHibernate;

using Xunit;

namespace Dnd.Ddd.Infrastructure.Tests
{
    [Collection(TestCollectionNames.ModelDatabaseTestsCollection)]
    public class AbilityScoresRolledTests : IDisposable
    {
        private readonly ISession session;

        private readonly ITransaction transaction;

        public AbilityScoresRolledTests(ModelDatabaseFixture fixture)
        {
            session = fixture.SessionFactory.OpenSession();
            transaction = session.BeginTransaction();
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
            transaction.Rollback();
            session.Close();
        }
    }
}