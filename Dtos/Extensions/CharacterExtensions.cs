using Dnd.Ddd.Model.Character;

namespace Dnd.Ddd.Dtos.Extensions
{
    public static class CharacterExtensions
    {
        public static CharacterDto ToDto(this Character character) =>
            new CharacterDto
            {
                PlayerId = character.PlayerId.PlayerUiD,
                UiD = character.UiD,
                Charisma = character.Charisma?.ToInteger(),
                Constitution = character.Constitution?.ToInteger(),
                Dexterity = character.Dexterity?.ToInteger(),
                Intelligence = character.Intelligence?.ToInteger(),
                Wisdom = character.Wisdom?.ToInteger(),
                Strength = character.Strength?.ToInteger(),
                Race = character.Race?.ToString(),
                Name = character.Name?.ToString(),
                Stage = character.GetType().Name
            };
    }
}
