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
    public class IZConnectionTypeController : Controller
    {
        // GET: IZConnectionType
        public ActionResult IZConnection()
        {
            return View();
        }
        public ActionResult IZConnectionType(IZConnectionData IZdata)
        {
            Tbl_IZConnectionType dbb = new Tbl_IZConnectionType();
            using(FOSDataModel tb=new FOSDataModel())
            {
                if (IZdata.ID == 0)
                {
                    //dbb.ConnectionID = IZdata.ID;
                    dbb.ConnectionName = IZdata.ConnectionType;
                    dbb.IsActive = true;
                    tb.Tbl_IZConnectionType.Add(dbb);
                    tb.SaveChanges();
                    return Content("1");
                }
                else
                {
                    Tbl_IZConnectionType DB = tb.Tbl_IZConnectionType.Where(x => x.ConnectionID == IZdata.ID).FirstOrDefault();
                    DB.ConnectionName = IZdata.ConnectionType;
                    DB.IsActive = true;
                    tb.Entry(DB).State = System.Data.Entity.EntityState.Modified;
                    tb.SaveChanges();
                    return Content("2");
                }
            }
        }

        public JsonResult GetData(DTParameters param)
        {
            try
            {
                FOSDataModel db = new FOSDataModel();

                var dtsource = new List<IZConnectionData>();
                dtsource = ManageIZConnection.GetBanks();
                List<String> columnSearch = new List<string>();
                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }
                List<IZConnectionData> data = ManageIZConnection.GetResultBank(param.Search.Value, param.SortOrder, param.Start, param.Length, dtsource, columnSearch);
                int count = ManageIZConnection.CountSocity(param.Search.Value, dtsource, columnSearch);
                DTResult<IZConnectionData> result = new DTResult<IZConnectionData>
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
            IZConnectionData data = new IZConnectionData();
            using (FOSDataModel db = new FOSDataModel())
            {
                Tbl_IZConnectionType IZ = db.Tbl_IZConnectionType.Where(x => x.ConnectionID == ID).FirstOrDefault();
                data.ID = IZ.ConnectionID;
                data.ConnectionType = IZ.ConnectionName;
                return Json(data);
            }
        }
    }
}