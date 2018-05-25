using System;
using System.Collections.Generic;

namespace DCT.ReferenceData.Model
{
    public class LearningDelivery
    {
        public string LearnAimRef { get; set; }

        public string NotionalNVQLevelv2 { get; set; }
        
        public IEnumerable<LearningDeliveryCategory> LearningDeliveryCategories { get; set; }

        public IEnumerable<FrameworkAim> FrameworkAims { get; set; }

        public IEnumerable<AnnualValue> AnnualValues { get; set; }
    }
}
