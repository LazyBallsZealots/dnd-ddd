using Dnd.Ddd.Infrastructure.Database.Middleware.UserTypes.EnumBase;
using Dnd.Ddd.Model.Character.CharacterStates;
using Dnd.Ddd.Model.Character.CharacterStates.Contract;

namespace Dnd.Ddd.Infrastructure.Database.Middleware.UserTypes
{
    internal class CharacterStateType : StringEnumType<CharacterStates, CharacterState>
    {
        public CharacterStateType()
            : base(
                  stateEnum => CharacterState.FromEnumeration(stateEnum),
                  state => state.GetType().Name)
        {
        }

        public override bool IsMutable => true;
    }
}
