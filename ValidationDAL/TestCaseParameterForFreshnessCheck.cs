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
    
    public partial class TestCaseParameterForFreshnessCheck
    {
        public int TestCaseParameterForFreshnessCheckId { get; set; }
        public int TestCaseId { get; set; }
        public string Source { get; set; }
        public string SourceQuery { get; set; }
        public string DestinationQuery { get; set; }
        public string SourceColumn { get; set; }
        public string DestinationColumn { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
