using Dnd.Ddd.Common.Infrastructure.Queries;
using Dnd.Ddd.Model.Character;
using Dnd.Ddd.Model.Character.Repository;

namespace Dnd.Ddd.Services.Queries.Handlers
{
    internal class GetCharacterByIdQueryHandler : IQueryHandler<GetCharacterByIdQuery, Character>
    {
        private readonly ICharacterRepository repository;

        public GetCharacterByIdQueryHandler(ICharacterRepository repository)
        {
            this.repository = repository;
        }

        public Character Handle(GetCharacterByIdQuery query) => repository.Get(query.CharacterId);
    }
}
