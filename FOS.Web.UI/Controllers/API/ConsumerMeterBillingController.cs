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
    public class ConsumerMeterBillingController : ApiController
    {
        FOSDataModel db = new FOSDataModel();

        public Result<SuccessResponse> Post(DailyActivityRequest rm)
        { // This controller is for retailers orders.
            JobsDetail jobDet = new JobsDetail();
            var JobObj = new Job();
            var RemObj = new TblReminder();
            try
            {
                var billingmonth = db.Tbl_IZBillingPeriod.Where(x => x.IsActive == true).FirstOrDefault();
                var Previousdata=db.JobsDetails.Where(x => x.ConsumerID == rm.RetailerId && x.BillingPeriodID==billingmonth.ID).FirstOrDefault();
                Tbl_IZConsumers ret = db.Tbl_IZConsumers.Where(r => r.ID == rm.RetailerId).FirstOrDefault();
                if (Previousdata != null)
                {
                    db.JobsDetails.Remove(Previousdata);
                    db.SaveChanges();

                }

                

                var data = db.JobsDetails.Where(x => x.ConsumerID == rm.RetailerId).OrderByDescending(x => x.ID).FirstOrDefault();

    

                if (ret != null)
                {
                    DateTime serverTime = DateTime.Now; // gives you current Time in server timeZone
                    DateTime utcTime = serverTime.ToUniversalTime(); // convert it to Utc using timezone setting of server computer

                    TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("Pakistan Standard Time");
                    DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi);

                   
                    JobObj.ID = db.Jobs.OrderByDescending(u => u.ID).Select(u => u.ID).FirstOrDefault() + 1;

                    JobObj.SaleOfficerID = rm.SaleOfficerId;
                    JobObj.RegionalHeadID = 4;
                    JobObj.RegionalHeadType = 1;
                    JobObj.Status = true;
                    JobObj.StartingDate = localTime;
                    JobObj.DateOfAssign = localTime;
                    JobObj.CreatedDate = localTime;
                    JobObj.LastUpdated = localTime;
                    JobObj.IsActive = true;
                    JobObj.IsDeleted = false;


                    //ADD New Job in jobsdetail 
                    jobDet.JobID = JobObj.ID;
                    jobDet.RegionalHeadID = JobObj.RegionalHeadID;
                    jobDet.SalesOficerID = JobObj.SaleOfficerID;
                    jobDet.RetailerID = 1;
                    jobDet.ConsumerID = rm.RetailerId;
                    jobDet.ActivityDetails = "Online";
                    jobDet.JobDate = localTime;
                    jobDet.JobType = "Meter Reading";
                    jobDet.Status = true;
                    jobDet.MeterReading = rm.MeterReading;
                    jobDet.OffPeakReading = rm.OffPeakReading;
                    jobDet.BillingPeriodID = billingmonth.ID;
                    jobDet.ExportUnits = rm.ExportsUnits;
                    jobDet.PeakReading = rm.Peakreading;
                    jobDet.ReadingFeedback = rm.ReadingFeedback;
                    jobDet.Remarks = rm.Remarks;
                    if (data == null)
                    {
                        jobDet.PreviousReading = 0;

                        jobDet.UnitsConsumed = rm.MeterReading - jobDet.PreviousReading;

                    }
                    else
                    {
                        jobDet.PreviousReading = data.MeterReading;
                        jobDet.UnitsConsumed = rm.MeterReading - data.MeterReading;
                    }
                  
                    // jobDet.ActivityType = rm.ActivityType;
                    jobDet.VisitPurpose = "Ordering";
                    if (rm.Picture1 == "" || rm.Picture1 == null)
                    {
                        jobDet.Picture1 = null;
                    }
                    else
                    {
                        jobDet.Picture1 = ConvertIntoByte(rm.Picture1, "OrderPicture", DateTime.Now.ToString("dd-mm-yyyy hhmmss").Replace(" ", ""), "OrderingPictures");
                    }

                    if (rm.Picture2 == "" || rm.Picture2 == null)
                    {
                        jobDet.PeakReadingPicture = null;
                    }
                    else
                    {
                        jobDet.PeakReadingPicture = ConvertIntoByte(rm.Picture2, "OrderPicture", DateTime.Now.ToString("dd-mm-yyyy hhmmss").Replace(" ", ""), "OrderingPictures");
                    }

                    if (rm.Picture3 == "" || rm.Picture3 == null)
                    {
                        jobDet.OffPeakReadingPicture = null;
                    }
                    else
                    {
                        jobDet.OffPeakReadingPicture = ConvertIntoByte(rm.Picture3, "OrderPicture", DateTime.Now.ToString("dd-mm-yyyy hhmmss").Replace(" ", ""), "OrderingPictures");
                    }


                    if (rm.Picture4 == "" || rm.Picture4 == null)
                    {
                        jobDet.ExportUnistPicture = null;
                    }
                    else
                    {
                        jobDet.ExportUnistPicture = ConvertIntoByte(rm.Picture4, "OrderPicture", DateTime.Now.ToString("dd-mm-yyyy hhmmss").Replace(" ", ""), "OrderingPictures");
                    }






                    db.Jobs.Add(JobObj);
                    db.SaveChanges();
                    db.JobsDetails.Add(jobDet);
                    db.SaveChanges();


                    
                    }

                    return new Result<SuccessResponse>
                    {
                        Data = null,
                        Message = "Reading Done Successfully",
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
                    Message = "Order API Failed",
                    ResultType = ResultType.Exception,
                    Exception = ex,
                    ValidationErrors = null
                };
            

        }
        }

        public string ConvertIntoByte(string Base64, string DealerName, string SendDateTime, string folderName)
        {
            byte[] bytes = Convert.FromBase64String(Base64);
            MemoryStream ms = new MemoryStream(bytes, 0, bytes.Length);
            ms.Write(bytes, 0, bytes.Length);
            Image image = Image.FromStream(ms, true);
            //string filestoragename = Guid.NewGuid().ToString() + UserName + ".jpg";
            string filestoragename = DealerName + SendDateTime;
            string outputPath = System.Web.HttpContext.Current.Server.MapPath(@"~/Images/" + folderName + "/" + filestoragename + ".jpg");
            image.Save(outputPath, ImageFormat.Jpeg);

            //string fileName = UserName + ".jpg";
            //string rootpath = Path.Combine(Server.MapPath("~/Photos/ProfilePhotos/"), Path.GetFileName(fileName));
            //System.IO.File.WriteAllBytes(rootpath, Convert.FromBase64String(Base64));
            return @"/Images/" + folderName + "/" + filestoragename + ".jpg";
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
            public int RetailerId { get; set; }
            public int SaleOfficerId { get; set; }
            public decimal MeterReading { get; set; }
            public decimal OffPeakReading { get; set; }

            public decimal Peakreading { get; set; }
            public decimal ExportsUnits { get; set; }
            public string PurposeofVisit { get; set; }
            public string ActivityDetails { get; set; }
            public string Picture1 { get; set; }
            public string Picture2 { get; set; }
            public string Picture3 { get; set; }
            public string Picture4 { get; set; }
            public string Remarks { get; set; }
            public string ReminderCancelStatus { get; set; }

            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{dd/MM/yyyy}")]
            public string NextVisitDate { get; set; }
            public string Priorty { get; set; }

            public string ReadingFeedback { get; set; }
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