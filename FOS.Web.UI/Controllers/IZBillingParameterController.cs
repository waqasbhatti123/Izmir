using FOS.DataLayer;
using FOS.Setup;
using FOS.Shared;
using FOS.Web.UI.Common.CustomAttributes;
using FOS.Web.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FOS.Web.UI.Controllers
{
    public class IZBillingParameterController : Controller
    {
        [CustomAuthorize]
        [HttpGet]
        // GET: IZBillingParameter
        public ActionResult BillingParameter()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BillingParameterDetail(IZBillingParaMeterData iZ)
        {
            using (FOSDataModel db = new FOSDataModel())
            {
                if (iZ != null)
                {
                    Tbl_IZBillingParameter tbl = new Tbl_IZBillingParameter();
                    if (iZ.ID == 0)
                    {
                        tbl.Domestic = iZ.Domestic;
                        tbl.DomesticTemp = iZ.DomesticTemp;
                        tbl.Commercial = iZ.Commercial;
                        tbl.Soceity = iZ.Society;
                        tbl.TeleCommunication = iZ.TeleCommunication;
                        tbl.FPA = iZ.FPA;
                        tbl.MonthID = db.Tbl_IZBillingPeriod.Where(x => x.IsActive == true).FirstOrDefault().ID;
                        db.Tbl_IZBillingParameter.Add(tbl);
                        db.SaveChanges();
                        return Content("1");
                    }
                    else
                    {
                        var par = db.Tbl_IZBillingParameter.Where(x => x.ParaID == iZ.ID).FirstOrDefault();
                        par.Domestic = iZ.Domestic;
                        par.DomesticTemp = iZ.DomesticTemp;
                        par.Commercial = iZ.Commercial;
                        par.Soceity = iZ.Society;
                        par.TeleCommunication = iZ.TeleCommunication;
                        par.FPA = iZ.FPA;
                        par.MonthID = db.Tbl_IZBillingPeriod.Where(x => x.IsActive == true).FirstOrDefault().ID;
                        db.Entry(par).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return Content("3");
                    }
                }
            }
            return Content("0");
        }

        public JsonResult BillingParaMeterHandler(DTParameters param)
        {
            try
            {
                var dtsource = new List<IZBillingParaMeterData>();
                dtsource = ManageCity.GetBillingParamter();
                List<String> columnSearch = new List<string>();
                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }
                List<IZBillingParaMeterData> data = ManageCity.GetResultBillingPara(param.Search.Value, param.SortOrder, param.Start, param.Length, dtsource, columnSearch);
                int count = ManageCity.CountBilliPara(param.Search.Value, dtsource, columnSearch);
                DTResult<IZBillingParaMeterData> result = new DTResult<IZBillingParaMeterData>
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
           
            using (FOSDataModel db = new FOSDataModel())
            {
                IZBillingParaMeterData data = new IZBillingParaMeterData();
                var temp = db.Tbl_IZBillingParameter.Where(x => x.ParaID == ID).FirstOrDefault();
                data.ID = temp.ParaID;
                data.Domestic = temp.Domestic;
                data.DomesticTemp = temp.DomesticTemp;
                data.Society = temp.Soceity;
                data.Commercial = temp.Commercial;
                data.TeleCommunication = temp.TeleCommunication;
                data.FPA = temp.FPA;
                return Json(data);
            }
        }
    }
}