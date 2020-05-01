using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dnd.Ddd.Common.ModelFramework
{
    /// <summary>
    ///     Contract for data-access objects for given
    ///     <typeparam name="TAggregate" />
    ///     .
    /// </summary>
    /// <typeparam name="TAggregate">Type of aggregate to perform data access operations on.</typeparam>
    /// <typeparam name="TAggregateId">Type of property used for database entry identification.</typeparam>
    public interface IRepository<TAggregate, TAggregateId> where TAggregate : Entity, IAggregateRoot
    {
        /// <summary>
        ///     Saves new <typeparamref name="TAggregate" />.
        /// </summary>
        /// <param name="aggregate">Aggregate to save.</param>
        TAggregateId Save(TAggregate aggregate);

        /// <summary>
        ///     Saves new <typeparamref name="TAggregate" /> asynchronously.
        /// </summary>
        /// <param name="aggregate">Aggregate to save.</param>
        /// <param name="token">Cancellation token</param>
        /// <returns><see cref="Task" /> representing result of an asynchronous 'save' operation.</returns>
        Task<TAggregateId> SaveAsync(TAggregate aggregate, CancellationToken token = default);

        /// <summary>
        ///     Returns <typeparamref name="TAggregate" /> instance associated with provided <paramref name="id" />.
        /// </summary>
        /// <param name="id">Id of the aggregate to return.</param>
        /// <returns><typeparamref name="TAggregate" /> instance associated with provided <paramref name="id" />.</returns>
        TAggregate Get(TAggregateId id);

        /// <summary>
        ///     Returns <typeparamref name="TAggregate" /> instance associated with provided <paramref name="id" /> asynchronously.
        /// </summary>
        /// <param name="id">Id of the aggregate to return.</param>
        /// <param name="token">Cancellation token</param>
        /// <returns><see cref="Nullable" /> representing result of an asynchronous 'get' operation.</returns>
        Task<TAggregate> GetAsync(TAggregateId id, CancellationToken token = default);

        /// <summary>
        ///     Updates an existing <paramref name="aggregate" />.
        /// </summary>
        /// <param name="aggregate"><typeparamref name="TAggregate" /> instance to update.</param>
        void Update(TAggregate aggregate);

        /// <summary>
        ///     Updates an existing <paramref name="aggregate" /> asynchronously.
        /// </summary>
        /// <param name="aggregate"><typeparamref name="TAggregate" /> to update.</param>
        /// <param name="token">Cancellation token</param>
        /// <returns><see cref="Task" /> representing the result of an asynchronous 'update' operation</returns>
        Task UpdateAsync(TAggregate aggregate, CancellationToken token = default);

        /// <summary>
        ///     Deletes an existing <paramref name="aggregate" />.
        /// </summary>
        /// <param name="aggregate"><typeparamref name="TAggregate" /> to remove.</param>
        void Delete(TAggregate aggregate);

        /// <summary>
        ///     Deletes an existing <paramref name="aggregate" /> asynchronously.
        /// </summary>
        /// <param name="aggregate">
        ///     <typeparam name="TAggregate" />
        ///     instance to delete.
        /// </param>
        /// <param name="token">Cancellation token</param>
        /// <returns><see cref="Task" /> representing the result of an asnychronous operation.</returns>
        Task DeleteAsync(TAggregate aggregate, CancellationToken token = default);
    }
}