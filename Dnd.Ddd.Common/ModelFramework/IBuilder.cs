namespace Dnd.Ddd.Common.ModelFramework
{
    public interface IBuilder<out T>
    {
        T Build();
    }
}