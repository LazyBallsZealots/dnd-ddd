using System;

using Dnd.Ddd.Infrastructure.Mappings.Sagas;
using Dnd.Ddd.Model.Character.Saga;

using NHibernate.Mapping.ByCode;

namespace Dnd.Ddd.Infrastructure.Mappings.Character.Sagas
{
    public class CharacterCreationSagaMap : SagaMap<CharacterCreationSaga>
    {
        public CharacterCreationSagaMap()
        {
            Lazy(true);
            Table("CharacterCreationSagas");
            Property(x => x.CreatorId, map => map.Access(Accessor.ReadOnly));
            ManyToOne(x => x.AbilityScoresRolled, EventMapping("AbilityScoresRolledUiD"));
            ManyToOne(x => x.CharacterRaceChosen, EventMapping("CharacterRaceChosenUiD"));
            ManyToOne(x => x.CharacterNameChosen, EventMapping("CharacterNameChosenUiD"));
        }

        private static Action<IManyToOneMapper> EventMapping(string columnName) =>
            map =>
            {
                map.Cascade(Cascade.All | Cascade.DeleteOrphans);
                map.Fetch(FetchKind.Join);
                map.Insert(true);
                map.Update(true);
                map.Unique(true);
                map.Lazy(LazyRelation.Proxy);
                map.NotNullable(false);
                map.Column(cm => cm.Name(columnName));
            };
    }
}