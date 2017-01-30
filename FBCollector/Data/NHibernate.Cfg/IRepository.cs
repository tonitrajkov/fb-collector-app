using System.Collections.Generic;
using System.Linq;

namespace NHibernateCfg
{
    public interface IRepository<TEntity>
    {
        TEntity Get(object id);

        IEnumerable<TEntity> GetAll();

        IQueryable<TEntity> Query();

        void Save(TEntity entity);

        void Update(TEntity entity);

        void SaveOrUpdate(TEntity entity);

        void Delete(TEntity entity);

        void DeleteById(object id);
    }
}
