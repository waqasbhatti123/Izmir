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
    public class ManageIZCSV
    {
        public static List<IZCSVData> GetPayment()
        {
            List<IZCSVData> Data = new List<IZCSVData>();
            try
            {
                using (FOSDataModel dbContext = new FOSDataModel())
                {
                    Data = dbContext.Tbl_IZPayments
                            .ToList().Select(
                                u => new IZCSVData
                                {
                                    ID = u.ID,
                                    Name = dbContext.Tbl_IZConsumers.Where(x => x.ID == u.ConsumerID).FirstOrDefault().OwnerName,
                                    Date = u.PaymentDate.Value.ToString("dd-MMM-yyyy"),
                                    MonthName = dbContext.Tbl_IZBillingPeriod.Where(x => x.ID == u.MonthID).FirstOrDefault().Name,
                                    BankName = dbContext.Tbl_IZBanks.Where(x => x.BankID == u.BankID).FirstOrDefault().BankName,
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

        public static List<IZCSVData> GetResultPay(string search, string sortOrder, int start, int length, List<IZCSVData> dtResult, List<string> columnFilters)
        {
            return FilterPayment(search, dtResult, columnFilters).SortBy(sortOrder).Skip(start).Take(length).ToList();
        }

        public static int CountPayment(string search, List<IZCSVData> dtResult, List<string> columnFilters)
        {
            return FilterPayment(search, dtResult, columnFilters).Count();
        }

        private static IQueryable<IZCSVData> FilterPayment(string search, List<IZCSVData> dtResult, List<string> columnFilters)
        {
            IQueryable<IZCSVData> results = dtResult.AsQueryable();

            results = results.Where(p => (search == null || (p.BankName != null && p.BankName.ToLower().Contains(search.ToLower())) ||
            (p.Amount != null && p.Amount.ToLower().Contains(search.ToLower())) || (p.Name != null && p.Name.ToLower().Contains(search.ToLower())))


                );

            return results;
        }
    }
}
