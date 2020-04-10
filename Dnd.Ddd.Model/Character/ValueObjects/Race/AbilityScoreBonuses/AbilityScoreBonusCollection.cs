using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Dnd.Ddd.Model.Character.ValueObjects.Race.AbilityScoreBonuses
{
    internal class AbilityScoreBonusCollection : IReadOnlyCollection<AbilityScoreBonus>
    {
        private readonly IReadOnlyCollection<AbilityScoreBonus> abilityScoreModifiers;

        public AbilityScoreBonusCollection(IEnumerable<AbilityScoreBonus> abilityScoreModifiers)
        {
            this.abilityScoreModifiers = abilityScoreModifiers.ToList().AsReadOnly();
        }

        public int Count => abilityScoreModifiers.Count;

        public IEnumerator<AbilityScoreBonus> GetEnumerator() => abilityScoreModifiers.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}