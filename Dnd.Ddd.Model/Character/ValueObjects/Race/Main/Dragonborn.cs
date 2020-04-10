﻿using Dnd.Ddd.Model.Character.ValueObjects.Race.CharacteristicBonus;
using Dnd.Ddd.Model.Character.ValueObjects.Race.CharacteristicBonus.Type;
using Dnd.Ddd.Model.Character.ValueObjects.Race.Traits;

namespace Dnd.Ddd.Model.Race.Main
{
    internal class Dragonborn : Race
    {
        internal override Speed Speed => Speed.FromInteger(30);

        private Dragonborn()
        {
        }

        internal override CharacteristicBonusCollection CharacteristicModifiers =>
            new CharacteristicBonusCollection(
                new CharacteristicBonus[]
                {
                    StrengthBonus.FromInteger(2),
                    CharismaBonus.FromInteger(1)
                });

        public static Dragonborn New() => new Dragonborn();
    }
}