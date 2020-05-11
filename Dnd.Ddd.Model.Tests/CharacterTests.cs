using System;
using System.Collections.Generic;

using Dnd.Ddd.Model.Character.Builder.Implementation;
using Dnd.Ddd.Model.Character.ValueObjects.Race;
using Dnd.Ddd.Model.Tests.Collection;

using Xunit;

namespace Dnd.Ddd.Model.Tests
{
    [Trait("Category", CharacterTestsCategory)]
    public class CharacterTests : IDisposable
    {
        public static readonly RaceTheoryDataCollection RacesData = new RaceTheoryDataCollection();

        private const string CharacterTestsCategory = "Unit tests: Character";

        private CharacterBuilder characterBuilder;

        public CharacterTests()
        {
            characterBuilder = new CharacterBuilder();
        }

        public void Dispose() => characterBuilder = null;

        [Theory, MemberData(nameof(RacesData))]
        public void Character_OnChoosingRace_HasCorrectRaceSetIfAbilityScoresAreSet(
            Races race,
            IList<int> abilityScores,
            Func<Character.Character, bool> testResult)
        {
            const string Name = "Argh";

            var character = characterBuilder.SetStrength(abilityScores[0])
                .SetDexterity(abilityScores[1])
                .SetConstitution(abilityScores[2])
                .SetIntelligence(abilityScores[3])
                .SetWisdom(abilityScores[4])
                .SetCharisma(abilityScores[5])
                .Named(Name)
                .OfRace(race)
                .Build();

            Assert.True(testResult(character));
            Assert.Equal(character.Name, Character.ValueObjects.Name.FromString(Name));
        }
    }
}