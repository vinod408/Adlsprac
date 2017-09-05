using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Models
{
    public class TestSuites
    {
        public int TestSuiteId { get; set; }
        public string TestSuite1 { get; set; }
        public string TestSuiteDescription { get; set; }
        public bool IsActive { get; set; }
        public string TestSuiteOwner { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}