using System;
using System.Collections.Generic;

using Dnd.Ddd.Model.Character.ValueObjects.Race.AbilityScoreBonuses;

namespace Dnd.Ddd.Model.Character.CharacterStates.Collections
{
    internal class AbilityScoreIncreasesCollection
    {
        private readonly IReadOnlyDictionary<string, Action<AbilityScoreBonus>> abilityScoreIncreases;

        private readonly Character character;

        public AbilityScoreIncreasesCollection(Character character)
        {
            this.character = character;
            abilityScoreIncreases = new Dictionary<string, Action<AbilityScoreBonus>>
            {
                [nameof(Character.Strength)] = bonus => this.character.Strength = this.character.Strength.Raise(bonus.AbilityScoreModifierLevel),
                [nameof(Character.Dexterity)] = bonus => this.character.Dexterity = this.character.Dexterity.Raise(bonus.AbilityScoreModifierLevel),
                [nameof(Character.Constitution)] = bonus => this.character.Constitution = this.character.Constitution.Raise(bonus.AbilityScoreModifierLevel),
                [nameof(Character.Intelligence)] = bonus => this.character.Intelligence = this.character.Intelligence.Raise(bonus.AbilityScoreModifierLevel),
                [nameof(Character.Wisdom)] = bonus => this.character.Wisdom = this.character.Wisdom.Raise(bonus.AbilityScoreModifierLevel),
                [nameof(Character.Charisma)] = bonus => this.character.Charisma = this.character.Charisma.Raise(bonus.AbilityScoreModifierLevel)
            };
        }

        public void IncreaseAbilityScore(AbilityScoreBonus bonus) => abilityScoreIncreases[bonus.AbilityScoreName](bonus);
    }
}
