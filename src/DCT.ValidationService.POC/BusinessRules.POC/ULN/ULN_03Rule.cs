using BusinessRules.POC.Extensions;
using BusinessRules.POC.FileData.Interface;
using BusinessRules.POC.Interfaces;
using DCT.ILR.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessRules.POC.ULN
{
    public class ULN_03Rule : IRule<MessageLearner>
    {
        private readonly IFileData _fileData;
        private readonly IValidationErrorHandler<MessageLearner> _validationErrorHandler;

        private readonly IEnumerable<long> _fundModels = new long[] { 25, 82, 35, 36, 81, 70 };
        private DateTime currentTeachingYearJanuaryFirst = new DateTime(2018, 1, 1);
        private const long _uln = 9999999999;

        public ULN_03Rule(IFileData fileData, IValidationErrorHandler<MessageLearner> validationErrorHandler)
        {
            _fileData = fileData;
            _validationErrorHandler = validationErrorHandler;
        }

        public void Validate(MessageLearner objectToValidate)
        {
            foreach (var learningDelivery in objectToValidate.LearningDelivery.Where(ld => !Exclude(ld)))
            {
                if (ConditionMet(learningDelivery.FundModel, objectToValidate.ULN, _fileData.FilePreparationDate))
                {
                    _validationErrorHandler.Handle(objectToValidate, "ULN_03");
                }
            }
        }

        public bool ConditionMet(long fundModel, long uln, DateTime filePreparationDate)
        {
            return _fundModels.Contains(fundModel) && uln == _uln && filePreparationDate < currentTeachingYearJanuaryFirst;
        }

        public bool Exclude(MessageLearnerLearningDelivery learningDelivery)
        {
            var fam = learningDelivery.LearningDeliveryFAMCodeForType(LearningDeliveryFAMTypeConstants.ACT);

            return fam != null && fam == "1";
        }
    }
}
