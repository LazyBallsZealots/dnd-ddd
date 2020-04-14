using Dnd.Ddd.Common.Infrastructure.UnitOfWork;

using NHibernate;

namespace Dnd.Ddd.Infrastructure.UnitOfWork.Contract
{
    public interface INHibernateUnitOfWork : IUnitOfWork
    {
        ISession Session { get; }
    }
}