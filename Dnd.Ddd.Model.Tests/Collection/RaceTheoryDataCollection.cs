using System;
using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace Dnd.Ddd.Model.Tests.Collection
{
    public class RaceTheoryDataCollection : TheoryData<Races, IList<int>, Func<Character.Character, bool>>
    {
        public RaceTheoryDataCollection()
        {
            var random = new Random();
            var stats = Enumerable.Range(1, 6).Select(index => random.Next(3, 19)).ToArray();
            foreach (Races race in Enum.GetValues(typeof(Races)))
            {
                switch (race)
                {
                    case Races.Dragonborn:
                    {
                        var expectedStats = new List<int>(stats);
                        expectedStats[0] += 2;
                        expectedStats[5] += 1;
                        Add(race, stats, character => CheckStats(character, expectedStats));
                        continue;
                    }

                    case Races.Dwarf:
                    {
                        var expectedStats = new List<int>(stats);
                        expectedStats[2] += 2;
                        Add(race, stats, character => CheckStats(character, expectedStats));
                        continue;
                    }

                    case Races.Elf:
                    {
                        var expectedStats = new List<int>(stats);
                        expectedStats[1] += 2;
                        Add(race, stats, character => CheckStats(character, expectedStats));
                        continue;
                    }

                    case Races.Gnome:
                    {
                        var expectedStats = new List<int>(stats);
                        expectedStats[3] += 2;
                        Add(race, stats, character => CheckStats(character, expectedStats));
                        continue;
                    }

                    case Races.HalfElf:
                    {
                        var expectedStats = new List<int>(stats);
                        expectedStats[5] += 2;
                        Add(race, stats, character => CheckStats(character, expectedStats));
                        continue;
                    }

                    case Races.Halfling:
                    {
                        var expectedStats = new List<int>(stats);
                        expectedStats[1] += 2;
                        Add(race, stats, character => CheckStats(character, expectedStats));
                        continue;
                    }

                    case Races.HalfOrc:
                    {
                        var expectedStats = new List<int>(stats);
                        expectedStats[0] += 2;
                        expectedStats[2] += 1;
                        Add(race, stats, character => CheckStats(character, expectedStats));
                        continue;
                    }

                    case Races.Human:
                    {
                        var expectedStats = new List<int>(stats.Select(stat => stat + 1));
                        Add(race, stats, character => CheckStats(character, expectedStats));
                        continue;
                    }

                    case Races.Tiefling:
                    {
                        var expectedStats = new List<int>(stats);
                        expectedStats[5] += 2;
                        expectedStats[3] += 1;
                        Add(race, stats, character => CheckStats(character, expectedStats));
                        continue;
                    }

                    default:
                        throw new ArgumentOutOfRangeException(nameof(race));
                }
            }
        }

        private static bool CheckStats(Character.Character character, IList<int> expectedStats) =>
            expectedStats[0] == character.StrengthValue &&
            expectedStats[1] == character.DexterityValue &&
            expectedStats[2] == character.ConstitutionValue &&
            expectedStats[3] == character.IntelligenceValue &&
            expectedStats[4] == character.WisdomValue &&
            expectedStats[5] == character.CharismaValue;
    }
}