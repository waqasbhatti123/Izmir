using FOS.DataLayer;
using FOS.Shared;
using NLog.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace FOS.Setup
{
    public class ManageZeroMeter
    {

        public ManageZeroMeter()
        {

        }


        public static List<IZMeterZeroUnit> GetDisplaySheetForGrid(int MonthID)
        {
            List<IZMeterZeroUnit> ConData = new List<IZMeterZeroUnit>();
            List<IZMeterZeroUnit> Data = new List<IZMeterZeroUnit>();
            try
            {
                int cou = 0;
                using (FOSDataModel dbContext = new FOSDataModel())
                {
                    List<Sp_GetReadingByUnitzero_Result> cur = dbContext.Sp_GetReadingByUnitzero(MonthID).ToList();

                    foreach (var item in cur)
                    {
                        var hoData = new IZMeterZeroUnit();
                        hoData.RefNo = item.RefNo;
                        hoData.Name = item.Name;
                        hoData.Address = item.Address;
                        hoData.MeterNo = item.MeterNo;
                        hoData.PreviousReading = Convert.ToDecimal(item.PreviousReading);
                        hoData.PresentReadin = Convert.ToDecimal(item.PresentReadin);
                        hoData.UnitConsumed = item.UnitConsumed;
                        hoData.Comments = item.Comments;
                        Data.Add(hoData);
                    }

                }
            }
            catch (Exception exp)
            {
                //Log.Instance.Error(exp, "Month Name Load Failed");
                throw;
            }

            return Data;
        }

        public static List<IZMeterZeroUnit> GetResultReadingCurrentMonth(string search, string sortOrder, int start, int length, List<IZMeterZeroUnit> dtResult, List<string> columnFilters)
        {
            return FilterResultCurrentMonth(search, dtResult, columnFilters).SortBy(sortOrder).Skip(start).Take(length).ToList();
        }

        public static int CountCurrentMonthReading(string search, List<IZMeterZeroUnit> dtResult, List<string> columnFilters)
        {
            return FilterResultCurrentMonth(search, dtResult, columnFilters).Count();
        }

        private static IQueryable<IZMeterZeroUnit> FilterResultCurrentMonth(string search, List<IZMeterZeroUnit> dtResult, List<string> columnFilters)
        {
            IQueryable<IZMeterZeroUnit> results = dtResult.AsQueryable();

            results = results.Where(p => (search == null || (p.RefNo != null && p.RefNo.ToLower().Contains(search.ToLower())))


                );

            return results;
        }

    }
}
