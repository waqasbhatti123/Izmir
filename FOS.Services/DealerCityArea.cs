
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
    
public partial class DealerCityArea
{

    public int ID { get; set; }

    public int DealerID { get; set; }

    public int CityID { get; set; }

    public int AreaID { get; set; }

    public System.DateTime CreatedOn { get; set; }

    public int CreatedBy { get; set; }



    public virtual Area Area { get; set; }

    public virtual City City { get; set; }

    public virtual Dealer Dealer { get; set; }

}

}
