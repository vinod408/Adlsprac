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
    
    public partial class Ad
    {
        public int AdId { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public string ThumbnailURL { get; set; }
        public System.DateTime PostedDate { get; set; }
        public Nullable<int> Category { get; set; }
        public string Phone { get; set; }
    }
}
