using DCT.ILR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.POC.Extensions
{
    public static class MessageLearnerExtensions
    {
        public static DateTime? EarliestLearningDeliveryLearnStartDateFor(this MessageLearner learner, long aimType, long progType, long fworkCode, long pwayCode)
        {
            if (learner.LearningDelivery == null)
            {
                return null;
            }

            return learner.LearningDelivery
                .Where(ld => ld.AimType == aimType && ld.ProgType == progType && ld.FworkCode == fworkCode && ld.PwayCode == pwayCode)
                .OrderBy(ld => ld.LearnStartDate)
                .Select(ld => (DateTime?)ld.LearnStartDate)
                .FirstOrDefault();
        }
    }
}
