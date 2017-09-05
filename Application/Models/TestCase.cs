using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Models
{
    public class TestCase
    {
        public int TestCaseId { get; set; }
        public int TestSuiteId { get; set; }
        public int PipelineStageId { get; set; }
        public int TestCaseTypeId { get; set; }
        public string TestCaseName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsObsolete { get; set; }
        public string ObsoleteReason { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}