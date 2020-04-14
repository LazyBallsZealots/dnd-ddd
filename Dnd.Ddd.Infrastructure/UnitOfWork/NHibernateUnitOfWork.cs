using System;
using System.Threading.Tasks;

using Dnd.Ddd.Common.Infrastructure.UnitOfWork;

using NHibernate;

namespace Dnd.Ddd.Infrastructure.UnitOfWork
{
    public class NHibernateUnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ITransaction transaction;

        public NHibernateUnitOfWork(ISession session)
        {
            transaction = session.BeginTransaction();
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

        public void Dispose()
        {
            if (transaction.IsActive)
            {
                transaction.Rollback();
            }

            transaction?.Dispose();
        }
    }
}