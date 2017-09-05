using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Models
{
    public class TestCaseParameterPrimaryKey
    {
        public int TestCaseParameterPrimaryKeyId { get; set; }
        public int TestCaseId { get; set; }
        public string TableName { get; set; }
        public string PrimaryKeyColumn { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}