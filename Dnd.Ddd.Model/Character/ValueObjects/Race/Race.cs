using System;
using System.Collections.Generic;

using Dnd.Ddd.Common.Guard;
using Dnd.Ddd.Common.ModelFramework;
using Dnd.Ddd.Model.Character.ValueObjects.Race.AbilityScoreBonuses;
using Dnd.Ddd.Model.Character.ValueObjects.Race.Main;
using Dnd.Ddd.Model.Character.ValueObjects.Race.Traits;

namespace Dnd.Ddd.Model.Character.ValueObjects.Race
{
    internal abstract class Race : ValueObject<Race>
    {
        private static readonly IDictionary<Races, Func<Race>> RacesFactoryMethods = new Dictionary<Races, Func<Race>>
        {
            [Races.Dragonborn] = Dragonborn.New,
            [Races.Dwarf] = Dwarf.New,
            [Races.Elf] = Elf.New,
            [Races.Gnome] = Gnome.New,
            [Races.HalfElf] = HalfElf.New,
            [Races.HalfOrc] = HalfOrc.New,
            [Races.Halfling] = Halfling.New,
            [Races.Human] = Human.New,
            [Races.Tiefling] = Tiefling.New
        };

        protected internal virtual string RaceName => ToString();

        internal abstract AbilityScoreBonusCollection AbilityScoreModifiers { get; }

        internal abstract Speed Speed { get; }

        internal abstract Size Size { get; }

        public static Race FromEnumeration(Races race)
        {
            Guard.With<ArgumentOutOfRangeException>().Against(!RacesFactoryMethods.ContainsKey(race), nameof(race));

            return RacesFactoryMethods[race]();
        }

        public override string ToString() => GetType().Name;

        protected override bool InternalEquals(Race valueObject) => valueObject.GetType() == GetType();

        protected override int InternalGetHashCode() => HashCode.Combine(GetType(), AbilityScoreModifiers);
    }
}