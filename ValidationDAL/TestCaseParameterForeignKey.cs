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
    
    public partial class TestCaseParameterForeignKey
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
