namespace Dnd.Ddd.Common.Infrastructure.Commands
{
    /// <summary>
    ///     Contract for command handlers which do not return any value.
    /// </summary>
    /// <typeparam name="TCommand">Type of command to handle.</typeparam>
    public interface IEmptyResultCommandHandler<in TCommand> : ICommandHandler<TCommand> where TCommand : BaseCommand
    {
        /// <summary>
        ///     Handles given command.
        /// </summary>
        /// <param name="command"><typeparamref name="TCommand" /> instance to handle.</param>
        void Handle(TCommand command);
    }
}