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
            bool flag = false;
            using (FOSDataModel db = new FOSDataModel())
            {
                Tbl_IZCreateBill cre = db.Tbl_IZCreateBill.Where(x => x.BillID == data.ID).FirstOrDefault();
                if (cre != null)
                {
                    cre.unpaid = true;
                    db.SaveChanges();
                    flag = true;
                }
                if (flag == true)
                {
                    Tbl_IZPayments pay = new Tbl_IZPayments();
                    pay.ConsumerID = db.Tbl_IZConsumers.Where(x => x.RefNo == data.RefNo).FirstOrDefault().ID;
                    pay.MonthID = data.MonthID;
                    pay.PaymentType = data.TransactionType;
                    pay.BankID = data.BankID;
                    pay.CSV = data.DateExtended;
                    pay.PaymentDate = Convert.ToDateTime(data.PaymentDate);
                    pay.Amount = data.Amount;
                    db.Tbl_IZPayments.Add(pay);
                    db.SaveChanges();
                    return Content("1");
                }
                
            }
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

        public JsonResult GetByID(int ID)
        {
            IZPaymentReceiveData data = new IZPaymentReceiveData();
            using (FOSDataModel db = new FOSDataModel())
            {
                Tbl_IZCreateBill cre = db.Tbl_IZCreateBill.Where(x => x.BillID == ID).FirstOrDefault();
                data.ID = cre.BillID;
                data.Name = cre.ConsumerName;
                data.Amount = cre.TotalBill;
                data.RefNo = cre.ReferenceNo;
                return Json(data);
            }
        }

        public ActionResult PaymentUpdate()
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

        public ActionResult PaymentsUpdates(IZPaymentReceiveData data)
        {
            using (FOSDataModel db = new FOSDataModel())
            {
                Tbl_IZPayments pay = db.Tbl_IZPayments.Where(x => x.ID == data.ID).FirstOrDefault();
                pay.ConsumerID = pay.ConsumerID;
                pay.MonthID = data.MonthID;
                pay.PaymentType = data.TransactionType;
                pay.BankID = data.BankID;
                pay.CSV = data.DateExtended;
                pay.PaymentDate = Convert.ToDateTime(data.PaymentDate);
                pay.Amount = data.Amount;
                db.Entry(pay).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Content("1");
            }
        }

        public JsonResult GetPaymentData(DTParameters param)
        {
            try
            {
                FOSDataModel db = new FOSDataModel();

                int BillName = db.Tbl_IZBillingPeriod.Where(x => x.IsActive == true).FirstOrDefault().ID;

                var dtsource = new List<IZPaymentReceiveData>();
                dtsource = ManagePaymentReceive.GetPaymentData(BillName);
                List<String> columnSearch = new List<string>();
                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }
                List<IZPaymentReceiveData> data = ManagePaymentReceive.GetResultPayData(param.Search.Value, param.SortOrder, param.Start, param.Length, dtsource, columnSearch);
                int count = ManagePaymentReceive.CountPayData(param.Search.Value, dtsource, columnSearch);
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

        public JsonResult PaymentGetByID(int ID)
        {
            IZPaymentReceiveData data = new IZPaymentReceiveData();
            using (FOSDataModel db = new FOSDataModel())
            {
                Tbl_IZPayments cre = db.Tbl_IZPayments.Where(x => x.ID == ID).FirstOrDefault();
                data.ID = cre.ID;
                data.Name = db.Tbl_IZConsumers.Where(x => x.ID == cre.ConsumerID).FirstOrDefault().OwnerName;
                data.RefNo = db.Tbl_IZConsumers.Where(x => x.ID == cre.ConsumerID).FirstOrDefault().RefNo;
                data.Amount = cre.Amount;
                data.TransactionType = cre.PaymentType;
                data.BankID = (int)cre.BankID;
                data.PaymentDate = cre.PaymentDate.Value.ToString("dd-MMM-yyyy");
                data.MonthID = (int)cre.MonthID;
                data.DateExtended = cre.CSV;

                return Json(data);
            }
        }


    }
}