using Dnd.Ddd.Infrastructure.Database.Mappings.Entities;
using Dnd.Ddd.Infrastructure.Database.Middleware.UserTypes;
using Dnd.Ddd.Model.Character;

using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Dnd.Ddd.Infrastructure.Database.Mappings.Character
{
    public class CharacterMap : BaseEntityMap<Model.Character.Character>
    {
        public CharacterMap()
        {
            Lazy(false);
            Id(x => x.UiD, map => map.Generator(Generators.Assigned));
            Property(x => x.State, pm => pm.Type<CharacterStateType>());
            Component(
                x => x.Strength,
                componentMapper =>
                {
                    componentMapper.Property(
                        x => x.AbilityScoreLevel,
                        mapper => mapper.Column(cm => cm.Name(nameof(Model.Character.Character.Strength))));

                    componentMapper.Insert(true);
                    componentMapper.Update(true);
                });
            Component(
                x => x.Dexterity,
                componentMapper =>
                {
                    componentMapper.Property(
                        x => x.AbilityScoreLevel,
                        mapper => mapper.Column(cm => cm.Name(nameof(Model.Character.Character.Dexterity))));

                    componentMapper.Insert(true);
                    componentMapper.Update(true);
                });
            Component(
                x => x.Constitution,
                componentMapper =>
                {
                    componentMapper.Property(
                        x => x.AbilityScoreLevel,
                        mapper => mapper.Column(cm => cm.Name(nameof(Model.Character.Character.Constitution))));

                    componentMapper.Insert(true);
                    componentMapper.Update(true);
                });
            Component(
                x => x.Intelligence,
                componentMapper =>
                {
                    componentMapper.Property(
                        x => x.AbilityScoreLevel,
                        mapper => mapper.Column(cm => cm.Name(nameof(Model.Character.Character.Intelligence))));

                    componentMapper.Insert(true);
                    componentMapper.Update(true);
                });
            Component(
                x => x.Wisdom,
                componentMapper =>
                {
                    componentMapper.Property(
                        x => x.AbilityScoreLevel,
                        mapper => mapper.Column(cm => cm.Name(nameof(Model.Character.Character.Wisdom))));

                    componentMapper.Insert(true);
                    componentMapper.Update(true);
                });
            Component(
                x => x.Charisma,
                componentMapper =>
                {
                    componentMapper.Property(
                        x => x.AbilityScoreLevel,
                        mapper => mapper.Column(cm => cm.Name(nameof(Model.Character.Character.Charisma))));

                    componentMapper.Insert(true);
                    componentMapper.Update(true);
                });
            Property(x => x.Race, pm => pm.Type<RaceType>());
            Component(
                x => x.Name,
                componentMapper =>
                {
                    componentMapper.Property(
                        x => x.CharacterName,
                        mapper => mapper.Column(cm => cm.Name(nameof(Model.Character.Character.Name))));

                    componentMapper.Insert(true);
                    componentMapper.Update(true);
                });
            Component(
                x => x.PlayerId,
                componentMapper =>
                {
                    componentMapper.Property(
                        x => x.PlayerUiD,
                        mapper => mapper.Column(cm => cm.Name(nameof(Model.Character.Character.PlayerId))));

                    componentMapper.Insert(true);
                    componentMapper.Update(true);
                });
        }
    }
}