//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ValidationDAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class TestInstance
    {
        public int TestInstanceId { get; set; }
        public Nullable<int> TestInstanceDateKey { get; set; }
        public string TestInstanceName { get; set; }
        public string Description { get; set; }
        public bool IsMailSent { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
