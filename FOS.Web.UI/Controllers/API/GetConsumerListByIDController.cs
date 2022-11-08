using FOS.DataLayer;
using FOS.Setup;
using Shared.Diagnostics.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace FOS.Web.UI.Controllers.API
{
    public class GetConsumerListByIDController : ApiController
    {

        FOSDataModel db = new FOSDataModel();

        public IHttpActionResult Get(int ID)
        {
            FOSDataModel dbContext = new FOSDataModel();
            try
            {
                //List<RetailerData> MAinCat = new List<RetailerData>();

                //  object[] param = { RegionID };

                // RetailerData cty;

                var result = dbContext.Tbl_IZConsumers.Where(x=>x.ID==ID).Select(x => new
                {
                    ID = x.ID,
                    Name = x.OwnerName,
                    RefNo=x.RefNo,
                    PlotNo=x.PlotNo,
                    MeterNo=x.MeterNo,
                    Metertype=x.MeterType,
                    Phase=x.Phase,
                    TeriffCode=x.Teriff,
                    ConnectionType=x.ConnectionType,
                    Address=x.Address,
                    BlockID=x.BlockID

                }).FirstOrDefault();

                if (result != null )
                    {
                        return Ok(new
                        {
                            ConsumerListByID = result

                        });
                    }

               

            }
            catch (Exception ex)
            {
                Log.Instance.Error(ex, "VisitDetailController GET API Failed");
            }
            object[] paramm = { };
            return Ok(new
            {
                ConsumerListByID = paramm
            });

        }


    }
}