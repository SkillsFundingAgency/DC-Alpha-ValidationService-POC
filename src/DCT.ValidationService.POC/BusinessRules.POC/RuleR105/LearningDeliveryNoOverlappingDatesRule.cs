using BusinessRules.POC.Helpers;
using DCT.ILR.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessRules.POC.RuleR105
{
    public class LearningDeliveryNoOverlappingDatesRule : ILearningDeliveryNoOverlappingDatesRule
    {
        public LearningDeliveryNoOverlappingDatesRule()
        {

        }

        public bool Evaluate(IEnumerable<MessageLearnerLearningDeliveryLearningDeliveryFAM> learningDeliveriesFams)
        {
            if (learningDeliveriesFams == null)
            {
                return false;
            }

            var count = learningDeliveriesFams.Count();

            //var overlappingDatesLDFAMs = new List<MessageLearnerLearningDeliveryLearningDeliveryFAM>();
            for (int i = 0; i < count; i++)
            {
               
                var ldFAMOuterLevel = learningDeliveriesFams.ElementAt(i);

                var outLevelRange = new Range<DateTime>(ldFAMOuterLevel.LearnDelFAMDateFrom,
                      ldFAMOuterLevel.LearnDelFAMDateTo);

                for (int j = i + 1; j < learningDeliveriesFams.Count(); j++)
                {
                    var ldFAMInnerLevel = learningDeliveriesFams.ElementAt(j);
                    //if the FAMCode is same then skip this
                    if (ldFAMOuterLevel.LearnDelFAMCode == ldFAMInnerLevel.LearnDelFAMCode) continue;
                   
                    var innerLevelRange = new Range<DateTime>(ldFAMInnerLevel.LearnDelFAMDateFrom,
                        ldFAMInnerLevel.LearnDelFAMDateTo);

                    //find intersection periods
                    if (outLevelRange.IsOverlapped(innerLevelRange)) return true;

                }

            }

            return false;


        }
    }

}
