//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FOS.DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tbl_IZBillingPeriod
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tbl_IZBillingPeriod()
        {
            this.Tbl_IZBillingParameter = new HashSet<Tbl_IZBillingParameter>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> ReadingStart { get; set; }
        public Nullable<System.DateTime> BillIssueDate { get; set; }
        public Nullable<System.DateTime> BillDueDate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tbl_IZBillingParameter> Tbl_IZBillingParameter { get; set; }
    }
}
