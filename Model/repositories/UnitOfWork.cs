
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
namespace webproject2.Model.repositories
{

public class UnitOfWork : IDisposable
    {
        private deeblearningdbContext _ctx = new deeblearningdbContext();
        

        public dbnrepository dbnrepository
        {
            get
            {
                return new dbnrepository(_ctx);
            }
        }

         //predictionrepository  
          public predictionrepository predictionrepository
          {
            get
            {
                return new predictionrepository(_ctx);
            }
          }
           
        public int Commit()
        {
            return _ctx.SaveChanges();
        }
        public void Dispose()
        {
            GC.SuppressFinalize(_ctx);
        }

        internal void Update<T>(T entity) where T : class
        {
            _ctx.Entry<T>(entity).State = EntityState.Modified;       
        }
    }

}