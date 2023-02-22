using FOS.DataLayer;
using FOS.Setup;
using Shared.Diagnostics.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace FOS.Web.UI.Controllers.API
{
    public class MyBillingListController : ApiController
    {
        FOSDataModel db = new FOSDataModel();

        public IHttpActionResult Get(int SOID,int BlockID)
        {
            FOSDataModel dbContext = new FOSDataModel();
            try
            {
                DateTime dtFromTodayUtc = DateTime.UtcNow.AddHours(5);

                DateTime dtFromToday = dtFromTodayUtc.Date;
                DateTime dtToToday = dtFromToday.AddDays(1);

                if (SOID > 0)
                {
                    object[] param = { SOID };
                    
                    
                        var result = dbContext.Sp_MyBillingList(SOID,dtFromToday,dtToToday,BlockID).ToList();
                    
                    if (result != null && result.Count > 0)
                    {
                        return Ok(new
                        {
                            MyReadingList = result
                            
                        });
                    }
                 
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(ex, "VisitDetailController GET API Failed");
            }
            object[] paramm = {};
            return Ok(new
            {
                MyReadingList = paramm
            });

        }


    }
}