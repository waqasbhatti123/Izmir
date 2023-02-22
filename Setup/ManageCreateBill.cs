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
    public class ManageCreateBill
    {
        public static List<IZCreateBillData> GetBillData(int BlockID)
        {
            List<IZCreateBillData> Data = new List<IZCreateBillData>();
            try
            {
                using (FOSDataModel dbContext = new FOSDataModel())
                {
                  var  Datas = dbContext.Tbl_IZCreateBill.Where(x => x.BlockID == BlockID).ToList();
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
                        var hoData = new IZCreateBillData();
                        hoData.ID = item.BillID;
                        hoData.ConsumerName = item.ConsumerName;
                        hoData.ReferenceNo = item.ReferenceNo;
                        hoData.TotalBill = Convert.ToDouble(item.TotalBill).ToString();
                        hoData.BillDueDate = Convert.ToDateTime(item.BillDueDate).ToString("dd-MMM-yyyy");
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

        public static List<IZCreateBillData> GetResultCreateBill(string search, string sortOrder, int start, int length, List<IZCreateBillData> dtResult, List<string> columnFilters)
        {
            return FilterBank(search, dtResult, columnFilters).SortBy(sortOrder).Skip(start).Take(length).ToList();
        }

        public static int CountBill(string search, List<IZCreateBillData> dtResult, List<string> columnFilters)
        {
            return FilterBank(search, dtResult, columnFilters).Count();
        }

        private static IQueryable<IZCreateBillData> FilterBank(string search, List<IZCreateBillData> dtResult, List<string> columnFilters)
        {
            IQueryable<IZCreateBillData> results = dtResult.AsQueryable();

            results = results.Where(p => (search == null || (p.ReferenceNo != null && p.ReferenceNo.ToLower().Contains(search.ToLower())))


                );

            return results;
        }
    }
}
