using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Models
{
    public class TestCaseLog
    {
        public int TestCaseLogId { get; set; }
        public int TestInstanceId { get; set; }
        public int TestCaseId { get; set; }
        public Nullable<int> ResultType { get; set; }
        public Nullable<System.DateTime> EventStartTime { get; set; }
        public Nullable<System.DateTime> EventEndTime { get; set; }
        public string Exception { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}