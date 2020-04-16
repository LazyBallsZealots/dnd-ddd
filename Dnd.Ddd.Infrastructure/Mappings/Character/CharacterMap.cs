using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Dnd.Ddd.Infrastructure.Database.Mappings.Character
{
    public class CharacterMap : ClassMapping<Model.Character.Character>
    {
        // TODO: Optimistic locking
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
                        pm =>
                        {
                            pm.Access(Accessor.ReadOnly);
                            pm.Column(nameof(Model.Character.Character.Strength));
                        });
                });
            Component(
                x => x.Dexterity,
                componentMapper =>
                {
                    componentMapper.Property(
                        x => x.AbilityScoreLevel,
                        pm =>
                        {
                            pm.Access(Accessor.ReadOnly);
                            pm.Column(nameof(Model.Character.Character.Dexterity));
                        });
                });
            Component(
                x => x.Constitution,
                componentMapper =>
                {
                    componentMapper.Property(
                        x => x.AbilityScoreLevel,
                        pm =>
                        {
                            pm.Access(Accessor.ReadOnly);
                            pm.Column(nameof(Model.Character.Character.Constitution));
                        });
                });
            Component(
                x => x.Intelligence,
                componentMapper =>
                {
                    componentMapper.Property(
                        x => x.AbilityScoreLevel,
                        pm =>
                        {
                            pm.Access(Accessor.ReadOnly);
                            pm.Column(nameof(Model.Character.Character.Intelligence));
                        });
                });
            Component(
                x => x.Wisdom,
                componentMapper =>
                {
                    componentMapper.Property(
                        x => x.AbilityScoreLevel,
                        pm =>
                        {
                            pm.Access(Accessor.ReadOnly);
                            pm.Column(nameof(Model.Character.Character.Wisdom));
                        });
                });
            Component(
                x => x.Charisma,
                componentMapper =>
                {
                    componentMapper.Property(
                        x => x.AbilityScoreLevel,
                        pm =>
                        {
                            pm.Access(Accessor.ReadOnly);
                            pm.Column(nameof(Model.Character.Character.Charisma));
                        });
                });
            Property(
                x => x.Race,
                pm =>
                {
                    pm.Access(Accessor.Field);
                    pm.Type(NHibernateUtil.String);
                });
            Component(
                x => x.Name,
                componentMapper =>
                {
                    componentMapper.Property(
                        x => x.CharacterName,
                        pm =>
                        {
                            pm.Access(Accessor.ReadOnly);
                            pm.Column(nameof(Model.Character.Character.Name));
                        });
                });
        }
    }
}