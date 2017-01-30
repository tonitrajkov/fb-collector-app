using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;

namespace NHibernateCfg
{
    public class NhRepository<TEntity> : IRepository<TEntity>
    {
        readonly NhUnitOfWork _nhUnitOfWork;

        public NhRepository(NhUnitOfWork nhUnitOfWork)
        {
            this._nhUnitOfWork = nhUnitOfWork;
        }

        public TEntity Get(object id)
        {
            this._nhUnitOfWork.OpenSession();
            this._nhUnitOfWork.BeginTransation();
            return this._nhUnitOfWork.Session.Get<TEntity>(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            this._nhUnitOfWork.OpenSession();
            this._nhUnitOfWork.BeginTransation();
            return this._nhUnitOfWork.Session.Query<TEntity>();
        }

        public IQueryable<TEntity> Query()
        {
            this._nhUnitOfWork.OpenSession();
            this._nhUnitOfWork.BeginTransation();
            return (IQueryable<TEntity>)LinqExtensionMethods.Query<TEntity>(this._nhUnitOfWork.Session);
        }

        public void Save(TEntity entity)
        {
            this._nhUnitOfWork.OpenSession();
            this._nhUnitOfWork.BeginTransation();
            this._nhUnitOfWork.Session.Save(entity);
            this._nhUnitOfWork.Commit();
            this._nhUnitOfWork.Session.Flush();
        }

        public void Update(TEntity entity)
        {
            this._nhUnitOfWork.OpenSession();
            this._nhUnitOfWork.BeginTransation();
            this._nhUnitOfWork.Session.Update(entity);
            this._nhUnitOfWork.Commit();
            this._nhUnitOfWork.Session.Flush();
        }

        public void SaveOrUpdate(TEntity entity)
        {
            this._nhUnitOfWork.OpenSession();
            this._nhUnitOfWork.BeginTransation();
            this._nhUnitOfWork.Session.SaveOrUpdate(entity);
            this._nhUnitOfWork.Commit();
            this._nhUnitOfWork.Session.Flush();
        }

        public void Delete(TEntity entity)
        {
            this._nhUnitOfWork.OpenSession();
            this._nhUnitOfWork.BeginTransation();
            this._nhUnitOfWork.Session.Delete(entity);
            this._nhUnitOfWork.Commit();
            this._nhUnitOfWork.Session.Flush();
        }

        public void DeleteById(object id)
        {
            this._nhUnitOfWork.OpenSession();
            this._nhUnitOfWork.BeginTransation();
            this._nhUnitOfWork.Session.CreateQuery(string.Format("delete {0} where id = :id", (object)typeof(TEntity))).SetParameter("id", id).ExecuteUpdate();
            this._nhUnitOfWork.Commit();
            this._nhUnitOfWork.Session.Flush();
        }
    }
}
