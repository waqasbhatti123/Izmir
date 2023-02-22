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
    public class IZWebViewsController : Controller
    {
        // GET: IZWebViews
        [HttpGet]
        public ActionResult Display()
        {
            FOSDataModel db = new FOSDataModel();
            IZHomeData home = new IZHomeData();
            home.Months = db.Tbl_IZBillingPeriod.ToList();
            home.MonID = db.Tbl_IZBillingPeriod.Where(x => x.IsActive == true).FirstOrDefault().ID;
            return View(home);
        }

        [HttpPost]
        public JsonResult Display(DTParameters param, int monthID)
        {
            try
            {
                FOSDataModel db = new FOSDataModel();
                //int monthID = db.Tbl_IZBillingPeriod.Where(x => x.IsActive == true).FirstOrDefault().ID;

                //ViewBag.BlockName = db.Tbl_IZBlocks.Where(x => x.ID == blockID).FirstOrDefault().Name;

                var dtsource = new List<IZHomeData>();
                dtsource = ManageCity.GetDisplaySheetForGrid(monthID);
                List<String> columnSearch = new List<string>();
                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }
                List<IZHomeData> data = ManageCity.GetResultReadingCurrentMonth(param.Search.Value, param.SortOrder, param.Start, param.Length, dtsource, columnSearch);
                int count = ManageCity.CountCurrentMonthReading(param.Search.Value, dtsource, columnSearch);
                DTResult<IZHomeData> result = new DTResult<IZHomeData>
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
        [HttpGet]
        public  ActionResult MCO()
        {
            FOSDataModel db = new FOSDataModel();
            IZHomeData home = new IZHomeData();
            home.Months = db.Tbl_IZBillingPeriod.ToList();
            home.MonID = db.Tbl_IZBillingPeriod.Where(x => x.IsActive == true).FirstOrDefault().ID;
            return View(home);
        }
        [HttpPost]
        public ActionResult MCO(DTParameters param, int monthID)
        {
            try
            {
                FOSDataModel db = new FOSDataModel();
                //int monthID = db.Tbl_IZBillingPeriod.Where(x => x.IsActive == true).FirstOrDefault().ID;

                //ViewBag.BlockName = db.Tbl_IZBlocks.Where(x => x.ID == blockID).FirstOrDefault().Name;

                var dtsource = new List<IZHomeData>();
                dtsource = ManageCity.GetReadingByMCOForGrid(monthID);
                List<String> columnSearch = new List<string>();
                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }
                List<IZHomeData> data = ManageCity.GetResultReadingCurrentMonth(param.Search.Value, param.SortOrder, param.Start, param.Length, dtsource, columnSearch);
                int count = ManageCity.CountCurrentMonthReading(param.Search.Value, dtsource, columnSearch);
                DTResult<IZHomeData> result = new DTResult<IZHomeData>
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
        [HttpGet]
        public ActionResult GetSummaryReading()
        {
            FOSDataModel db = new FOSDataModel();
            IZHomeData home = new IZHomeData();
            Tbl_IZBillingPeriod bil = db.Tbl_IZBillingPeriod.Where(x => x.IsActive == true).FirstOrDefault();
            ViewBag.Moths = Convert.ToDateTime(bil.Name).ToString("MMM-yyyy");
            int Sec = bil.ID - 1;
            Tbl_IZBillingPeriod bilpr = db.Tbl_IZBillingPeriod.Where(y => y.ID == Sec).FirstOrDefault();
            if (bilpr != null)
            {
                ViewBag.PreMo = Convert.ToDateTime(bilpr.Name).ToString("MMM-yyyy");
            }
            else
            {
                ViewBag.PreMo = "";
            }
            return View(home);
        }
       
        public  ActionResult GetSummaryReading(DTParameters param)
        {
            try
            {
                FOSDataModel db = new FOSDataModel();
                //int monthID = db.Tbl_IZBillingPeriod.Where(x => x.IsActive == true).FirstOrDefault().ID;
                
                //ViewBag.BlockName = db.Tbl_IZBlocks.Where(x => x.ID == blockID).FirstOrDefault().Name;
                
                var dtsource = new List<IZHomeData>();
                dtsource = ManageCity.GetSummaryReadingForGrid();
                List<String> columnSearch = new List<string>();
                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }
                List<IZHomeData> data = ManageCity.GetSummaryReadingCurrentMonth(param.Search.Value, param.SortOrder, param.Start, param.Length, dtsource, columnSearch);
                int count = ManageCity.CountSummaryCurrentMonthReading(param.Search.Value, dtsource, columnSearch);
                DTResult<IZHomeData> result = new DTResult<IZHomeData>
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

        [HttpGet]
        public ActionResult GetAboveUnit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetAboveUnit(DTParameters param, string unit)
        {
            try
            {
                FOSDataModel db = new FOSDataModel();
                //int monthID = db.Tbl_IZBillingPeriod.Where(x => x.IsActive == true).FirstOrDefault().ID;

                //ViewBag.BlockName = db.Tbl_IZBlocks.Where(x => x.ID == blockID).FirstOrDefault().Name;

                var dtsource = new List<IZHomeData>();
                dtsource = ManageCity.GetAboveUniReadingForGrid(Convert.ToInt32(unit));
                List<String> columnSearch = new List<string>();
                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }
                List<IZHomeData> data = ManageCity.GetAboveUnitReadingCurrentMonth(param.Search.Value, param.SortOrder, param.Start, param.Length, dtsource, columnSearch);
                int count = ManageCity.CountSummaryCurrentMonthReading(param.Search.Value, dtsource, columnSearch);
                DTResult<IZHomeData> result = new DTResult<IZHomeData>
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