using System;
using System.Collections.Generic;
using webproject2.Helper;

namespace webproject2.Model.repositories
{
    public class predictionrepository :  RepositoryBase<Prediction>
    {
       public predictionrepository(deeblearningdbContext _ctx): base(_ctx) {
            this.db=_ctx;
        } 

        
        // public IList<Projectviewmodel>  getprojects(int projecttype){

        //  IList<Projectviewmodel> someTypeList = new List<Projectviewmodel>();
        //    using(var context = new DB_A66DAB_accountingsheetContext())
        //    {
        //                  someTypeList = context.LoadStoredProc("dbo.getprojects")
        //                   .WithSqlParam("projecttype",projecttype)
        //                  // .WithSqlParam("anotherparamname",projecttype)
        //                   .ExecuteStoredProc<Projectviewmodel>();
        //    }

        //     //  var param = new SqlParameter[] {
        //     //             new SqlParameter() {
        //     //                 ParameterName = "@projecttype",
        //     //                 SqlDbType =  System.Data.SqlDbType.Int,
        //     //                 Direction = System.Data.ParameterDirection.Input,
        //     //                 Value = projecttype
        //     //             }};      
        //    // var list=dbhelper.RawSqlQuery("exec getprojects @projecttype=4",new Projectviewmodel());
        //     // db.Database.SqlQuery
        //     return someTypeList;
        //  }


        //   public Projectviewmodel  getprojectdetail(int projectid){

        //  Projectviewmodel _Projectviewmodel = new Projectviewmodel();
        //    using(var context = new DB_A66DAB_accountingsheetContext())
        //    {
        //                  _Projectviewmodel = context.LoadStoredProc("dbo.getprojectdetail")
        //                  // .WithSqlParam("projecttype",projecttype)
        //                   .WithSqlParam("projectid",projectid)
        //                   .ExecuteStoredProcFirst<Projectviewmodel>();
        //    }
        //     return _Projectviewmodel;
        //  }

        



    }
}
