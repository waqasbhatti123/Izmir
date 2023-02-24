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
    public class IZBlockController : Controller
    {
        // GET: IZBlock
        [HttpGet]
        public ActionResult Block()
        {
            return View();
        }
        
        public ActionResult AddBlock(IZBlockData data)
        {
            Tbl_IZBlocks bl = new Tbl_IZBlocks();
            using (FOSDataModel db = new FOSDataModel())
            {
                if (data.ID == 0)
                {
                    bl.Name = data.BlockName;
                    bl.Status = true;
                    db.Tbl_IZBlocks.Add(bl);
                    db.SaveChanges();
                    return Content("1");
                }
                else
                {
                    Tbl_IZBlocks blo = db.Tbl_IZBlocks.Where(x => x.ID == data.ID).FirstOrDefault();
                    blo.Name = data.BlockName;
                    blo.Status = true;
                    db.Entry(blo).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return Content("2");
                }
            }
            return Content("0");
        }

        public JsonResult GetBankData(DTParameters param)
        {
            try
            {
                FOSDataModel db = new FOSDataModel();

                var dtsource = new List<IZBlockData>();
                dtsource = ManageIZBlock.GetBanks();
                List<String> columnSearch = new List<string>();
                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }
                List<IZBlockData> data = ManageIZBlock.GetResultBank(param.Search.Value, param.SortOrder, param.Start, param.Length, dtsource, columnSearch);
                int count = ManageIZBlock.CountSocity(param.Search.Value, dtsource, columnSearch);
                DTResult<IZBlockData> result = new DTResult<IZBlockData>
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
            IZBlockData data = new IZBlockData();
            using (FOSDataModel db = new FOSDataModel())
            {
                Tbl_IZBlocks bank = db.Tbl_IZBlocks.Where(x => x.ID == ID).FirstOrDefault();
                data.ID = bank.ID;
                data.BlockName = bank.Name;
                return Json(data);
            }
        }
    }
}