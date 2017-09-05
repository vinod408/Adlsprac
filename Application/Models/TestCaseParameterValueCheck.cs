using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Models
{
    public class TestCaseParameterValueCheck
    {
        public int TestCaseParameterValueCheckId { get; set; }
        public int TestCaseId { get; set; }
        public string TableName { get; set; }
        public string PrimaryKeyColumnName { get; set; }
        public string ValueCheckColumnName { get; set; }
        public string DestinationFilePath { get; set; }
        public Nullable<short> FileColumnsCount { get; set; }
        public string FileColumnsName { get; set; }
        public string SourceWhereClauseFilter { get; set; }
        public string DestinationWhereClauseFilter { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}