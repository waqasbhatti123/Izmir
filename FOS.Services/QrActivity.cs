
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
    
public partial class QrActivity
{

    public int QrID { get; set; }

    public string Title { get; set; }

    public string Detail { get; set; }

    public Nullable<System.DateTime> DueDate { get; set; }

    public string QrCode { get; set; }

    public int ActivityType { get; set; }

    public Nullable<int> CityID { get; set; }

    public int Priority { get; set; }

    public int Status { get; set; }

    public System.DateTime CreatedOn { get; set; }

    public int CreatedBy { get; set; }

    public Nullable<System.DateTime> UpdatedOn { get; set; }

    public Nullable<int> UpdatedBy { get; set; }



    public virtual City City { get; set; }

}

}