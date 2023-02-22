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
    public class ManageIZBank
    {
        public static List<IZBankData> GetBanks()
        {
            List<IZBankData> Data = new List<IZBankData>();
            try
            {
                using (FOSDataModel dbContext = new FOSDataModel())
                {
                    Data = dbContext.Tbl_IZBanks
                            .ToList().Select(
                                u => new IZBankData
                                {
                                    ID = u.BankID,
                                    BankName = u.BankName,
                                    AccountNo = u.AccountNo
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

        public static List<IZBankData> GetResultBank(string search, string sortOrder, int start, int length, List<IZBankData> dtResult, List<string> columnFilters)
        {
            return FilterBank(search, dtResult, columnFilters).SortBy(sortOrder).Skip(start).Take(length).ToList();
        }

        public static int CountSocity(string search, List<IZBankData> dtResult, List<string> columnFilters)
        {
            return FilterBank(search, dtResult, columnFilters).Count();
        }

        private static IQueryable<IZBankData> FilterBank(string search, List<IZBankData> dtResult, List<string> columnFilters)
        {
            IQueryable<IZBankData> results = dtResult.AsQueryable();

            results = results.Where(p => (search == null || (p.BankName != null && p.BankName.ToLower().Contains(search.ToLower())))


                );

            return results;
        }
    }
}
