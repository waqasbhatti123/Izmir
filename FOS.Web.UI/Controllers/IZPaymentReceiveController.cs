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
    public class IZPaymentReceiveController : Controller
    {
        // GET: IZPaymentReceive
        public ActionResult PaymentReceive()
        {
            IZPaymentReceiveData Block = new IZPaymentReceiveData();
            using (FOSDataModel db = new FOSDataModel())
            {
                Block.Blocks = db.Tbl_IZBlocks.Where(x => x.Status == true).ToList();
                string MonthName = db.Tbl_IZBillingPeriod.Where(x => x.IsActive == true).FirstOrDefault().Name;
                ViewBag.BillingMonth = Convert.ToDateTime(MonthName).ToString("MMM-yyyy");
                Block.Mots = ManagePaymentReceive.GetMonth();
                Block.MonthID = db.Tbl_IZBillingPeriod.Where(x => x.IsActive == true).FirstOrDefault().ID;
                Block.Banks = ManagePaymentReceive.GetBank();
            }
            return View(Block);
        }
        
        public ActionResult PaymentReceivePay(IZPaymentReceiveData data)
        {
            return Content("0");
        }

        public JsonResult GetBillingData(DTParameters param, int BlockID)
        {
            try
            {
                FOSDataModel db = new FOSDataModel();

                string BillName = db.Tbl_IZBillingPeriod.Where(x => x.IsActive == true).FirstOrDefault().Name;

                var dtsource = new List<IZPaymentReceiveData>();
                dtsource = ManagePaymentReceive.GetBillData(BlockID, BillName);
                List<String> columnSearch = new List<string>();
                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }
                List<IZPaymentReceiveData> data = ManagePaymentReceive.GetResultBillData(param.Search.Value, param.SortOrder, param.Start, param.Length, dtsource, columnSearch);
                int count = ManagePaymentReceive.CountBillData(param.Search.Value, dtsource, columnSearch);
                DTResult<IZPaymentReceiveData> result = new DTResult<IZPaymentReceiveData>
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