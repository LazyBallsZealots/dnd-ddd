namespace Dnd.Ddd.Common.ModelFramework
{
    public abstract class ValueObject<T> where T : ValueObject<T>
    {
        public static bool operator ==(ValueObject<T> first, ValueObject<T> second)
        {
            if (first is null && second is null)
            {
                return true;
            }

            if (first is null || second is null)
            {
                return false;
            }

            return ReferenceEquals(first, second) || first.Equals(second);
        }

        public static bool operator !=(ValueObject<T> first, ValueObject<T> second) => !(first == second);

        public override bool Equals(object obj) => obj is ValueObject<T> && InternalEquals((T)obj);

        public override int GetHashCode() => InternalGetHashCode();

        protected abstract bool InternalEquals(T valueObject);

        protected abstract int InternalGetHashCode();
    }
}