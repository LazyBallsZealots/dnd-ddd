using System;

namespace Dnd.Ddd.Common.Infrastructure.Commands
{
    /// <summary>
    ///     Base contract for command handlers which return unique object identifier.
    /// </summary>
    /// <typeparam name="TCommand">Type of the command.</typeparam>
    public interface IIdResultCommandHandler<in TCommand> : ICommandHandler<TCommand>
        where TCommand : BaseCommand
    {
        /// <summary>
        ///     Handles given <paramref name="command" />.
        /// </summary>
        /// <param name="command"><typeparamref name="TCommand" /> instance to handle.</param>
        /// <returns>Unique object identifier, resulting from command execution.</returns>
        Guid Handle(TCommand command);
    }
}