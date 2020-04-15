using System;

using Dnd.Ddd.Common.ModelFramework;

namespace Dnd.Ddd.Model.Character.ValueObjects
{
    internal class CreatorId : ValueObject<CreatorId>
    {
        private readonly Guid id;

        private CreatorId(Guid id)
        {
            this.id = id;
        }

        public static CreatorId FromUiD(Guid id) => new CreatorId(id);

        public Guid ToUiD() => id;

        protected override bool InternalEquals(CreatorId valueObject) => valueObject.id == id;

        protected override int InternalGetHashCode() => HashCode.Combine(GetType(), id);
    }
}