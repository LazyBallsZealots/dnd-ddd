using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Dnd.Ddd.Infrastructure.Mappings.Character
{
    public class CharacterMap : ClassMapping<Model.Character.Character>
    {
        public CharacterMap()
        {
            Lazy(false);
            Id(x => x.UiD, map => map.Generator(Generators.Assigned));
            Property(
                x => x.Strength,
                mapper =>
                {
                    mapper.Access(Accessor.Field);
                    mapper.Type(NHibernateUtil.Int32);
                });
            Property(
                x => x.Dexterity,
                mapper =>
                {
                    mapper.Access(Accessor.Field);
                    mapper.Type(NHibernateUtil.Int32);
                });
            Property(
                x => x.Constitution,
                mapper =>
                {
                    mapper.Access(Accessor.Field);
                    mapper.Type(NHibernateUtil.Int32);
                });
            Property(
                x => x.Intelligence,
                mapper =>
                {
                    mapper.Access(Accessor.Field);
                    mapper.Type(NHibernateUtil.Int32);
                });
            Property(
                x => x.Wisdom,
                mapper =>
                {
                    mapper.Access(Accessor.Field);
                    mapper.Type(NHibernateUtil.Int32);
                });
            Property(
                x => x.Charisma,
                mapper =>
                {
                    mapper.Access(Accessor.Field);
                    mapper.Type(NHibernateUtil.Int32);
                });
            Property(
                x => x.Race,
                mapper =>
                {
                    mapper.Access(Accessor.Field);
                    mapper.Type(NHibernateUtil.String);
                });
            Property(
                x => x.Name,
                mapper =>
                {
                    mapper.Access(Accessor.Field);
                    mapper.Type(NHibernateUtil.String);
                });
        }
    }
}