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
    
    public partial class LARS_StandardFunding
    {
        public int StandardCode { get; set; }
        public string FundingCategory { get; set; }
        public System.DateTime EffectiveFrom { get; set; }
        public Nullable<System.DateTime> EffectiveTo { get; set; }
        public Nullable<int> BandNumber { get; set; }
        public Nullable<decimal> CoreGovContributionCap { get; set; }
        public Nullable<decimal> C1618Incentive { get; set; }
        public Nullable<decimal> SmallBusinessIncentive { get; set; }
        public Nullable<decimal> AchievementIncentive { get; set; }
        public string FundableWithoutEmployer { get; set; }
        public System.DateTime Created_On { get; set; }
        public string Created_By { get; set; }
        public System.DateTime Modified_On { get; set; }
        public string Modified_By { get; set; }
    
        public virtual LARS_Standard LARS_Standard { get; set; }
    }
}
