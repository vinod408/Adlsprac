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
    
    public partial class TestCaseParameterCubeRevenueAllUpsByDimension
    {
        public int TestCaseParameterCubeRevenueAllUpsByDimensionId { get; set; }
        public int TestCaseId { get; set; }
        public string SourceCube { get; set; }
        public string SourceMdxQuery { get; set; }
        public string DestinationMdxQuery { get; set; }
        public string SourceDimension { get; set; }
        public string DestinationDimension { get; set; }
        public string Measure { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}