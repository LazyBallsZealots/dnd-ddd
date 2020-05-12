using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dnd.Ddd.CharacterCreation.Api.Controllers.Character.ChooseName
{
    public class ChooseNameRequest
    {
        public Guid DraftId { get; set; }

        public string Name { get; set; }
    }
}
