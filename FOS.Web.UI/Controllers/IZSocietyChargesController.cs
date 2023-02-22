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
    public class IZSocietyChargesController : Controller
    {
        // GET: IZSocietyCharges
        [HttpGet]
        public ActionResult SocityCharges()
        {
            IZSocietyChargesData data = new IZSocietyChargesData();
            data.Consumers = ManageIZSocityCharges.GetConsumer();
            return View(data);
        }
        [HttpPost]
        public ActionResult SocityCharges(IZSocietyChargesData data)
        {
            using (FOSDataModel db = new FOSDataModel())
            {
                TBl_IZSocietyCharges so = new TBl_IZSocietyCharges();
                if (data.ID == 0)
                {
                    so.ConsumerID = data.ConsumerID;
                    so.ConsumerRefNo = db.Tbl_IZConsumers.Where(x => x.ID == data.ConsumerID).FirstOrDefault().RefNo;
                    so.meterNo = data.MeterNo;
                    so.StreetLight = data.StreetLight;
                    so.Garbage = data.Garbage;
                    so.Water = data.Water;
                    so.Sew = data.Sew;
                    so.OtherCharges = data.OtherCharges;
                    so.PTVFee = data.PtvFee;
                    so.Maintenace = data.Maintenance;
                    db.TBl_IZSocietyCharges.Add(so);
                    db.SaveChanges();
                    return Content("1");
                }
                else
                {
                    TBl_IZSocietyCharges soc = db.TBl_IZSocietyCharges.Where(x => x.ID == data.ID).FirstOrDefault();
                    soc.ConsumerID = data.ConsumerID;
                    soc.ConsumerRefNo = db.Tbl_IZConsumers.Where(x => x.ID == data.ConsumerID).FirstOrDefault().RefNo;
                    soc.meterNo = data.MeterNo;
                    soc.StreetLight = data.StreetLight;
                    soc.Garbage = data.Garbage;
                    soc.Water = data.Water;
                    soc.Sew = data.Sew;
                    soc.OtherCharges = data.OtherCharges;
                    soc.PTVFee = data.PtvFee;
                    soc.Maintenace = data.Maintenance;
                    db.Entry(soc).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return Content("2");
                }
            }
            return Content("0");
        }

        public JsonResult SocityChargesData(DTParameters param)
        {
            try
            {
                FOSDataModel db = new FOSDataModel();

                var dtsource = new List<IZSocietyChargesData>();
                dtsource = ManageIZSocityCharges.GetSocietyCharges();
                List<String> columnSearch = new List<string>();
                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }
                List<IZSocietyChargesData> data = ManageIZSocityCharges.GetResultSocity(param.Search.Value, param.SortOrder, param.Start, param.Length, dtsource, columnSearch);
                int count = ManageIZSocityCharges.CountSocity(param.Search.Value, dtsource, columnSearch);
                DTResult<IZSocietyChargesData> result = new DTResult<IZSocietyChargesData>
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
                IZSocietyChargesData data = new IZSocietyChargesData();
                var temp = db.TBl_IZSocietyCharges.Where(x => x.ID == ID).FirstOrDefault();
                data.ID = temp.ID;
                data.ConsumerID = Convert.ToInt32(temp.ConsumerID);
                data.StreetLight = temp.StreetLight;
                data.Garbage = temp.Garbage;
                data.Water = temp.Water;
                data.Sew = temp.Sew;
                data.PtvFee = temp.PTVFee;
                data.OtherCharges = temp.OtherCharges;
                data.Maintenance = temp.Maintenace;
                data.MeterNo = temp.meterNo;
                return Json(data);
            }
        }
    }
}