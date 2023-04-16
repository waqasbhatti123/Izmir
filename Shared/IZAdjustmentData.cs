using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Shared
{
    public class IZAdjustmentData
    {
        public int ID { get; set; }
        public string ReferenceNo { get; set; }
        public string Amount { get; set; }
        public string BillingMonth { get; set; }
        public string Comments { get; set; }
        public string Adjtype { get; set; }
    }
}
