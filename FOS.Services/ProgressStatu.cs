
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
    
public partial class ProgressStatu
{

    public int ID { get; set; }

    public string Name { get; set; }

    public Nullable<int> FaulttypeId { get; set; }

    public Nullable<bool> Isdeleted { get; set; }

    public Nullable<System.DateTime> CreatedOn { get; set; }



    public virtual FaultType FaultType { get; set; }

}

}
