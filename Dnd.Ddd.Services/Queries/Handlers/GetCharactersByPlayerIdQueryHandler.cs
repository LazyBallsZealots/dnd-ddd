using System.Collections.Generic;
using System.Linq;

using Dnd.Ddd.Common.Infrastructure.Queries;
using Dnd.Ddd.Dtos;
using Dnd.Ddd.Dtos.Extensions;
using Dnd.Ddd.Model.Character.Repository;

namespace Dnd.Ddd.Services.Queries.Handlers
{
    internal class GetCharactersByPlayerIdQueryHandler : IQueryHandler<GetCharactersByPlayerIdQuery, IList<CharacterDto>>
    {
        private readonly ICharacterRepository repository;

        public GetCharactersByPlayerIdQueryHandler(ICharacterRepository repository)
        {
            this.repository = repository;
        }

        public IList<CharacterDto> Handle(GetCharactersByPlayerIdQuery query) =>
            repository.GetByPlayerId(query.PlayerId).Select(character => character.ToDto()).ToList();
    }
}