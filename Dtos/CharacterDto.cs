using System;
using System.Collections.Generic;
using Dnd.Ddd.Common.ModelFramework;

namespace Dtos
{
    [Serializable]
    public class CharacterDto
    {
        public Guid PlayerId { get; set; }

        public Guid UiD { get; set; }

        public string Name { get; set; }
  
        public int? Strength { get; set; }

        public int? Dexterity { get; set; }

        public int? Constitution { get; set; }

        public int? Charisma { get; set; }

        public int? Intelligence { get; set; }

        public int? Wisdom { get; set; }

        public string Race { get; set; }

        public bool IsDeleted { get; set; }

        public long Version { get; set; }

        public string Stage { get; set; }
    }
}
