
using System.Data.Common;
namespace webproject2.Model.repositories
{
interface IRepository<TEntity>
    {
//        IQueryable<TEntity> GetAll();
        TEntity GetById(params object[] id);
        void Add(TEntity entity);
        void Update(TEntity entity,int key);
        void Delete(params object[] id);

    }

}