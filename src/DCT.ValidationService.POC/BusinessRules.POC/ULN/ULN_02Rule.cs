using BusinessRules.POC.Extensions;
using BusinessRules.POC.Interfaces;
using DCT.ILR.Model;
using System.Collections.Generic;
using System.Linq;

namespace BusinessRules.POC.ULN
{
    public class ULN_02Rule : IRule<MessageLearner>
    {
        private readonly IValidationErrorHandler<MessageLearner> _validationErrorHandler;
        private readonly IEnumerable<long> _fundModels = new long[] { 99, 10 };
        private const long _uln = 9999999999;

        public ULN_02Rule(IValidationErrorHandler<MessageLearner> validationErrorHandler)
        {
            _validationErrorHandler = validationErrorHandler;
        }

        public void Validate(MessageLearner objectToValidate)
        {
            foreach (var learningDelivery in objectToValidate.LearningDelivery.Where(ld => !Exclude(ld)))
            {
                if (ConditionMet(learningDelivery.FundModel, objectToValidate.ULN))
                {
                    _validationErrorHandler.Handle(objectToValidate, "ULN_02");
                }
            }
        }

        public bool ConditionMet(long fundModel, long uln)
        {
            return _fundModels.Contains(fundModel) && uln == _uln;
        }

        public bool Exclude(MessageLearnerLearningDelivery learningDelivery)
        {
            var fam = learningDelivery.LearningDeliveryFAMCodeForType("SOF");

            return fam != null && fam == "1";
        }
    }
}
