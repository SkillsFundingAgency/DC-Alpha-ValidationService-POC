//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DCT.LARS.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class LARS_Version
    {
        public int MajorNumber { get; set; }
        public int MinorNumber { get; set; }
        public int MaintenanceNumber { get; set; }
        public string MainDataSchemaName { get; set; }
        public string RefDataSchemaName { get; set; }
        public System.DateTime ActivationDate { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public System.DateTime Created_On { get; set; }
        public string Created_By { get; set; }
        public System.DateTime Modified_On { get; set; }
        public string Modified_By { get; set; }
    }
}
