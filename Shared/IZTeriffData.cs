using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Shared
{
    public class IZTeriffData
    {
        public int ID { get; set; }
        public string TeriffCode { get; set; }
        public string Active { get; set; }
        public DateTime Createat { get; set; }
        public int Createby { get; set; }
        public DateTime updateat { get; set; }
        public int updateby { get; set; }
    }
}
