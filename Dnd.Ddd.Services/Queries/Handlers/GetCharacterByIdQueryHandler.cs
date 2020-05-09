using Dnd.Ddd.Common.Infrastructure.Queries;
using Dnd.Ddd.Model.Character;
using Dnd.Ddd.Model.Character.Repository;
using Dnd.Ddd.Dtos;
using System.ComponentModel.DataAnnotations;

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
