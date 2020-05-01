using Dnd.Ddd.Model.Character.ValueObjects.Race;
using System;
using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace Dnd.Ddd.Model.Tests.Collection
{
    public class RaceTheoryDataCollection : TheoryData<Races, IList<int>, Func<Character.Character, bool>>
    {
        private static readonly IDictionary<Races, Func<IList<int>, IList<int>>> AbilityScoresModifications =
            new Dictionary<Races, Func<IList<int>, IList<int>>>
            {
                [Races.Dragonborn] = PrepareDragonbornAbilityScoresAndSpeed,
                [Races.Dwarf] = PrepareDwarfAbilityScoresAndSpeed,
                [Races.Elf] = PrepareElfAbilityScoresAndSpeed,
                [Races.Gnome] = PrepareGnomeAbilityScoresAndSpeed,
                [Races.HalfElf] = PrepareHalfElfAbilityScoresAndSpeed,
                [Races.Halfling] = PrepareHalflingAbilityScoresAndSpeed,
                [Races.HalfOrc] = PrepareHalfOrcAbilityScoresAndSpeed,
                [Races.Human] = PrepareHumanAbilityScoresAndSpeed,
                [Races.Tiefling] = PrepareTieflingAbilityScoresAndSpeed
            };

        private static readonly IDictionary<Races, string> RaceSizes = new Dictionary<Races, string>
        {
            [Races.Dragonborn] = "Medium",
            [Races.Dwarf] = "Small",
            [Races.Elf] = "Medium",
            [Races.Gnome] = "Small",
            [Races.HalfElf] = "Medium",
            [Races.Halfling] = "Small",
            [Races.HalfOrc] = "Medium",
            [Races.Human] = "Medium",
            [Races.Tiefling] = "Medium"
        };

        public RaceTheoryDataCollection()
        {
            var random = new Random();
            var stats = Enumerable.Range(1, 6).Select(index => random.Next(3, 19)).ToArray();
            foreach (Races race in Enum.GetValues(typeof(Races)))
            {
                Add(race, stats, character => CheckStats(character, AbilityScoresModifications[race](stats), RaceSizes[race]));
            }
        }

        private static bool CheckStats(Character.Character character, IList<int> expectedStats, string size) =>
            expectedStats[0] == character.Strength.ToInteger() &&
            expectedStats[1] == character.Dexterity.ToInteger() &&
            expectedStats[2] == character.Constitution.ToInteger() &&
            expectedStats[3] == character.Intelligence.ToInteger() &&
            expectedStats[4] == character.Wisdom.ToInteger() &&
            expectedStats[5] == character.Charisma.ToInteger() &&
            expectedStats[6] == character.Race.Speed.ToInteger() &&
            size == character.Race.Size.ToString();

        private static IList<int> PrepareDragonbornAbilityScoresAndSpeed(IList<int> abilityScores)
        {
            var expectedStats = new List<int>(abilityScores.Append(30));
            expectedStats[0] += 2;
            expectedStats[5] += 1;
            return expectedStats;
        }

        private static IList<int> PrepareDwarfAbilityScoresAndSpeed(IList<int> arg)
        {
            var expectedStats = new List<int>(arg.Append(25));
            expectedStats[2] += 2;
            return expectedStats;
        }

        private static IList<int> PrepareElfAbilityScoresAndSpeed(IList<int> arg)
        {
            var expectedStats = new List<int>(arg.Append(30));
            expectedStats[1] += 2;
            return expectedStats;
        }

        private static IList<int> PrepareGnomeAbilityScoresAndSpeed(IList<int> arg)
        {
            var expectedStats = new List<int>(arg.Append(25));
            expectedStats[3] += 2;
            return expectedStats;
        }

        private static IList<int> PrepareHalfElfAbilityScoresAndSpeed(IList<int> arg)
        {
            var expectedStats = new List<int>(arg.Append(30));
            expectedStats[5] += 2;
            return expectedStats;
        }

        private static IList<int> PrepareHalflingAbilityScoresAndSpeed(IList<int> arg)
        {
            var expectedStats = new List<int>(arg.Append(25));
            expectedStats[1] += 2;
            return expectedStats;
        }

        private static IList<int> PrepareHalfOrcAbilityScoresAndSpeed(IList<int> arg)
        {
            var expectedStats = new List<int>(arg.Append(30));
            expectedStats[0] += 2;
            expectedStats[2] += 1;
            return expectedStats;
        }

        private static IList<int> PrepareHumanAbilityScoresAndSpeed(IList<int> arg) =>
            new List<int>(arg.Select(stat => stat + 1).Append(30));

        private static IList<int> PrepareTieflingAbilityScoresAndSpeed(IList<int> arg)
        {
            var expectedStats = new List<int>(arg.Append(30));
            expectedStats[5] += 2;
            expectedStats[3] += 1;
            return expectedStats;
        }
    }
}