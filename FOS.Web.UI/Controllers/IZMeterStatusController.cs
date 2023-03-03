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
    public class IZMeterStatusController : Controller
    {
        // GET: IZMeterStatus
        public ActionResult MeterStatus()
        {
            return View();
        }
        public ActionResult IZMeterStatus(IZMeterData MeterData)
        {
            Tbl_IZMeterStatus tbl = new Tbl_IZMeterStatus();
            using(FOSDataModel dbb=new FOSDataModel())
            {
                if (MeterData.MeterID == 0)
                {
                    tbl.MeterStatusID = MeterData.MeterID;
                    tbl.StatusName = MeterData.MeterStatus;
                    tbl.IsActive = true;
                    dbb.Tbl_IZMeterStatus.Add(tbl);
                    dbb.SaveChanges();
                    return Content("1");
                }
                else
                {
                    Tbl_IZMeterStatus dbl = dbb.Tbl_IZMeterStatus.Where(x => x.MeterStatusID == MeterData.MeterID).FirstOrDefault();
                    dbl.StatusName = MeterData.MeterStatus;
                    dbl.IsActive = true;
                    dbb.Entry(MeterData).State = System.Data.Entity.EntityState.Modified;
                    dbb.SaveChanges();
                    return Content("2");
                }
            }
        }
        public JsonResult GetData(DTParameters param)
        {
            try
            {
                FOSDataModel db = new FOSDataModel();

                var dtsource = new List<IZMeterData>();
                dtsource = ManageIZMeter.GetBanks();
                List<String> columnSearch = new List<string>();
                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }
                List<IZMeterData> data = ManageIZMeter.GetResultBank(param.Search.Value, param.SortOrder, param.Start, param.Length, dtsource, columnSearch);
                int count = ManageIZMeter.CountSocity(param.Search.Value, dtsource, columnSearch);
                DTResult<IZMeterData> result = new DTResult<IZMeterData>
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
            IZMeterData data = new IZMeterData();
            using (FOSDataModel db = new FOSDataModel())
            {
                Tbl_IZMeterStatus IZ = db.Tbl_IZMeterStatus.Where(x => x.MeterStatusID == ID).FirstOrDefault();
                data.MeterID = IZ.MeterStatusID;
                data.MeterStatus = IZ.StatusName;
                return Json(data);
            }
        }
    }
}


