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
    public class IZBankController : Controller
    {
        // GET: IZBank
        [HttpGet]
        public ActionResult IZBank()
        {
            return View();
        }
        [HttpPost]
        public ActionResult IZBank(IZBankData data)
        {
            Tbl_IZBanks bank = new Tbl_IZBanks();
            using (FOSDataModel db = new FOSDataModel())
            {
                if (data.ID == 0)
                {
                    bank.BankName = data.BankName;
                    bank.AccountNo = data.AccountNo;
                    bank.IsActive = Convert.ToBoolean(data.Status);
                    bank.CreatedDate = DateTime.Now;
                    db.Tbl_IZBanks.Add(bank);
                    db.SaveChanges();
                    return Content("1");
                }
                else
                {
                    Tbl_IZBanks banks = db.Tbl_IZBanks.Where(x => x.BankID == data.ID).FirstOrDefault();
                    banks.BankName = data.BankName;
                    banks.AccountNo = data.AccountNo;
                    banks.IsActive = Convert.ToBoolean(data.Status);
                    banks.CreatedDate = DateTime.Now;
                    db.Entry(banks).State = System.Data.Entity.EntityState.Modified;
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

                var dtsource = new List<IZBankData>();
                dtsource = ManageIZBank.GetBanks();
                List<String> columnSearch = new List<string>();
                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }
                List<IZBankData> data = ManageIZBank.GetResultBank(param.Search.Value, param.SortOrder, param.Start, param.Length, dtsource, columnSearch);
                int count = ManageIZBank.CountSocity(param.Search.Value, dtsource, columnSearch);
                DTResult<IZBankData> result = new DTResult<IZBankData>
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
            IZBankData data = new IZBankData();
            using (FOSDataModel db = new FOSDataModel())
            {
                Tbl_IZBanks bank = db.Tbl_IZBanks.Where(x => x.BankID == ID).FirstOrDefault();
                data.ID = bank.BankID;
                data.BankName = bank.BankName;
                data.AccountNo = bank.AccountNo;
                data.Status = bank.IsActive.ToString();
                return Json(data);
            }
        }
    }
}