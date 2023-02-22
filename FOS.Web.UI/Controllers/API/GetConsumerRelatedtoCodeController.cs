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
    public class GetConsumerRelatedtoCodeForBanksController : ApiController
    {

        FOSDataModel db = new FOSDataModel();

        public IHttpActionResult Get(int Code)
        {
            FOSDataModel dbContext = new FOSDataModel();
            try
            {
                System.Net.Http.Headers.HttpRequestHeaders headers = this.Request.Headers;
                string token = string.Empty;
                string pwd = string.Empty;
                if (headers.Contains("UserName"))
                {
                    token = headers.GetValues("UserName").First();
                }
                if (headers.Contains("Password"))
                {
                    pwd = headers.GetValues("Password").First();
                }

                var validUser = db.Users.Where(x => x.UserName == token && x.Password == pwd).FirstOrDefault();
                if (validUser != null)
                {


                    if (Code > 0)
                    {
                        object[] param = { Code };

                        // RetailerData cty;

                        var result = dbContext.TBL_Consumers.Where(x => x.ConsumerID == Code).Select(x => new
                        {
                            ID = x.ID,
                            ConsumerNo = x.ConnectionCode,
                            DDR = dbContext.Cities.Where(z => z.ID == x.DDRID).Select(z => z.Name).FirstOrDefault(),
                            Ward = dbContext.Areas.Where(z => z.ID == x.AreaID).Select(z => z.Name).FirstOrDefault(),
                            ConsumerName = x.ConsumerName,
                            Address = x.Address,
                            MobileNo = 123,
                            AreaMarla = x.AreaMarla,
                            Lattitude = x.Latitude,
                            Longitude = x.Longitude,
                            MeterNo = x.MeterNo,
                            ConnectionType = dbContext.ConnectionTypes.Where(z => z.ID == x.ConnectionCode).Select(z => z.ConnectionTypeName).FirstOrDefault()

                        }).FirstOrDefault();


                        if (result != null)
                        {
                            return Ok(new
                            {
                                ConsumerInfo = result

                            });
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Log.Instance.Error(ex, "VisitDetailController GET API Failed");
            }
            object[] paramm = { };
            return Ok(new
            {
                ConsumerInfo = paramm
            });

        }


    }
}