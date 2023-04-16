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
    public class IZInstallmentController : Controller
    {
        // GET: IZInstallment
        [HttpGet]
        public ActionResult Installment()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Installment(IZInstallmentData dt)
        {
            Tbl_IZInstallment tbl = new Tbl_IZInstallment();
            using(FOSDataModel db=new FOSDataModel())
            {
                if (dt.ID == 0)
                {
                    tbl.ReferenceNo = dt.ReferenceNo;
                    tbl.Amount = dt.Amount;
                    tbl.BillingMonth = dt.BillingMonth;
                    db.Tbl_IZInstallment.Add(tbl);
                    db.SaveChanges();
                    return Content("1");
                }
                else
                {
                    Tbl_IZInstallment IZ = db.Tbl_IZInstallment.Where(x => x.ID == dt.ID).FirstOrDefault();
                    IZ.ReferenceNo = dt.ReferenceNo;
                    IZ.Amount = dt.Amount;
                    IZ.BillingMonth = dt.BillingMonth;
                    db.Entry(IZ).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return Content("2");
                }
            }
        }

        public JsonResult GetBankData(DTParameters param)
        {
            try
            {
                FOSDataModel db = new FOSDataModel();

                var dtsource = new List<IZInstallmentData>();
                dtsource = ManageIZInstallment.GetBanks();
                List<String> columnSearch = new List<string>();
                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }
                List<IZInstallmentData> data = ManageIZInstallment.GetResultBank(param.Search.Value, param.SortOrder, param.Start, param.Length, dtsource, columnSearch);
                int count = ManageIZInstallment.CountSocity(param.Search.Value, dtsource, columnSearch);
                DTResult<IZInstallmentData> result = new DTResult<IZInstallmentData>
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
            IZInstallmentData data = new IZInstallmentData();
            using (FOSDataModel db = new FOSDataModel())
            {
                Tbl_IZInstallment IZ = db.Tbl_IZInstallment.Where(x => x.ID == ID).FirstOrDefault();
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