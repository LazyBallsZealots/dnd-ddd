using System;
using System.Threading.Tasks;

using Dnd.Ddd.Common.Infrastructure.UnitOfWork;

using NHibernate;

using IsolationLevel = System.Data.IsolationLevel;

namespace Dnd.Ddd.Infrastructure.Database.UnitOfWork
{
    public class NHibernateUnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ITransaction transaction;

        public NHibernateUnitOfWork(ISession session)
        {
            Session = session;
            transaction = session.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        internal ISession Session { get; }

        public void Dispose()
        {
            if (transaction.IsActive && !transaction.WasCommitted && !transaction.WasRolledBack)
            {
                transaction.Rollback();
            }

            transaction?.Dispose();

            Session.Dispose();
        }

        public void Commit()
        {
            try
            {
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task CommitAsync()
        {
            try
            {
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public void Rollback()
        {
            if (transaction.IsActive && !transaction.WasRolledBack)
            {
                transaction.Rollback();
            }
        }

        public async Task RollbackAsync() => await transaction.RollbackAsync();
    }
}