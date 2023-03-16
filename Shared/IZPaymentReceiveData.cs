using FOS.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Shared
{
    public class IZPaymentReceiveData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string RefNo { get; set; }
        public string PlotNo { get; set; }
        public string Payable { get; set; }
        public string After { get; set; }
        public string BillingMonthName { get; set; }
        public string  BillingID { get; set; }
        public int BlockID { get; set; }
        public List<Tbl_IZBlocks> Blocks { get; set; }
        public string BlockName { get; set; }
        public int MonthID { get; set; }
        public List<IZMonthCloseData> Mots { get; set; }
        public string TransactionType { get; set; }
        public int BankID { get; set; }
        public List<IZBankData> Banks { get; set; }
        public string BankName { get; set; }
        public string DateExtended { get; set; }
        public string PaymentDate { get; set; }
        public string Amount { get; set; }
    }
}
