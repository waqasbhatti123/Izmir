using FOS.DataLayer;
using FOS.Web.UI.Common;
using Shared.Diagnostics.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web.Http;

namespace FOS.Web.UI.Controllers.API
{
    public class ConsumerLatLongRegistrationController : ApiController
    {
        FOSDataModel db = new FOSDataModel();

        public Result<SuccessResponse> Post(DailyActivityRequest rm)
        { // This controller is for retailers orders.
            JobsDetail jobDet = new JobsDetail();
            var JobObj = new Job();
            var RemObj = new TblReminder();
            try
            {
                Tbl_IZConsumers ret = db.Tbl_IZConsumers.Where(r => r.ID == rm.RetailerID).FirstOrDefault();
                if (ret != null)
                {
                    ret.Latitude = rm.Latitude;
                    ret.Longitude = rm.Longitude;
                    ret.Location=rm.Latitude+","+rm.Longitude;

                  
                    db.SaveChanges();


                  
                   
                    }

                    return new Result<SuccessResponse>
                    {
                        Data = null,
                        Message = "Lat Long Saved Successfully",
                        ResultType = ResultType.Success,
                        Exception = null,
                        ValidationErrors = null
                    };
                }
            catch (Exception ex)
            {
                Log.Instance.Error(ex, "Order API Failed");
                return new Result<SuccessResponse>
                {
                    Data = null,
                    Message = "Lat Long Saved API Failed",
                    ResultType = ResultType.Exception,
                    Exception = ex,
                    ValidationErrors = null
                };
            

        }
        }

        


        public class SuccessResponse
        {

        }
        public class DailyActivityRequest
        {
            public DailyActivityRequest()
            {
                StockItems = new List<JobItemModel>();
            }
            public int JobId { get; set; }
            public int RetailerID { get; set; }
            public int SaleOfficerId { get; set; }
            public int Followupreason { get; set; }
            public string Type { get; set; }
            public decimal Latitude { get; set; }
            public decimal Longitude { get; set; }
            public string Location { get; set; }
            public string PurposeofVisit { get; set; }
            public string ActivityDetails { get; set; }
            public string Picture1 { get; set; }
            public string ReminderCancelStatus { get; set; }

            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{dd/MM/yyyy}")]
            public string NextVisitDate { get; set; }
            public string Priorty { get; set; }
            public string TentativeCloseDate { get; set; }
            public List<JobItemModel> StockItems { get; set; }

        }

        public class JobItemModel
        {
            public int JobID { get; set; }
            public int ItemID { get; set; }
            public decimal Quantity { get; set; }
            public decimal Price { get; set; }
        }
    }
}