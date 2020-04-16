using System.Threading.Tasks;

namespace Dnd.Ddd.Common.ModelFramework
{
    public abstract class Saga : Entity, IAggregateRoot
    {
        public abstract bool IsComplete { get; }

        protected virtual void CheckForCompletion()
        {
            if (!IsComplete)
            {
            }
            else
            {
                Complete();
            }
        }

        protected abstract void Complete();
    }
}