using System.Threading.Tasks;

namespace Dnd.Ddd.Common.Infrastructure.UnitOfWork
{
    /// <summary>
    ///     Abstraction over transaction.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        ///     Commits changes.
        /// </summary>
        void Commit();

        /// <summary>
        ///     Commits changes asynchronously.
        /// </summary>
        /// <returns><see cref="Task"/> representing result of an asynchronous operation.</returns>
        Task CommitAsync();

        /// <summary>
        ///     Rolls back changes.
        /// </summary>
        void Rollback();

        /// <summary>
        ///     Rolls back changes asynchronously.
        /// </summary>
        /// <returns><see cref="Task"/> representing resul;t of an asynchronous operation.</returns>
        Task RollbackAsync();
    }
}