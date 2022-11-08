using FOS.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Shared
{
    public class IZMeterZeroUnit
    {
        public string RefNo { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string MeterNo { get; set; }
        public decimal? PreviousReading { get; set; }
        public decimal? PresentReadin { get; set; }
        public decimal? UnitConsumed { get; set; }
        public string Comments { get; set; }
        //public List<Sp_GetReadingByUnitzero> Data { get; set; }
    }
}
