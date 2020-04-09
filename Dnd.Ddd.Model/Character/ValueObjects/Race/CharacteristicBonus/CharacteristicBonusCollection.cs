using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Dnd.Ddd.Model.Character.ValueObjects.Race.CharacteristicBonus
{
    internal class CharacteristicBonusCollection : IReadOnlyCollection<CharacteristicBonus>
    {
        private readonly IReadOnlyCollection<CharacteristicBonus> characteristicModifiers;

        public CharacteristicBonusCollection(IEnumerable<CharacteristicBonus> characteristicModifiers)
        {
            this.characteristicModifiers = characteristicModifiers.ToList().AsReadOnly();
        }

        public int Count => characteristicModifiers.Count;

        public IEnumerator<CharacteristicBonus> GetEnumerator() => characteristicModifiers.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}