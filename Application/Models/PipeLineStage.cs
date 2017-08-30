using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Models
{
    public class PipeLineStage
    {
        public int PipelineStageId { get; set; }
        public string PipelineStage1
        {
            get; set;
        }
        public string PipelineStageDescription
        {
            get; set;
        }
        public DateTime CreatedDate
        {
            get; set;
        }
        public string CreatedBy
        {
            get; set;
        }
    }
}