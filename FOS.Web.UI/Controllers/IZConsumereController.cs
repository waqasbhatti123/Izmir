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
    public class IZConsumereController : Controller
    {
        // GET: IZConsumere
        [HttpGet]
        public ActionResult AddConsumer()
        {
            IZConsumerData con = new IZConsumerData();
            using (FOSDataModel db = new FOSDataModel())
            {
                con.Blocks = db.Tbl_IZBlocks.Where(x => x.Status == true).ToList();
                con.Triff = db.tbl_IZTeriffCode.Where(x => x.IsActive == true).ToList();
                con.MeterStatuses = db.Tbl_IZMeterStatus.Where(x => x.IsActive == true).ToList();
            }
            return View(con);
        }

        [HttpPost]
        public ActionResult AddConsumer(IZConsumerData data)
        {
            Tbl_IZConsumers con = new Tbl_IZConsumers();
            using (FOSDataModel db = new FOSDataModel())
            {
                if (data.ID == 0)
                {
                    con.RefNo = data.RefNO;
                    //con.PlotNo = data.PlotNo;
                    con.BlockID = Convert.ToInt32(data.BlocksID);
                    con.BlockName = db.Tbl_IZBlocks.Where(x => x.ID == data.BlocksID).FirstOrDefault().Name;
                    con.Address = data.PlotNo;
                    con.MemberNO = data.MemberShip;
                    con.AccNO = data.AccNO;
                    con.SubAcco = data.SubAccNo;
                    con.SubHead = data.SubHead;
                    con.TeriffID = data.TriffID;
                    con.Teriff = db.tbl_IZTeriffCode.Where(x => x.ID == data.TriffID).FirstOrDefault().teriffCode;
                    con.ConnectionType = data.Connectiontype;
                    if (data.ConnectionDate == null || data.ConnectionDate == "")
                    {
                        con.ConnectionDate = null;
                    }
                    else
                    {
                        con.ConnectionDate = Convert.ToDateTime(data.ConnectionDate);
                    }
                    con.OwnerName = data.OwnerName;
                    con.CNIC = data.CNIC;
                    con.PhoneNo = data.PhoneNO;
                    con.Filer = data.Filer;
                    con.NTN = data.NTN;
                    con.CoOwnerName = data.CoOwnerName;
                    con.CoOwnerCNIC = data.CoOwnerCNIC;
                    con.CoOwnerContact = data.CoOwnerContact;
                    con.PTV = data.PTV;
                    con.Status = true;
                    con.CellNo = data.CellNO;
                    con.MeterNo = data.MeterNo;
                    con.MeterType = data.MeterTypess;
                    con.ChallanNo = data.ChallanNo;
                    if (data.ChallanDate == null || data.ChallanDate == "")
                    {
                        con.ChallanDate = null;
                    }
                    else
                    {
                        con.ChallanDate = Convert.ToDateTime(data.ChallanDate);
                    }
                    con.MeterMonth = data.Month;
                    con.MF = Convert.ToDouble(data.MultiplyingFector);
                    con.currentReading = data.CurrentReading;
                    con.MeterStatusID = data.MeterStatusID;
                    db.Tbl_IZConsumers.Add(con);
                    db.SaveChanges();
                    return Content("1");
                }
                else
                {
                    Tbl_IZConsumers cons = db.Tbl_IZConsumers.Where(x => x.ID == data.ID).FirstOrDefault();
                    cons.RefNo = data.RefNO;
                    //con.PlotNo = data.PlotNo;
                    cons.BlockID = Convert.ToInt32(data.BlocksID);
                    cons.BlockName = db.Tbl_IZBlocks.Where(x => x.ID == data.BlocksID).FirstOrDefault().Name;
                    cons.Address = data.PlotNo;
                    cons.MemberNO = data.MemberShip;
                    cons.AccNO = data.AccNO;
                    cons.SubAcco = data.SubAccNo;
                    cons.SubHead = data.SubHead;
                    cons.TeriffID = data.TriffID;
                    cons.Teriff = db.tbl_IZTeriffCode.Where(x => x.ID == data.TriffID).FirstOrDefault().teriffCode;
                    cons.ConnectionType = data.Connectiontype;
                    if (data.ConnectionDate == null || data.ConnectionDate == "")
                    {
                        cons.ConnectionDate = null;
                    }
                    else
                    {
                        cons.ConnectionDate = Convert.ToDateTime(data.ConnectionDate);
                    }
                    cons.OwnerName = data.OwnerName;
                    cons.CNIC = data.CNIC;
                    cons.PhoneNo = data.PhoneNO;
                    cons.Filer = data.Filer;
                    cons.NTN = data.NTN;
                    cons.CoOwnerName = data.CoOwnerName;
                    cons.CoOwnerCNIC = data.CoOwnerCNIC;
                    cons.CoOwnerContact = data.CoOwnerContact;
                    cons.PTV = data.PTV;
                    cons.Status = true;
                    cons.CellNo = data.CellNO;
                    cons.MeterNo = data.MeterNo;
                    cons.MeterType = data.MeterTypess;
                    cons.ChallanNo = data.ChallanNo;
                    if (data.ChallanDate == null || data.ChallanDate == "")
                    {
                        cons.ChallanDate = null;
                    }
                    else
                    {
                        cons.ChallanDate = Convert.ToDateTime(data.ChallanDate);
                    }
                    cons.MeterMonth = data.Month;
                    cons.MF = Convert.ToDouble(data.MultiplyingFector);
                    cons.currentReading = data.CurrentReading;
                    cons.MeterStatusID = data.MeterStatusID;
                    db.Entry(cons).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return Content("2");
                }
            }

            return Content("0");
        }

        public JsonResult ConsumerDataHandler(DTParameters param)
        {
            try
            {
                var dtsource = new List<IZConsumerData>();
                dtsource = ManageCity.GetConsumerForGrid();
                List<String> columnSearch = new List<string>();
                foreach (var col in param.Columns)
                {
                    columnSearch.Add(col.Search.Value);
                }
                List<IZConsumerData> data = ManageCity.GetResultConsumer(param.Search.Value, param.SortOrder, param.Start, param.Length, dtsource, columnSearch);
                int count = ManageCity.CountConsumer(param.Search.Value, dtsource, columnSearch);
                DTResult<IZConsumerData> result = new DTResult<IZConsumerData>
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
            IZConsumerData mon = new IZConsumerData();
            using (FOSDataModel db = new FOSDataModel())
            {
                Tbl_IZConsumers con = db.Tbl_IZConsumers.Where(x => x.ID == ID).FirstOrDefault();
                mon.ID = con.ID;
                mon.RefNO = con.RefNo;
                mon.OwnerName = con.OwnerName;
                mon.BlocksID = Convert.ToInt32(con.BlockID);
                mon.PlotNo = con.Address;
                mon.AccNO = con.AccNO;
                mon.MemberShip = con.MemberNO;
                mon.SubAccNo = con.SubAcco;
                mon.SubHead = con.SubHead;
                mon.CNIC = con.CNIC;
                if (con.PhoneNo == null || con.PhoneNo == "")
                {
                    mon.PhoneNO = "";
                }
                else
                {
                    mon.PhoneNO = con.PhoneNo.ToString();
                }
                mon.CellNO = con.CellNo;
                mon.TriffID = Convert.ToInt32(con.TeriffID);
                mon.Connectiontype = con.ConnectionType;
                mon.CoOwnerName = con.CoOwnerName;
                if (con.CoOwnerCNIC == null || con.CoOwnerCNIC == "")
                {
                    mon.CoOwnerCNIC = "";
                }
                else
                {
                    mon.CoOwnerCNIC = con.CoOwnerCNIC.ToString();
                }
                mon.Filer = Convert.ToBoolean(con.Filer);
                mon.NTN = con.NTN;
                if (con.ConnectionDate == null)
                {
                    mon.ConnectionDate = "";
                }
                else
                {
                    mon.ConnectionDate = con.ConnectionDate.Value.ToString("dd-MMM-yyyy");
                }
                mon.PTV = con.PTV;
                mon.MeterNo = con.MeterNo;
                mon.MeterTypess = con.MeterType;
                mon.ChallanNo = con.ChallanNo;
                if (con.ChallanDate == null)
                {
                    mon.ChallanDate = "";
                }
                else
                {
                    mon.ChallanDate = con.ChallanDate.Value.ToString("dd-MMM-yyyy");
                }
                mon.Month = con.MeterMonth;
                mon.MultiplyingFector = Convert.ToDouble(con.MF);
                mon.CurrentReading = con.currentReading;
                mon.MeterStatusID = Convert.ToInt32(con.MeterStatusID);
            }
            return Json(mon, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckMeterInstall(string PlotNo)
        {
            List<IZConsumerData> data = new List<IZConsumerData>();
            using (FOSDataModel db = new FOSDataModel())
            {
                var meters = db.Tbl_IZConsumers.Where(x => x.Address == PlotNo).ToList();

                foreach (var item in meters)
                {
                    var da = new IZConsumerData();
                    da.MeterNo = item.MeterNo;
                    data.Add(da);
                }
            }
            return Json(data);
        }

    }
}