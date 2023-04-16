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
   public class ManageIZInstallment
    {
        public static List<IZInstallmentData> GetBanks()
        {
            List<IZInstallmentData> Data = new List<IZInstallmentData>();
            try
            {
                using (FOSDataModel dbContext = new FOSDataModel())
                {
                    Data = dbContext.Tbl_IZInstallment
                            .ToList().Select(
                                u => new IZInstallmentData
                                {
                                    ID = u.ID,
                                    ReferenceNo = u.ReferenceNo,
                                    Amount = u.Amount
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
    

    public static List<IZInstallmentData> GetResultBank(string search, string sortOrder, int start, int length, List<IZInstallmentData> dtResult, List<string> columnFilters)
    {
        return FilterBank(search, dtResult, columnFilters).SortBy(sortOrder).Skip(start).Take(length).ToList();
    }

    public static int CountSocity(string search, List<IZInstallmentData> dtResult, List<string> columnFilters)
    {
        return FilterBank(search, dtResult, columnFilters).Count();
    }

    private static IQueryable<IZInstallmentData> FilterBank(string search, List<IZInstallmentData> dtResult, List<string> columnFilters)
    {
        IQueryable<IZInstallmentData> results = dtResult.AsQueryable();

        results = results.Where(p => (search == null || (p.ReferenceNo != null && p.ReferenceNo.ToLower().Contains(search.ToLower()))));

        return results;
    }
    }

}
