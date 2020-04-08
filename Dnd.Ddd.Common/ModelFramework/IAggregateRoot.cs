namespace Dnd.Ddd.Common.ModelFramework
{
    /// <summary>
    ///     Defines necessary contract to be fulfilled by all aggregates.
    /// </summary>
    public interface IAggregateRoot
    {
        /// <summary>
        ///     Used for soft-deletion of any aggregate.
        /// </summary>
        bool Valid { get; }
    }
}