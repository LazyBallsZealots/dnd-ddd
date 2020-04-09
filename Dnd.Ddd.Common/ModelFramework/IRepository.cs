using System;
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
    public interface IRepository<TAggregate, in TAggregateId>
        where TAggregate : Entity, IAggregateRoot
    {
        /// <summary>
        ///     Saves new <typeparamref name="TAggregate" />.
        /// </summary>
        /// <param name="aggregate">Aggregate to save.</param>
        void Save(TAggregate aggregate);

        /// <summary>
        ///     Saves new <typeparamref name="TAggregate" /> asynchronously.
        /// </summary>
        /// <param name="aggregate">Aggregate to save.</param>
        /// <returns><see cref="Task" /> representing result of an asynchronous 'save' operation.</returns>
        Task SaveAsync(TAggregate aggregate);

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
        /// <returns><see cref="Nullable" /> representing result of an asynchronous 'get' operation.</returns>
        Task<TAggregate> GetAsync(TAggregateId id);

        /// <summary>
        ///     Updates an existing <paramref name="aggregate" />.
        /// </summary>
        /// <param name="aggregate"><typeparamref name="TAggregate" /> instance to update.</param>
        void Update(TAggregate aggregate);

        /// <summary>
        ///     Updates an existing <paramref name="aggregate" /> asynchronously.
        /// </summary>
        /// <param name="aggregate"><typeparamref name="TAggregate" /> to update.</param>
        /// <returns><see cref="Task" /> representing the result of an asynchronous 'update' operation</returns>
        Task UpdateAsync(TAggregate aggregate);

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
        /// <returns><see cref="Task" /> representing the result of an asnychronous operation.</returns>
        Task DeleteAsync(TAggregate aggregate);
    }
}