using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Shared
{
    public class IZCreateBillData
    {
        public int ID { get; set; }
        public string Address { get; set; }
        public string ConsumerName { get; set; }
        public string AccountNo { get; set; }
        public string PlotSize { get; set; }
        public string ConnectionDate { get; set; }
        public string MeterStatus { get; set; }
        public string ReferenceNo { get; set; }
        public string MeterNo { get; set; }
        public string MeterType { get; set; }
        public string Connectionntype { get; set; }
        public string BillMonth { get; set; }
        public string BillDueDate { get; set; }
        public string BillIssueDate { get; set; }
        public string PreviousUnit { get; set; }
        public string PresentUnit { get; set; }
        public string MF { get; set; }
        public string UnitConsume { get; set; }
        public string Rates { get; set; }
        public string Amount { get; set; }
        public string WHT { get; set; }
        public string FPA { get; set; }
        public string Adjustment { get; set; }
        public string StreetLight { get; set; }
        public string Garbage { get; set; }
        public string Water { get; set; }
        public string PTVFee { get; set; }
        public string SewAquafer { get; set; }
        public string OtherCharges { get; set; }
        public string Advance { get; set; }
        public int BlockID { get; set; }
        public int ConsumerID { get; set; }
        public string Maintenance { get; set; }
        public string TotalBill { get; set; }
        public string AfterBill { get; set; }
        public string Unpaid { get; set; }
    }
}
