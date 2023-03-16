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
    public class ManagePaymentReceive
    {
        public static List<IZPaymentReceiveData> GetBillData(int BlockID, string BillName)
        {
            List<IZPaymentReceiveData> Data = new List<IZPaymentReceiveData>();
            try
            {
                using (FOSDataModel dbContext = new FOSDataModel())
                {
                    var Datas = dbContext.Tbl_IZCreateBill.Where(x => x.BlockID == BlockID && x.BillMonth == BillName).ToList();
                    //.ToList().Select(
                    //    u => new IZCreateBillData
                    //    {
                    //        ID = u.BillID,
                    //        ConsumerName = u.ConsumerName,
                    //        ReferenceNo = u.ReferenceNo,
                    //        TotalBill = Convert.ToInt32(u.TotalBill).ToString(),
                    //        BillDueDate = Convert.ToDateTime(u.BillDueDate).ToString("dd-MMM-yyyy")
                    //    }).ToList();


                    //List<sp_DisplaySheetReadingMeter_Result> cur = dbContext.sp_DisplaySheetReadingMeter(MonthID).ToList();

                    foreach (var item in Datas)
                    {
                        var hoData = new IZPaymentReceiveData();
                        hoData.ID = item.BillID;
                        hoData.Name = item.ConsumerName;
                        hoData.RefNo = item.ReferenceNo;
                        hoData.Payable = Convert.ToDouble(item.TotalBill).ToString();
                        hoData.After = Convert.ToDouble(item.AfterBill).ToString();
                        hoData.BillingMonthName = Convert.ToDateTime(item.BillMonth).ToString("MMM-yyyy");
                        hoData.PlotNo = item.PlotNo;
                        Data.Add(hoData);
                    }

                }
            }
            catch (Exception exp)
            {
                Log.Instance.Error(exp, "Month Name Load Failed");
                throw;
            }

            return Data;
        }

        public static List<IZPaymentReceiveData> GetResultBillData(string search, string sortOrder, int start, int length, List<IZPaymentReceiveData> dtResult, List<string> columnFilters)
        {
            return FilterGetBillData(search, dtResult, columnFilters).SortBy(sortOrder).Skip(start).Take(length).ToList();
        }

        public static int CountBillData(string search, List<IZPaymentReceiveData> dtResult, List<string> columnFilters)
        {
            return FilterGetBillData(search, dtResult, columnFilters).Count();
        }

        private static IQueryable<IZPaymentReceiveData> FilterGetBillData(string search, List<IZPaymentReceiveData> dtResult, List<string> columnFilters)
        {
            IQueryable<IZPaymentReceiveData> results = dtResult.AsQueryable();

            results = results.Where(p => (search == null || (p.RefNo != null && p.RefNo.ToLower().Contains(search.ToLower())))


                );

            return results;
        }

        public static List<IZMonthCloseData> GetMonth()
        {
            List<IZMonthCloseData> Data = new List<IZMonthCloseData>();
            try
            {
                using (FOSDataModel dbContext = new FOSDataModel())
                {
                    var Datas = dbContext.Tbl_IZBillingPeriod.ToList();
                    //.ToList().Select(
                    //    u => new IZCreateBillData
                    //    {
                    //        ID = u.BillID,
                    //        ConsumerName = u.ConsumerName,
                    //        ReferenceNo = u.ReferenceNo,
                    //        TotalBill = Convert.ToInt32(u.TotalBill).ToString(),
                    //        BillDueDate = Convert.ToDateTime(u.BillDueDate).ToString("dd-MMM-yyyy")
                    //    }).ToList();


                    //List<sp_DisplaySheetReadingMeter_Result> cur = dbContext.sp_DisplaySheetReadingMeter(MonthID).ToList();

                    foreach (var item in Datas)
                    {
                        var hoData = new IZMonthCloseData();
                        hoData.ID = item.ID;
                        hoData.Name = item.Name;
                        Data.Add(hoData);
                    }

                }
            }
            catch (Exception exp)
            {
                Log.Instance.Error(exp, "Month Name Load Failed");
                throw;
            }

            return Data;
        }

        public static List<IZBankData> GetBank()
        {
            List<IZBankData> Data = new List<IZBankData>();
            try
            {
                using (FOSDataModel dbContext = new FOSDataModel())
                {
                    var Datas = dbContext.Tbl_IZBanks.Where(x => x.IsActive == true).ToList();
                    //.ToList().Select(
                    //    u => new IZCreateBillData
                    //    {
                    //        ID = u.BillID,
                    //        ConsumerName = u.ConsumerName,
                    //        ReferenceNo = u.ReferenceNo,
                    //        TotalBill = Convert.ToInt32(u.TotalBill).ToString(),
                    //        BillDueDate = Convert.ToDateTime(u.BillDueDate).ToString("dd-MMM-yyyy")
                    //    }).ToList();


                    //List<sp_DisplaySheetReadingMeter_Result> cur = dbContext.sp_DisplaySheetReadingMeter(MonthID).ToList();

                    foreach (var item in Datas)
                    {
                        var hoData = new IZBankData();
                        hoData.ID = item.BankID;
                        hoData.BankName = item.BankName;
                        Data.Add(hoData);
                    }

                    Data.Insert(0, new IZBankData
                    {
                        ID = 0,
                        BankName = "Select"
                        
                    });
                }
            }
            catch (Exception exp)
            {
                Log.Instance.Error(exp, "Month Name Load Failed");
                throw;
            }

            return Data;
        }

    }
}
