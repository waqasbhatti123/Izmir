using FOS.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Shared
{
    public class IZHomeData
    {
        public int ID { get; set; }
        public string ReferenceNo { get; set; }
        public string OwnerName { get; set; }
        public string Adress { get; set; }
        public string MeterNo { get; set; }
        public decimal PreReading { get; set; }
        public decimal CurrentReading { get; set; }
        public decimal? UnitConsume { get; set; }
        public string ReadingFeadback { get; set; }
        public int BlockID { get; set; }
        public List<Tbl_IZBlocks> Blocks { get; set; }
        public int Count { get; set; }
        public string MonthName { get; set; }
        public string BlockName { get; set; }
        public string Image { get; set; }
        public int MonID { get; set; }
        public List<Tbl_IZBillingPeriod> Months { get; set; }

        public string PreMonthName { get; set; }

        public decimal Prereead { get; set; }
        public decimal Percentage { get; set; }

        public int CurrentConnect { get; set; }
        public int PreviConnect { get; set; }

        public string UnitNo { get; set; }

    }
}
