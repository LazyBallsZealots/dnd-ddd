using Dnd.Ddd.Infrastructure.Database.Mappings.Entities;
using Dnd.Ddd.Infrastructure.Database.Middleware;

using NHibernate;
using NHibernate.Mapping.ByCode;

namespace Dnd.Ddd.Infrastructure.Database.Mappings.Character
{
    public class CharacterMap : BaseEntityMap<Model.Character.Character>
    {
        public CharacterMap()
        {
            Lazy(false);
            Id(x => x.UiD, map => map.Generator(Generators.Assigned));
            Component(
                x => x.Strength,
                componentMapper =>
                {
                    componentMapper.Property(
                        x => x.AbilityScoreLevel,
                        mapper =>
                        {
                            mapper.Column(cm => cm.Name(nameof(Model.Character.Character.Strength)));
                            mapper.Type(NHibernateUtil.Int32);
                        });
                    componentMapper.Insert(true);
                    componentMapper.Update(true);
                });
            Component(
                x => x.Dexterity,
                componentMapper =>
                {
                    componentMapper.Property(
                        x => x.AbilityScoreLevel,
                        mapper =>
                        {
                            mapper.Column(cm => cm.Name(nameof(Model.Character.Character.Dexterity)));
                            mapper.Type(NHibernateUtil.Int32);
                        });
                    componentMapper.Insert(true);
                    componentMapper.Update(true);
                });
            Component(
                x => x.Constitution,
                componentMapper =>
                {
                    componentMapper.Property(
                        x => x.AbilityScoreLevel,
                        mapper =>
                        {
                            mapper.Column(cm => cm.Name(nameof(Model.Character.Character.Constitution)));
                            mapper.Type(NHibernateUtil.Int32);
                        });
                    componentMapper.Insert(true);
                    componentMapper.Update(true);
                });
            Component(
                x => x.Intelligence,
                componentMapper =>
                {
                    componentMapper.Property(
                        x => x.AbilityScoreLevel,
                        mapper =>
                        {
                            mapper.Column(cm => cm.Name(nameof(Model.Character.Character.Intelligence)));
                            mapper.Type(NHibernateUtil.Int32);
                        });
                    componentMapper.Insert(true);
                    componentMapper.Update(true);
                });
            Component(
                x => x.Wisdom,
                componentMapper =>
                {
                    componentMapper.Property(
                        x => x.AbilityScoreLevel,
                        mapper =>
                        {
                            mapper.Column(cm => cm.Name(nameof(Model.Character.Character.Wisdom)));
                            mapper.Type(NHibernateUtil.Int32);
                        });
                    componentMapper.Insert(true);
                    componentMapper.Update(true);
                });
            Component(
                x => x.Charisma,
                componentMapper =>
                {
                    componentMapper.Property(
                        x => x.AbilityScoreLevel,
                        mapper =>
                        {
                            mapper.Column(cm => cm.Name(nameof(Model.Character.Character.Charisma)));
                            mapper.Type(NHibernateUtil.Int32);
                        });
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
                        mapper =>
                        {
                            mapper.Column(cm => cm.Name(nameof(Model.Character.Character.Name)));
                            mapper.Type(NHibernateUtil.String);
                        });
                    componentMapper.Insert(true);
                    componentMapper.Update(true);
                });
        }
    }
}