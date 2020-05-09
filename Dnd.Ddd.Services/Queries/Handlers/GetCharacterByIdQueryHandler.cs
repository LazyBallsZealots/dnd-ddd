using Dnd.Ddd.Common.Infrastructure.Queries;
using Dnd.Ddd.Dtos;
using Dnd.Ddd.Dtos.Extensions;
using Dnd.Ddd.Model.Character.Repository;

namespace Dnd.Ddd.Services.Queries.Handlers
{
    internal class GetCharacterByIdQueryHandler : IQueryHandler<GetCharacterByIdQuery, CharacterDto>
    {
        private readonly ICharacterRepository repository;

        public GetCharacterByIdQueryHandler(ICharacterRepository repository)
        {
            this.repository = repository;
        }

        public CharacterDto Handle(GetCharacterByIdQuery query) => repository.Get(query.CharacterId).ToDto();

    }
}
