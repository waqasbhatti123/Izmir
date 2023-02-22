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
    public class GetConsumerListController : ApiController
    {

        FOSDataModel db = new FOSDataModel();

        public IHttpActionResult Get(int BlockID)
        {
            FOSDataModel dbContext = new FOSDataModel();
            try
            {
                //List<RetailerData> MAinCat = new List<RetailerData>();

                //  object[] param = { RegionID };

                // RetailerData cty;

                var result = dbContext.Tbl_IZConsumers.Where(x=>x.BlockID==BlockID).Select(x => new
                {
                    ID = x.ID,
                    Name = x.Address+ "/"+ "" + x.MeterNo,
                 
                }).ToList();

                if (result != null )
                    {
                        return Ok(new
                        {
                            ConsumerDetail = result

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
                ConsumerDetail = paramm
            });

        }


    }
}