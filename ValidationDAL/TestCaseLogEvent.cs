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
    
    public partial class TestCaseLogEvent
    {
        public long TestCaseLogEventId { get; set; }
        public int TestCaseLogId { get; set; }
        public string EventName { get; set; }
        public Nullable<System.DateTime> EventStartTime { get; set; }
        public Nullable<System.DateTime> EventEndTime { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}