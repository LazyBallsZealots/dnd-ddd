using System;

using Dnd.Ddd.Common.ModelFramework;
using Dnd.Ddd.Model.Character.ValueObjects.Race.AbilityScoreBonuses;
using Dnd.Ddd.Model.Character.ValueObjects.Race.Main;
using Dnd.Ddd.Model.Character.ValueObjects.Race.Traits;

namespace Dnd.Ddd.Model.Character.ValueObjects.Race
{
    internal abstract class Race : ValueObject<Race>
    {
        internal abstract AbilityScoreBonusCollection AbilityScoreModifiers { get; }

        internal abstract Speed Speed { get; }

        internal abstract Size Size { get; }

        public static Race FromEnumeration(Races race)
        {
            switch (race)
            {
                case Races.Dragonborn:
                    return Dragonborn.New();
                case Races.Dwarf:
                    return Dwarf.New();
                case Races.Elf:
                    return Elf.New();
                case Races.Gnome:
                    return Gnome.New();
                case Races.HalfElf:
                    return HalfElf.New();
                case Races.HalfOrc:
                    return HalfOrc.New();
                case Races.Halfling:
                    return Halfling.New();
                case Races.Human:
                    return Human.New();
                case Races.Tiefling:
                    return Tiefling.New();
                default:
                    throw new ArgumentOutOfRangeException(nameof(race), race, null);
            }
        }

        public override string ToString() => GetType().Name;

        protected override bool InternalEquals(Race valueObject) => valueObject.GetType() == GetType();

        protected override int InternalGetHashCode() => HashCode.Combine(GetType(), AbilityScoreModifiers);
    }
}