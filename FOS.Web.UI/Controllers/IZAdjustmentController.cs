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
    public class IZAdjustmentController : Controller
    {
        [HttpGet]
        // GET: IZAdjustment
        public ActionResult Adjustmnet()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Adjustmnet(IZAdjustmentData data )
        {
            Tbl_IZAdjustment tbl = new Tbl_IZAdjustment();
            using (FOSDataModel dbb=new FOSDataModel())
            {
                if (data.ID == 0)
                {
                    tbl.ReferenceNo = data.ReferenceNo;
                    tbl.Amount = data.Amount;
                    tbl.BillingMonth = data.BillingMonth;
                    tbl.Comments = data.Comments;
                    tbl.Adjtype = data.Adjtype;
                    dbb.Tbl_IZAdjustment.Add(tbl);
                    dbb.SaveChanges();
                    return Content("1");
                }
                else
                {
                    Tbl_IZAdjustment Adj = dbb.Tbl_IZAdjustment.Where(x => x.ID == data.ID).FirstOrDefault();
                    Adj.ReferenceNo = data.ReferenceNo;
                    Adj.Amount = data.Amount;
                    Adj.BillingMonth = data.BillingMonth;
                    Adj.Comments = data.Comments;
                    Adj.Adjtype = data.Adjtype;
                    dbb.SaveChanges();
                    return Content("2");
                }

            }
            
        }

        public JsonResult GetBankData(DTParameters param)
        {
            try
            {
                FOSDataModel db = new FOSDataModel();

                var dtsource = new List<IZAdjustmentData>();
                dtsource = ManageIZAdjustment.GetBanks();
                List<String> columnSearch = new List<string>();
            
    foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }
                List<IZAdjustmentData> data = ManageIZAdjustment.GetResultBank(param.Search.Value, param.SortOrder, param.Start, param.Length, dtsource, columnSearch);
                int count = ManageIZAdjustment.CountSocity(param.Search.Value, dtsource, columnSearch);
                DTResult<IZAdjustmentData> result = new DTResult<IZAdjustmentData>
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

        public JsonResult GetbyID(int ID)
        {
            IZAdjustmentData data = new IZAdjustmentData();
            using (FOSDataModel db = new FOSDataModel())
            {
                Tbl_IZAdjustment IZ = db.Tbl_IZAdjustment.Where(x => x.ID == ID).FirstOrDefault();
                data.ID = IZ.ID;
                data.ReferenceNo = IZ.ReferenceNo;
                data.Amount = IZ.Amount;
                data.BillingMonth = Convert.ToDateTime(IZ.BillingMonth).ToString("MMM-yyyy");
                //data.Status = bank.IsActive.ToString();
                return Json(data);
            }
        }
    }
}