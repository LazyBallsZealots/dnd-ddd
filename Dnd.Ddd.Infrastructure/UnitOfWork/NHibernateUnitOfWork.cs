using System;
using System.Threading.Tasks;

using Dnd.Ddd.Common.Infrastructure.UnitOfWork;

using NHibernate;

namespace Dnd.Ddd.Infrastructure.Database.UnitOfWork
{
    public class NHibernateUnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ISession session;

        private readonly ITransaction transaction;

        public NHibernateUnitOfWork(ISession session)
        {
            this.session = session;
            transaction = session.BeginTransaction();
        }

        public void Dispose()
        {
            session.Dispose();
            if (transaction.IsActive || !transaction.WasCommitted)
            {
                transaction.Rollback();
            }

            transaction?.Dispose();
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

        public void Rollback() => transaction.Rollback();

        public async Task RollbackAsync() => await transaction.RollbackAsync();
    }
}