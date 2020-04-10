using System;

using Dnd.Ddd.Common.ModelFramework;
using Dnd.Ddd.Model.Character.ValueObjects.Race.CharacteristicBonus;
using Dnd.Ddd.Model.Character.ValueObjects.Race.Traits;
using Dnd.Ddd.Model.Race.Main;

namespace Dnd.Ddd.Model.Race
{
    internal abstract class Race : ValueObject<Race>
    {
        internal abstract CharacteristicBonusCollection CharacteristicModifiers { get; }

        internal abstract Speed Speed { get; }

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

        protected override bool InternalEquals(Race valueObject) => valueObject.GetType() == GetType();

        protected override int InternalGetHashCode() => HashCode.Combine(GetType(), CharacteristicModifiers);
    }
}