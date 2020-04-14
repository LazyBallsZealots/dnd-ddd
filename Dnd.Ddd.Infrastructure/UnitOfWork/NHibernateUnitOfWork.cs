using System;
using System.Threading.Tasks;

using Dnd.Ddd.Infrastructure.UnitOfWork.Contract;

using NHibernate;

namespace Dnd.Ddd.Infrastructure.UnitOfWork
{
    internal class NHibernateUnitOfWork : INHibernateUnitOfWork, IDisposable
    {
        private readonly ITransaction transaction;

        public NHibernateUnitOfWork(ISessionFactory sessionFactory)
        {
            Session = sessionFactory.OpenSession();
            transaction = Session.BeginTransaction();
        }

        public ISession Session { get; }

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
            Session?.Dispose();
        }
    }
}