using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using webproject2.Model;
using webproject2.Model.repositories;
using Microsoft.EntityFrameworkCore;
using System.Web;
namespace webproject2.Model.repositories
{
    public class BaseRepository 
    {
        protected readonly deeblearningdbContext db;    

        public BaseRepository(){
              deeblearningdbContext _db=new deeblearningdbContext();
             db=_db;
        }
        public void Save()
        {
            db.SaveChanges();           
        }
       
       
    }
}
