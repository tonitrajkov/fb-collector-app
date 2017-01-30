using System.Data;
using NHibernate;

namespace NHibernateCfg
{
    public class NhUnitOfWork : IUnitOfWork
    {
        private ISession _session;
        private ITransaction _transaction;

        public ISession Session
        {
            get
            {
                return this._session;
            }
        }

        public NhUnitOfWork() { }

        public void OpenSession()
        {
            if (this._session == null || !this._session.IsConnected)
            {
                if (this._session != null)
                    this._session.Dispose();

                this._session = SessionFactory.GetNewSession();
            }
        }

        public void BeginTransation(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            if (this._transaction == null || !this._transaction.IsActive)
            {
                if (this._transaction != null)
                    this._transaction.Dispose();

                this._transaction = this._session.BeginTransaction(isolationLevel);
            }
        }

        public void Commit()
        {
            try
            {
                this._transaction.Commit();
            }
            catch
            {
                this._transaction.Rollback();
                throw;
            }
        }

        public void Rollback()
        {
            this._transaction.Rollback();
        }

        public void Dispose()
        {
            if (this._transaction != null)
            {
                this._transaction.Dispose();
                this._transaction = null;
            }

            if (this._session != null)
            {
                this._session.Dispose();
                _session = null;
            }
        }
    }
}
