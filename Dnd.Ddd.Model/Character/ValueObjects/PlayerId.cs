using System;

using Dnd.Ddd.Common.ModelFramework;

namespace Dnd.Ddd.Model.Character.ValueObjects
{
    internal class PlayerId : ValueObject<PlayerId>
    {
        protected PlayerId()
        {
        }

        private PlayerId(Guid id)
        {
            PlayerUiD = id;
        }

        protected internal Guid PlayerUiD { get; private set; }

        public static PlayerId FromUiD(Guid id) => new PlayerId(id);

        protected override bool InternalEquals(PlayerId valueObject) => valueObject.PlayerUiD == PlayerUiD;

        protected override int InternalGetHashCode() => HashCode.Combine(GetType(), PlayerUiD);
    }
}