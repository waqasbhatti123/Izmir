using FOS.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Shared
{
    public class IZBillingParaMeterData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Domestic { get; set; }
        public string DomesticTemp { get; set; }
        public string Commercial { get; set; }
        public string Society { get; set; }
        public string TeleCommunication { get; set; }
        public string FPA { get; set; }
        public int MonthID { get; set; }
        public List<Tbl_IZBillingPeriod> Months { get; set; }
        public string MonthName { get; set; }
        public string ReadingStartDate { get; set; }
        public string BillIssueDate { get; set; }
        public string BillDueDate { get; set; }
    }
}
