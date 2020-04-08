namespace Dnd.Ddd.Common.Infrastructure.Commands
{
    /// <summary>
    ///     Base contract for all objects handling commands.
    /// </summary>
    /// <typeparam name="TCommand">Type of command to handle.</typeparam>
    public interface ICommandHandler<in TCommand>
        where TCommand : BaseCommand
    {
    }
}