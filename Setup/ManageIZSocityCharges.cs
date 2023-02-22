using FOS.DataLayer;
using FOS.Shared;
using Shared.Diagnostics.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace FOS.Setup
{
    public static class ManageIZSocityCharges
    {
        public static List<IZConsumerData> GetConsumer()
        {
            List<IZConsumerData> data = new List<IZConsumerData>();

            using (FOSDataModel db = new FOSDataModel())
            {
                data = db.Tbl_IZConsumers.Where(x => x.Status == true)
                    .Select
                    (
                        u => new IZConsumerData
                        {
                            ID = u.ID,
                            RefNO = u.RefNo
                        }
                    ).ToList();
            }
            data.Insert(0, new IZConsumerData
            {
                ID = 0,
                RefNO = "Select"
            });
            return data;
        }

        public static List<IZSocietyChargesData> GetSocietyCharges()
        {
            List<IZSocietyChargesData> ConData = new List<IZSocietyChargesData>();
            List<IZSocietyChargesData> Data = new List<IZSocietyChargesData>();
            try
            {
                int cou = 0;
                using (FOSDataModel dbContext = new FOSDataModel())
                {
                    Data = dbContext.TBl_IZSocietyCharges
                            .ToList().Select(
                                u => new IZSocietyChargesData
                                {
                                    ID = u.ID,
                                    RefNo = u.ConsumerRefNo,
                                    MeterNo = u.meterNo,
                                    StreetLight = u.StreetLight,
                                    Garbage = u.Garbage,
                                    Water = u.Water,
                                    Sew = u.Sew,
                                    PtvFee = u.PTVFee,
                                    OtherCharges = u.OtherCharges,
                                    Maintenance = u.Maintenace
                                }).ToList();


                    //List<sp_DisplaySheetReadingMeter_Result> cur = dbContext.sp_DisplaySheetReadingMeter(MonthID).ToList();

                    //foreach (var item in cur)
                    //{
                    //    var hoData = new IZHomeData();
                    //    hoData.ID = item.ID;
                    //    hoData.ReferenceNo = item.RefNo;
                    //    hoData.OwnerName = item.OwnerName;
                    //    hoData.Adress = item.Address;
                    //    hoData.MeterNo = item.MeterNo;
                    //    hoData.PreReading = Convert.ToDecimal(item.PreviousReading);
                    //    hoData.CurrentReading = Convert.ToDecimal(item.MeterReading);
                    //    hoData.UnitConsume = item.UnitConsumer;
                    //    hoData.ReadingFeadback = item.ReadingFeedback;
                    //    hoData.Image = item.Picture1;
                    //    Data.Add(hoData);
                    //}

                }
            }
            catch (Exception exp)
            {
                Log.Instance.Error(exp, "Month Name Load Failed");
                throw;
            }

            return Data;
        }

        public static List<IZSocietyChargesData> GetResultSocity(string search, string sortOrder, int start, int length, List<IZSocietyChargesData> dtResult, List<string> columnFilters)
        {
            return FilterSocity(search, dtResult, columnFilters).SortBy(sortOrder).Skip(start).Take(length).ToList();
        }

        public static int CountSocity(string search, List<IZSocietyChargesData> dtResult, List<string> columnFilters)
        {
            return FilterSocity(search, dtResult, columnFilters).Count();
        }

        private static IQueryable<IZSocietyChargesData> FilterSocity(string search, List<IZSocietyChargesData> dtResult, List<string> columnFilters)
        {
            IQueryable<IZSocietyChargesData> results = dtResult.AsQueryable();

            results = results.Where(p => (search == null || (p.RefNo != null && p.RefNo.ToLower().Contains(search.ToLower())))


                );

            return results;
        }
    }
}
