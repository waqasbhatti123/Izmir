using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Shared
{
    public class IZSocietyChargesData
    {
        public int ID { get; set; }
        public int ConsumerID { get; set; }
        public string RefNo { get; set; }
        public string MeterNo { get; set; }
        public string StreetLight { get; set; }
        public string Garbage { get; set; }
        public string Water { get; set; }
        public string Sew { get; set; }
        public string PtvFee { get; set; }
        public string OtherCharges { get; set; }
        public string Maintenance { get; set; }
        public List<IZConsumerData> Consumers { get; set; }
    }
}
