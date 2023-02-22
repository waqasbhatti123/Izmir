using FOS.DataLayer;
using FOS.Setup;
using FOS.Setup.Validation;
using FOS.Shared;
using FOS.Web.UI.Common.CustomAttributes;
using FOS.Web.UI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FOS.Web.UI.Controllers
{
    public class IZMonthController : Controller
    {
        // GET: IZMonth
        [CustomAuthorize]
        [HttpGet]
        public ActionResult MonthClose()
        {
            string acmon = GelActiveMonth();
            ViewBag.monName = "Current Active Month " + acmon + " do you want to end it?";
            return View();
        }

        [HttpPost]
        public ActionResult MonthClose(IZMonthCloseData data)
        {
            using (FOSDataModel db = new FOSDataModel())
            {
                Tbl_IZBillingPeriod mon = new Tbl_IZBillingPeriod();
                if (data != null)
                {
                    if (data.ID == 0)
                    {
                        Tbl_IZBillingPeriod checkMonth = db.Tbl_IZBillingPeriod.Where(x => x.Name == data.Name).FirstOrDefault();
                        if (checkMonth != null)
                        {

                            //TempData["msg"] = "Month Already Exits";
                            return Content("2");
                        }
                    }
                    
                    List<Tbl_IZBillingPeriod> monList = db.Tbl_IZBillingPeriod.Where(x => x.IsActive == true).ToList();
                    foreach (var item in monList)
                    {
                        item.IsActive = false;
                        db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    if (data.ID == 0)
                    {


                        mon.Name = data.Name;
                        //mon.ReadingStart = Convert.ToDateTime(data.ReadingStart);
                        //mon.BillIssueDate = Convert.ToDateTime(data.IssueDate);
                        //mon.BillDueDate = Convert.ToDateTime(data.DueDate);
                        mon.IsActive = true;
                        db.Tbl_IZBillingPeriod.Add(mon);
                        db.SaveChanges();
                        return Content("1");

                    }
                    else
                    {
                        Tbl_IZBillingPeriod izmo = db.Tbl_IZBillingPeriod.Where(x => x.ID == data.ID).FirstOrDefault();
                        izmo.Name = data.Name;
                        //izmo.ReadingStart = Convert.ToDateTime(data.ReadingStart);
                        //izmo.BillIssueDate = Convert.ToDateTime(data.IssueDate);
                        //izmo.BillDueDate = Convert.ToDateTime(data.DueDate);
                        izmo.IsActive = true;
                        db.Entry(izmo).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return Content("3");
                        //TempData["msg"] = "";
                    }
                }

            }
            return Content("0");
        }


        public string GelActiveMonth()
        {
            using (FOSDataModel db = new FOSDataModel())
            {
                string monName;
                Tbl_IZBillingPeriod mon = db.Tbl_IZBillingPeriod.Where(x => x.IsActive == true).FirstOrDefault();
                if (mon != null)
                {
                    monName = mon.Name;
                }
                else
                {
                    monName = "";
                }

                return monName;
            }
        }

        public JsonResult MonthDataHandler(DTParameters param)
        {
            try
            {
                var dtsource = new List<IZMonthCloseData>();
                dtsource = ManageCity.GetMonthsForGrid();
                List<String> columnSearch = new List<string>();
                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }
                List<IZMonthCloseData> data = ManageCity.GetResultMonth(param.Search.Value, param.SortOrder, param.Start, param.Length, dtsource, columnSearch);
                int count = ManageCity.CountMonth(param.Search.Value, dtsource, columnSearch);
                DTResult<IZMonthCloseData> result = new DTResult<IZMonthCloseData>
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
            IZMonthCloseData mon = new IZMonthCloseData();
            using (FOSDataModel db = new FOSDataModel())
            {
                Tbl_IZBillingPeriod month = db.Tbl_IZBillingPeriod.Where(x => x.ID == ID).FirstOrDefault();
                mon.ID = month.ID;
                mon.Name = month.Name;
               // mon.ReadingStart = month.ReadingStart.Value.ToString("dd-MMM-yyy");
               // mon.IssueDate = month.BillIssueDate.Value.ToString("dd-MMM-yyy");
               // mon.DueDate = month.BillDueDate.Value.ToString("dd-MMM-yyy");
            }
            return Json(mon, JsonRequestBehavior.AllowGet);
        }

    }





}