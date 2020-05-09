using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnd.Ddd.CharacterCreation.Api.Controllers.Character.AddAbilities
{
    public class RollAbilityScoresRequest
    {
        public Guid DraftId { get; set; }

        public int Strength { get; set; }

        public int Wisdom { get; set; }

        public int Dexterity { get; set; }

        public int Intelligence { get; set; }

        public int Charisma { get; set; }

        public int Constitution { get; set; }
    }
}
