using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Shared
{
    public class IZInstallmentData
    {
        public int ID { get; set; }
        public string ReferenceNo { get; set; }
        public string Amount { get; set; }
        public string BillingMonth { get; set; }
        public string Instype { get; set; }
    }
}
