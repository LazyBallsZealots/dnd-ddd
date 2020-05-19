using Dnd.Ddd.Infrastructure.Database.Middleware.UserTypes.EnumBase;
using Dnd.Ddd.Model.Character.CharacterStates;
using Dnd.Ddd.Model.Character.CharacterStates.Contract;

namespace Dnd.Ddd.Infrastructure.Database.Middleware.UserTypes
{
    internal class CharacterStateType : StringEnumType<CharacterStates, CharacterState>
    {
        public override bool IsMutable => false;

        public CharacterStateType()
            : base(
                  stateEnum => CharacterState.FromEnumeration(stateEnum),
                  state => state.GetType().Name)
        {
        }
    }
}
