
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
    
public partial class AccessLog
{

    public int ID { get; set; }

    public int SaleOfficerID { get; set; }

    public System.DateTime LoginDate { get; set; }

    public Nullable<int> Status { get; set; }



    public virtual SaleOfficer SaleOfficer { get; set; }

}

}
