using System;

using Dnd.Ddd.Model.Character.Builder.Implementation;
using Dnd.Ddd.Model.Tests.Collection;

using Xunit;

namespace Dnd.Ddd.Model.Tests
{
    [Trait("Category", CharacterTestsCategory)]
    public class CharacterTests : IDisposable
    {
        public const string CharacterTestsCategory = "Unit tests: character";

        public static readonly CharacteristicTheoryDataCollection TheoryData = new CharacteristicTheoryDataCollection();

        private CharacterBuilder characterBuilder;

        public CharacterTests()
        {
            characterBuilder = new CharacterBuilder();
        }

        public void Dispose() => characterBuilder = null;

        [Theory, MemberData(nameof(TheoryData))]
        public void Characteristic_OnSettingValue_ReturnsCorrectlyDefinedLevel(
            int characteristicLevel,
            Action<CharacterBuilder, int> characteristicSetter,
            Func<Character.Character, int> characteristicSelector)
        {
            characteristicSetter(characterBuilder, characteristicLevel);

            Assert.Equal(characteristicLevel, characteristicSelector(characterBuilder.Build()));
        }

        [Fact]
        public void Character_OnSettingName_HasCorrectNameAssigned()
        {
            const string Name = "Yrel";

            _ = characterBuilder.Named(Name);

            Assert.Equal(Name, characterBuilder.Build().CharacterName);
        }
    }
}