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
    public class IZTeriffCodeController : Controller
    {
        // GET: IZTeriffCode
        [HttpGet]
        public ActionResult IZTeriff()
        {
            return View();
        }
        public ActionResult IZTeriffCode(IZTeriffData data )
        {
            tbl_IZTeriffCode tb = new tbl_IZTeriffCode();
            using(FOSDataModel db=new FOSDataModel())
            {
                if (data.ID == 0)
                {
                    tb.teriffCode = data.TeriffCode;
                    tb.IsActive = true;
                    tb.CreatedAt = DateTime.Now;
                    tb.CreatedBy = null;
                    tb.UpdatedAt = DateTime.Now;
                    tb.Updateby = null;
                    db.tbl_IZTeriffCode.Add(tb);
                    db.SaveChanges();
                    return Content("1");
                }
                else
                {
                    tbl_IZTeriffCode bll = db.tbl_IZTeriffCode.Where(x => x.ID == data.ID).FirstOrDefault();
                    bll.teriffCode = data.TeriffCode;
                    bll.IsActive = true;
                    bll.CreatedAt = bll.CreatedAt;
                    bll.CreatedBy = null;
                    bll.UpdatedAt = DateTime.Now;
                    bll.Updateby = null;
                    db.Entry(bll).State = System.Data.Entity.EntityState.Modified;
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

                var dtsource = new List<IZTeriffData>();
                dtsource = ManageIZTeriff.GetBanks();
                List<String> columnSearch = new List<string>();
                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }
                List<IZTeriffData> data = ManageIZTeriff.GetResultBank(param.Search.Value, param.SortOrder, param.Start, param.Length, dtsource, columnSearch);
                int count = ManageIZTeriff.CountSocity(param.Search.Value, dtsource, columnSearch);
                DTResult<IZTeriffData> result = new DTResult<IZTeriffData>
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
            IZTeriffData data = new IZTeriffData();
            using (FOSDataModel db = new FOSDataModel())
            {
                tbl_IZTeriffCode bs = db.tbl_IZTeriffCode.Where(x => x.ID == ID).FirstOrDefault();
                data.ID = bs.ID;
                data.TeriffCode = bs.teriffCode;
                data.Createat = Convert.ToDateTime(bs.CreatedAt);
                
                return Json(data);
            }
        }


    }
}