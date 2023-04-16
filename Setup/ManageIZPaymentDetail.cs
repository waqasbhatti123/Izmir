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
    public class ManageIZPaymentDetail
    {
        public static List<IZPaymentDetailData> GetPayment()
        {
            List<IZPaymentDetailData> Data = new List<IZPaymentDetailData>();
            try
            {
                using (FOSDataModel dbContext = new FOSDataModel())
                {
                    List<sp_IZPaymentDetail_Result> det = dbContext.sp_IZPaymentDetail().ToList();


                    foreach (var item in det)
                    {
                        var payment = new IZPaymentDetailData();
                        payment.ID = item.BillID;
                        payment.Name = item.OwnerName;
                        payment.RefNo = item.RefNo;
                        payment.HouseNo = item.Address;
                        payment.MonthName = Convert.ToDateTime(item.BillMonth).ToString("MMM-yyyy");
                        payment.DueDate = Convert.ToDateTime(item.BillDueDate).ToString("dd-MMM-yyyy");
                        if (item.PaymentDate == null)
                        {
                            payment.AfterDate = "";
                        }
                        else
                        {
                            payment.AfterDate = Convert.ToDateTime(item.PaymentDate).ToString("dd-MMM-yyyy");
                        }
                        payment.Payable = item.TotalBill;
                        payment.After = item.AfterBill;
                        Data.Add(payment);
                    }

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

        public static List<IZPaymentDetailData> GetResultPay(string search, string sortOrder, int start, int length, List<IZPaymentDetailData> dtResult, List<string> columnFilters)
        {
            return FilterPayment(search, dtResult, columnFilters).SortBy(sortOrder).Skip(start).Take(length).ToList();
        }

        public static int CountPayment(string search, List<IZPaymentDetailData> dtResult, List<string> columnFilters)
        {
            return FilterPayment(search, dtResult, columnFilters).Count();
        }

        private static IQueryable<IZPaymentDetailData> FilterPayment(string search, List<IZPaymentDetailData> dtResult, List<string> columnFilters)
        {
            IQueryable<IZPaymentDetailData> results = dtResult.AsQueryable();

            results = results.Where(p => (search == null || (p.RefNo != null && p.RefNo.ToLower().Contains(search.ToLower())) ||
            (p.HouseNo != null && p.HouseNo.ToLower().Contains(search.ToLower())) || (p.Name != null && p.Name.ToLower().Contains(search.ToLower()))
            || (p.Payable != null && p.Payable.ToLower().Contains(search.ToLower())) || (p.After != null && p.After.ToLower().Contains(search.ToLower())))


                );

            return results;
        }
    }
}
