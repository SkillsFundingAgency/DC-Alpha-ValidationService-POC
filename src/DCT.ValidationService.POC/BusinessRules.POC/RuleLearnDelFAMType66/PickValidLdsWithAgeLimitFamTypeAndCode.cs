using BusinessRules.POC.Helpers;
using BusinessRules.POC.Helpers.Interface;
using BusinessRules.POC.Interfaces;
using DCT.ILR.Model;
using System.Collections.Generic;
using System.Linq;

namespace BusinessRules.POC.RuleLearnDelFAMType66
{
    public interface IPickValidLdsWithAgeLimitFamTypeAndCode : IShortRule<MessageLearner, List<MessageLearnerLearningDelivery>>
    {
    }

    public class PickValidLdsWithAgeLimitFamTypeAndCode : IPickValidLdsWithAgeLimitFamTypeAndCode
    {
        private readonly IDateHelper _dateHelper;
        private readonly IValidateLARNotionalNVQLevelRule _ValidateLARNotionalNVQLevelRule;

        public PickValidLdsWithAgeLimitFamTypeAndCode(IDateHelper dateHelper, IValidateLARNotionalNVQLevelRule validateLARNotionalNVQLevelRule)
        {
            _dateHelper = dateHelper;
            _ValidateLARNotionalNVQLevelRule = validateLARNotionalNVQLevelRule;
        }
        public List<MessageLearnerLearningDelivery> Evaluate(MessageLearner learner)
        {
            var result = new List<MessageLearnerLearningDelivery>();

            foreach (var learningDelivery in learner.LearningDelivery)
            {
                var learnerAgewithResToProgStartdate =
                    _dateHelper.GetAge(learningDelivery.LearnStartDate, learner.DateOfBirth);
                //pick learningdeliverys  with age greater than or equal to 24
                if (learnerAgewithResToProgStartdate < 24) continue;

                //pick lds with FAMCode and type
                if (learningDelivery.LearningDeliveryFAM.Count(ldFAM => ldFAM.LearnDelFAMCode == "1" && ldFAM.LearnDelFAMType == "FFI") <= 0) continue;

                //check and validate the LARSNVQ condition
                if (_ValidateLARNotionalNVQLevelRule.Evaluate(learningDelivery))
                    result.Add(learningDelivery);
            }

            return result;
        }
    }
}
