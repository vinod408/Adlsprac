using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Models
{
    public class TestCaseParameterRowCount
    {
        public int TestCaseParameterRowCountId { get; set; }
        public int TestCaseId { get; set; }
        public string TableName { get; set; }
        public string SourceFilePath { get; set; }
        public string DestinationFilePath { get; set; }
        public Nullable<short> FileColumnsCount { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}