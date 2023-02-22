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
    public class IZUnitZeroMeterController : Controller
    {
        // GET: IZUnitZeroMeter
        [HttpGet]
        public ActionResult IZUnitZero()
        {
            {
                FOSDataModel db = new FOSDataModel();
                IZHomeData data = new IZHomeData();
                data.Months = db.Tbl_IZBillingPeriod.ToList();
                data.MonID = db.Tbl_IZBillingPeriod.Where(x => x.IsActive == true).FirstOrDefault().ID;
                return View(data);
            }
        }


        [HttpPost]
        public JsonResult IZUnitZero(DTParameters param, int monthID)
        {
            try
            {
                FOSDataModel db = new FOSDataModel();
                //int monthID = db.Tbl_IZBillingPeriod.Where(x => x.IsActive == true).FirstOrDefault().ID;

                //ViewBag.BlockName = db.Tbl_IZBlocks.Where(x => x.ID == blockID).FirstOrDefault().Name;

                var dtsource = new List<IZMeterZeroUnit>();
                dtsource = ManageZeroMeter.GetDisplaySheetForGrid(monthID);
                List<String> columnSearch = new List<string>();
                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }
                List<IZMeterZeroUnit> data = ManageZeroMeter.GetResultReadingCurrentMonth(param.Search.Value, param.SortOrder, param.Start, param.Length, dtsource, columnSearch);
                int count = ManageZeroMeter.CountCurrentMonthReading(param.Search.Value, dtsource, columnSearch);
                DTResult<IZMeterZeroUnit> result = new DTResult<IZMeterZeroUnit>
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
    