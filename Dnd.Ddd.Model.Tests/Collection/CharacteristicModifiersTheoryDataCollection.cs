using System;
using Dnd.Ddd.Model.Character.Builder.Implementation;
using Xunit;

namespace Dnd.Ddd.Model.Tests.Collection
{
    public class CharacteristicModifiersTheoryDataCollection : TheoryData<int, Action<CharacterBuilder, int>, Func<Character.Character, int>, int>
    {
        public CharacteristicModifiersTheoryDataCollection()
        {
            Add(20, (builder, level) => builder.SetStrength(level), character => character.StrengthModifierValue, 5);
            Add(15, (builder, level) => builder.SetDexterity(level), character => character.DexterityModifierValue, 2);
            Add(12, (builder, level) => builder.SetConstitution(level), character => character.ConstitutionModifierValue, 1);
            Add(10, (builder, level) => builder.SetWisdom(level), character => character.WisdomModifierValue, 0);
            Add(5, (builder, level) => builder.SetIntelligence(level), character => character.IntelligenceModifierValue, -3);
            Add(4, (builder, level) => builder.SetCharisma(level), character => character.CharismaModifierValue, -3);
        }
    }
}
