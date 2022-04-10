using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webproject2.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Data.Common;
namespace webproject2.Model.repositories
{

public class RepositoryBase<T> : IRepository<T> where T : class
    {
        //private Model1 _ctx;
        private UnitOfWork unitOfWork = new UnitOfWork();
        protected  DbSet<T> _set;
        protected  deeblearningdbContext db;    
        
        public RepositoryBase(deeblearningdbContext _ctx)
        {
            // _ctx = new Model1();
            _set = _ctx.Set<T>();
             db=_ctx;
        }

        public IQueryable<T> GetAll()
        {
            return _set;
        }

        public T GetById(params object[] id)
        {
            return _set.Find(id);
        }

        public void Add(T entity)
        {
           _set.Add(entity);

        }

       //  public void Update(T entity,UnitOfWork uow)
       //  {
       //        unitOfWork.Update(entity);       
       //  }
         public void Update(T entity,int key)
         {
           // unitOfWork.Update(entity);
              //var key = GetKeyValue(entity);
              var originalEntity = _set.Find(key);
              db.Entry(originalEntity).CurrentValues.SetValues(entity); 
         }

        public void Delete(params object[] id)
        {
            _set.Remove(_set.Find(id));
        }


         public object GetKeyValue(T t)
         {
             var key = typeof(T).GetProperties().FirstOrDefault(
                     p => p.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.KeyAttribute), true).Length != 0);
             return (key != null) ? key.GetValue(t, null) : null;

         }

        


    }
}