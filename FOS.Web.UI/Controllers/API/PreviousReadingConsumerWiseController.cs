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
    public class PreviousReadingConsumerWiseController : ApiController
    {
        FOSDataModel db = new FOSDataModel();

        public IHttpActionResult Get(int ConsumerID)
        {
            FOSDataModel dbContext = new FOSDataModel();
            try
            {
                if (ConsumerID > 0)
                {
                    object[] param = { ConsumerID };
                    var billingmonth = db.Tbl_IZBillingPeriod.Where(x => x.IsActive == true).FirstOrDefault();

                    var result = db.JobsDetails.Where(x => x.ConsumerID == ConsumerID && x.BillingPeriodID==billingmonth.ID).OrderByDescending(x => x.ID).Select(x => new
                    {
                        ID = x.ID,
                        PreviousReading = x.PreviousReading,
                        PreviosUnits = (x.MeterReading - x.PreviousReading)

                    }
                    ).FirstOrDefault();

                    if (result != null)
                    {
                        return Ok(new
                        {
                            PreviousReading = result

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
                PreviousReading = paramm
            });

        }


    }
}