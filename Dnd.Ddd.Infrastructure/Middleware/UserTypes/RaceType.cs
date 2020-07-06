using Dnd.Ddd.Infrastructure.Database.Middleware.UserTypes.EnumBase;
using Dnd.Ddd.Model.Character.ValueObjects.Race;

namespace Dnd.Ddd.Infrastructure.Database.Middleware.UserTypes
{
    internal class RaceType : StringEnumType<Races, Race>
    {
        public RaceType()
            : base(
                  raceEnum => Race.FromEnumeration(raceEnum),
                  race => race.RaceName)
        {
        }

        public override bool IsMutable => true;
    }
}