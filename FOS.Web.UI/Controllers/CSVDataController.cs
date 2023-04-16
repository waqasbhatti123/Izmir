using FOS.DataLayer;
using FOS.Setup;
using FOS.Shared;
using FOS.Web.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FOS.Web.UI.Controllers
{
    public class CSVDataController : Controller
    {
        // GET: CSVData
        public ActionResult Index()
        {
            using (FOSDataModel db = new FOSDataModel())
            {
                var data = db.Tbl_IZBillingPeriod.Where(x => x.IsActive == true).FirstOrDefault();
                ViewBag.Month = Convert.ToDateTime(data.Name).ToString("MMM-yyyy");
            }
            return View();
        }

        public JsonResult GetPaymentData(DTParameters param)
        {
            try
            {
                FOSDataModel db = new FOSDataModel();

                var dtsource = new List<IZPaymentDetailData>();
                dtsource = ManageIZPaymentDetail.GetPayment();
                List<String> columnSearch = new List<string>();
                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }
                List<IZPaymentDetailData> data = ManageIZPaymentDetail.GetResultPay(param.Search.Value, param.SortOrder, param.Start, param.Length, dtsource, columnSearch);
                int count = ManageIZPaymentDetail.CountPayment(param.Search.Value, dtsource, columnSearch);
                DTResult<IZPaymentDetailData> result = new DTResult<IZPaymentDetailData>
                {
                    draw = param.Draw,
                    data = data,
                    recordsFiltered = count,
                    recordsTotal = count
                };
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
    }
}