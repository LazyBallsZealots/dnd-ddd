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
            Id = id;
        }

        protected internal Guid Id { get; private set; }

        public static PlayerId FromUiD(Guid id) => new PlayerId(id);

        protected override bool InternalEquals(PlayerId valueObject) => valueObject.Id == Id;

        protected override int InternalGetHashCode() => HashCode.Combine(GetType(), Id);
    }
}