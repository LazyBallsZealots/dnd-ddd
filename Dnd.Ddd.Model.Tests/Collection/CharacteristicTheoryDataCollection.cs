using System;

using Dnd.Ddd.Model.Character.Builder.Implementation;

using Xunit;

namespace Dnd.Ddd.Model.Tests.Collection
{
    public class CharacteristicTheoryDataCollection : TheoryData<int, Action<CharacterBuilder, int>, Func<Character.Character, bool>>
    {
        public CharacteristicTheoryDataCollection()
        {
            Add(
                20,
                (builder, level) => builder.SetStrength(level),
                character => character.StrengthModifierValue.Equals(5) && character.StrengthValue.Equals(20));
            Add(
                15,
                (builder, level) => builder.SetDexterity(level),
                character => character.DexterityModifierValue.Equals(2) && character.DexterityValue.Equals(15));
            Add(
                12,
                (builder, level) => builder.SetConstitution(level),
                character => character.ConstitutionModifierValue.Equals(1) && character.ConstitutionValue.Equals(12));
            Add(
                10,
                (builder, level) => builder.SetWisdom(level),
                character => character.WisdomModifierValue.Equals(0) && character.WisdomValue.Equals(10));
            Add(
                5,
                (builder, level) => builder.SetIntelligence(level),
                character => character.IntelligenceModifierValue.Equals(-3) && character.IntelligenceValue.Equals(5));
            Add(
                4,
                (builder, level) => builder.SetCharisma(level),
                character => character.CharismaModifierValue.Equals(-3) && character.CharismaValue.Equals(4));
        }
    }
}