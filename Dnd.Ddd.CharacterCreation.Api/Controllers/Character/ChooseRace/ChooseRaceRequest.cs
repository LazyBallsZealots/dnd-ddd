using System;

namespace Dnd.Ddd.CharacterCreation.Api.Controllers.Character.ChooseRace
{
    public class ChooseRaceRequest
    {
        public Guid DraftId { get; set; }

        public string Race { get; set; }
    }
}