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
    
    public partial class Asset
    {
        public System.Guid AssetID { get; set; }
        public string Barcode { get; set; }
        public string SerialNumber { get; set; }
        public string PMGuide { get; set; }
        public string AstID { get; set; }
        public string ChildAsset { get; set; }
        public string GeneralAssetDescription { get; set; }
        public string SecondaryAssetDescription { get; set; }
        public int Quantity { get; set; }
        public string Manufacturer { get; set; }
        public string ModelNumber { get; set; }
        public string Building { get; set; }
        public string Floor { get; set; }
        public string Corridor { get; set; }
        public string RoomNo { get; set; }
        public string MERNo { get; set; }
        public string EquipSystem { get; set; }
        public string Comments { get; set; }
        public bool Issued { get; set; }
        public System.Guid FacilitySiteID { get; set; }
    }
}
