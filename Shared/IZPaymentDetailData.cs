using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Shared
{
    public class IZPaymentDetailData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string RefNo { get; set; }
        public string HouseNo { get; set; }
        public string MonthName { get; set; }
        public string DueDate { get; set; }
        public string AfterDate { get; set; }
        public string Payable { get; set; }
        public string After { get; set; }
    }
}
