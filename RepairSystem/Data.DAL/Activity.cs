//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Activity
    {
        public long activity_id { get; set; }
        public Nullable<int> sequance_no { get; set; }
        public string decription { get; set; }
        public string result { get; set; }
        public string status { get; set; }
        public System.DateTime registration_date { get; set; }
        public Nullable<System.DateTime> finish_cancel_date { get; set; }
        public string activity_code { get; set; }
        public long request_id { get; set; }
        public string worker { get; set; }
    
        public virtual ActivityType ActivityType { get; set; }
        public virtual Personel Personel { get; set; }
        public virtual Request Request { get; set; }
    }
}
