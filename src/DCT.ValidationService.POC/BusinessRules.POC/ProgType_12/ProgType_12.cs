using BusinessRules.POC.ExternalData.Interface;
using BusinessRules.POC.Interfaces;
using DCT.ILR.Model;
using System.Collections.Generic;
using System.Linq;

namespace BusinessRules.POC.ProgType_12
{
    public class ProgType_12 : IRule<MessageLearner>
    {
        private readonly ILARSFrameworkAimComponentTypeRefData _larsFrameworkAimComponentTypeRefData;
        private readonly IValidationErrorHandler<MessageLearner> _validationErrorHandler;

        private readonly IEnumerable<int?> _excludedFrameworkAimComponentType = new HashSet<int?>() { 1, 2, 3 };
        private readonly IEnumerable<long> _allowedFundModels = new HashSet<long>() { 35, 36 };
        private readonly IEnumerable<int> _disallowedBasicSkillsTypes = new HashSet<int>() { 2, -1, 11, 12, 19, 20, 33, 34 };

        public ProgType_12(ILARSFrameworkAimComponentTypeRefData larsFrameworkAimComponentTypeRefData, IValidationErrorHandler<MessageLearner> validationErrorHandler)
        {
            _larsFrameworkAimComponentTypeRefData = larsFrameworkAimComponentTypeRefData;
            _validationErrorHandler = validationErrorHandler;
        }

        public void Validate(MessageLearner objectToValidate)
        {
            foreach (var learningDelivery in objectToValidate.LearningDelivery.Where(ld => !Exclude(_larsFrameworkAimComponentTypeRefData.Get(ld))))
            {
                if (ConditionMet(learningDelivery.FundModel, learningDelivery.AimType, learningDelivery.ProgType, learningDelivery.FworkCode, learningDelivery.PwayCode, 1)) // TODO: look up Basic Skills Type
                {
                    _validationErrorHandler.Handle(objectToValidate, RuleNameConstants.ULN_06);
                }
            }
        }

        public bool Exclude(int? frameworkAimComponentType)
        {
            return _excludedFrameworkAimComponentType.Contains(frameworkAimComponentType);
        }

        public bool ConditionMet(long fundModel, long aimType, long progType, long fworkCode, long pwayCode, int basicSkillsType)
        {
            return aimType == 3
                && progType == 2
                && fworkCode == 445
                && pwayCode == 1
                && _allowedFundModels.Contains(fundModel)
                && !_disallowedBasicSkillsTypes.Contains(basicSkillsType);

            //DD04
            //FAM
        }
    }
}
