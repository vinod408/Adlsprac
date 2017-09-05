using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Models
{
    public class TestCaseParameterForeignKey
    {
        public int TestCaseParameterForeignKeyId { get; set; }
        public int TestCaseId { get; set; }
        public string ForeignKeyTableName { get; set; }
        public string ForeignKeyColumn { get; set; }
        public string PrimaryKeyTableName { get; set; }
        public string PrimaryKeyColumn { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}