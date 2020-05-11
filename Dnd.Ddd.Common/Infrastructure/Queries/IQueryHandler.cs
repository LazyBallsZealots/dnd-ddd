namespace Dnd.Ddd.Common.Infrastructure.Queries
{
    /// <summary>
    ///     Base contract for query handlers.
    /// </summary>
    /// <typeparam name="TQuery">Type of handled query.</typeparam>
    /// <typeparam name="TResult">Returned query result.</typeparam>
    public interface IQueryHandler<in TQuery, out TResult> where TQuery : BaseQuery<TResult>
    {
        /// <summary>
        ///     Handles given <paramref name="query" />.
        /// </summary>
        /// <param name="query"><typeparamref name="TQuery" /> instance to handle.</param>
        /// <returns><see cref="TResult" /> instance representing query result.</returns>
        TResult Handle(TQuery query);
    }
}