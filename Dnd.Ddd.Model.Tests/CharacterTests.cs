using System;
using System.Collections.Generic;

using Dnd.Ddd.Model.Tests.Collection;

using Xunit;

namespace Dnd.Ddd.Model.Tests
{
    [Trait("Category", CharacterTestsCategory)]
    public class CharacterTests
    {
        public static readonly RaceTheoryDataCollection RacesData = new RaceTheoryDataCollection();

        private const string CharacterTestsCategory = "Unit tests: Character";

        [Theory, MemberData(nameof(RacesData))]
        public void Character_OnCompletingCharacter_HasCorrectRaceSetIfAbilityScoresAreSet(
            string race,
            IList<int> abilityScores,
            Func<Character.Character, bool> testResult)
        {
            const string Name = "Argh";

            var character = Character.Character.ForPlayer(Guid.NewGuid()).SetStrength(abilityScores[0])
                .SetDexterity(abilityScores[1])
                .SetConstitution(abilityScores[2])
                .SetIntelligence(abilityScores[3])
                .SetWisdom(abilityScores[4])
                .SetCharisma(abilityScores[5]);

            character.SetName(Name);
            character.SetRace(race);

            Assert.True(testResult(character));
            Assert.Equal(character.Name, Character.ValueObjects.Name.FromString(Name));
        }
    }
}