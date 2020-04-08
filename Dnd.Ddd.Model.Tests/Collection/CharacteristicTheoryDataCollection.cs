using System;

using Dnd.Ddd.Model.Character.Builder.Implementation;

using Xunit;

namespace Dnd.Ddd.Model.Tests.Collection
{
    public class CharacteristicTheoryDataCollection : TheoryData<int, Action<CharacterBuilder, int>, Func<Character.Character, int>>
    {
        public CharacteristicTheoryDataCollection()
        {
            Add(20, (builder, level) => builder.SetStrength(level), character => character.StrengthValue);
            Add(15, (builder, level) => builder.SetDexterity(level), character => character.DexterityValue);
            Add(12, (builder, level) => builder.SetConstitution(level), character => character.ConstitutionValue);
            Add(10, (builder, level) => builder.SetWisdom(level), character => character.WisdomValue);
            Add(5, (builder, level) => builder.SetIntelligence(level), character => character.IntelligenceValue);
            Add(14, (builder, level) => builder.SetCharisma(level), character => character.CharismaValue);
        }
    }
}