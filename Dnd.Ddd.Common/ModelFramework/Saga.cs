using System.Threading.Tasks;

namespace Dnd.Ddd.Common.ModelFramework
{
    public abstract class Saga : Entity, IAggregateRoot
    {
        public abstract bool IsComplete { get; }

        protected virtual async Task CheckForCompletion()
        {
            if (!IsComplete)
            {
            }
            else
            {
                await Complete();
            }
        }

        protected abstract Task Complete();
    }
}