using FOS.DataLayer;
using FOS.Setup;
using FOS.Shared;
using FOS.Web.UI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FOS.Web.UI.Controllers
{
    public class CSVFileController : Controller
    {
        // GET: CSVFile
        public ActionResult CSVFile()
        {
            return View();
        }


        public void UplodFile(IZBankData data)
        {
            using (FOSDataModel db = new FOSDataModel())
            {
                HttpPostedFileBase file = Request.Files["BankName"];
                string filePath = string.Empty;
                if (file != null)
                {
                    string path = Server.MapPath("~/TempFiles/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    filePath = path + Path.GetFileName(file.FileName);
                    string extension = Path.GetExtension(file.FileName);
                    file.SaveAs(filePath);

                    //Read the contents of CSV file.
                    string csvData = System.IO.File.ReadAllText(filePath);
                    int MontID = db.Tbl_IZBillingPeriod.Where(x => x.IsActive == true).FirstOrDefault().ID;
                    string Ref = "";
                    string ress = "";
                    foreach (string row in csvData.Split('\n'))
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            ress = row.Split(',')[0];
                            Tbl_IZCreateBill cre = db.Tbl_IZCreateBill.Where(x => x.ReferenceNo == ress).FirstOrDefault();
                            if (cre != null)
                            {
                                cre.unpaid = true;
                                db.SaveChanges();
                            }
                            Tbl_IZConsumers con = db.Tbl_IZConsumers.Where(x => x.RefNo == ress).FirstOrDefault();
                            Tbl_IZPayments payment = db.Tbl_IZPayments.Where(x => x.ConsumerID == con.ID).FirstOrDefault();
                            if (payment == null)
                            {
                                var pay = new Tbl_IZPayments();
                                Ref = row.Split(',')[0];
                                pay.ConsumerID = db.Tbl_IZConsumers.Where(x => x.RefNo == Ref).FirstOrDefault().ID;
                                pay.PaymentDate = Convert.ToDateTime(row.Split(',')[1]);
                                pay.Amount = row.Split(',')[2];
                                pay.BankID = Convert.ToInt32(row.Split(',')[3]);
                                pay.TransanctionType = null;
                                pay.PaymentType = "Bank";
                                pay.MonthID = MontID;
                                pay.CreatedOn = DateTime.Now;
                                pay.CSV = "Nill";
                                pay.MF = null;
                                db.Tbl_IZPayments.Add(pay);
                                db.SaveChanges();
                            }
                            
                        }
                    }
                    //return Content("1");

                }
                //return Json("Save Successfully");
            }
        }


        public JsonResult GetPaymentData(DTParameters param)
        {
            try
            {
                FOSDataModel db = new FOSDataModel();

                var dtsource = new List<IZCSVData>();
                dtsource = ManageIZCSV.GetPayment();
                List<String> columnSearch = new List<string>();
                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }
                List<IZCSVData> data = ManageIZCSV.GetResultPay(param.Search.Value, param.SortOrder, param.Start, param.Length, dtsource, columnSearch);
                int count = ManageIZCSV.CountPayment(param.Search.Value, dtsource, columnSearch);
                DTResult<IZCSVData> result = new DTResult<IZCSVData>
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