
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
    
public partial class ComplaintNotification
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public ComplaintNotification()
    {

        this.NotificationSeens = new HashSet<NotificationSeen>();

    }


    public int ID { get; set; }

    public Nullable<int> JobID { get; set; }

    public Nullable<int> JobDetailID { get; set; }

    public Nullable<int> ComplaintHistoryID { get; set; }

    public Nullable<bool> IsSiteIDChanged { get; set; }

    public Nullable<bool> IsSiteCodeChanged { get; set; }

    public Nullable<bool> IsFaulttypeIDChanged { get; set; }

    public Nullable<bool> IsFaulttypeDetailIDChanged { get; set; }

    public Nullable<bool> IsPriorityIDChanged { get; set; }

    public Nullable<bool> IsComplaintStatusIDChanged { get; set; }

    public Nullable<bool> IsPersonNameChanged { get; set; }

    public Nullable<bool> IsPicture1Changed { get; set; }

    public Nullable<bool> IsPicture2Changed { get; set; }

    public Nullable<bool> IsPicture3Changed { get; set; }

    public Nullable<bool> IsProgressStatusIDChanged { get; set; }

    public Nullable<bool> IsFaulttypeDetailRemarksChanged { get; set; }

    public Nullable<bool> IsProgressStatusRemarksChanged { get; set; }

    public Nullable<bool> IsAssignedSaleOfficerChanged { get; set; }

    public Nullable<bool> IsUpdateRemarksChanged { get; set; }

    public Nullable<System.DateTime> CreatedDate { get; set; }

    public Nullable<bool> IsSeen { get; set; }



    public virtual Job Job { get; set; }

    public virtual JobsDetail JobsDetail { get; set; }

    public virtual Tbl_ComplaintHistory Tbl_ComplaintHistory { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<NotificationSeen> NotificationSeens { get; set; }

}

}
