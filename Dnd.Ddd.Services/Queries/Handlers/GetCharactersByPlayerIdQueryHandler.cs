using System.Collections.Generic;
using System.Linq;

using Dnd.Ddd.Common.Infrastructure.Queries;
using Dnd.Ddd.Model.Character;
using Dnd.Ddd.Model.Character.Repository;

namespace Dnd.Ddd.Services.Queries.Handlers
{
    internal class GetCharactersByPlayerIdQueryHandler : IQueryHandler<GetCharactersByPlayerIdQuery, IList<Character>>
    {
        private readonly ICharacterRepository repository;

        public GetCharactersByPlayerIdQueryHandler(ICharacterRepository repository)
        {
            this.repository = repository;
        }

        public IList<Character> Handle(GetCharactersByPlayerIdQuery query) => repository.GetByPlayerId(query.PlayerId).ToList();
    }
}
