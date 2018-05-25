using BusinessRules.POC.Extensions;
using BusinessRules.POC.FileData.Interface;
using BusinessRules.POC.Interfaces;
using DCT.ILR.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessRules.POC.ULN
{
    public class ULN_06Rule : IRule<MessageLearner>
    {
        private readonly IFileData _fileData;
        private readonly IValidationErrorHandler<MessageLearner> _validationErrorHandler;

        private readonly IEnumerable<long> _fundModels = new long[] { 25, 82, 35, 36, 81, 70 };
        private DateTime currentTeachingYearJanuaryFirst = new DateTime(2018, 1, 1);
        private const long _uln = 9999999999;

        public ULN_06Rule(IFileData fileData, IValidationErrorHandler<MessageLearner> validationErrorHandler)
        {
            _fileData = fileData;
            _validationErrorHandler = validationErrorHandler;
        }

        public void Validate(MessageLearner objectToValidate)
        {
            foreach (var learningDelivery in objectToValidate.LearningDelivery.Where(ld => !Exclude(ld)))
            {
                if (ConditionMet(
                    learningDelivery.FundModel,
                    learningDelivery.LearningDeliveryFAMCodeForType("ADL"),
                    objectToValidate.ULN,
                    _fileData.FilePreparationDate,
                    learningDelivery.LearnStartDateNullable,
                    learningDelivery.LearnPlanEndDateNullable,
                    learningDelivery.LearnActEndDateNullable))
                {
                    _validationErrorHandler.Handle(objectToValidate, RuleNameConstants.ULN_06);
                }
            }            
        }

        public bool ConditionMet(long fundModel, string adlFamCode, long uln, DateTime filePreparationDate, DateTime? learnStartDate, DateTime? learnPlanEndDate, DateTime? learnActEndDate)
        {
            return FundModelConditionMet(fundModel, adlFamCode)
                && FilePreparationDateConditionMet(filePreparationDate)
                && LearningDatesConditionMet(learnStartDate, learnPlanEndDate, learnActEndDate, filePreparationDate)
                && UlnConditionMet(uln);
        }

        public bool FundModelConditionMet(long fundModel, string adlFamCode)
        {
            return _fundModels.Contains(fundModel)
                || (fundModel == 99 && adlFamCode == "1");
        }

        public bool FilePreparationDateConditionMet(DateTime filePreparationDate)
        {
            return filePreparationDate >= currentTeachingYearJanuaryFirst;
        }
        
        public bool LearningDatesConditionMet(DateTime? learnStartDate, DateTime? learnPlanEndDate, DateTime? learnActEndDate, DateTime filePreparationDate)
        {
            return ((learnPlanEndDate - learnStartDate).Value.TotalDays >= 5
                || (learnActEndDate.HasValue && (learnActEndDate - learnStartDate).Value.TotalDays >= 5))
                && (filePreparationDate - learnStartDate).Value.TotalDays <= 60;            
        }

        public bool UlnConditionMet(long uln)
        {
            return uln == _uln;
        }

        public bool Exclude(MessageLearnerLearningDelivery learningDelivery)
        {
            return LearningDeliveryFAMCodeAndTypeMatch(learningDelivery, LearningDeliveryFAMTypeConstants.LDM, "034") 
                || LearningDeliveryFAMCodeAndTypeMatch(learningDelivery, LearningDeliveryFAMTypeConstants.ACT, "1");
        }

        public bool LearningDeliveryFAMCodeAndTypeMatch(MessageLearnerLearningDelivery learningDelivery, string type, string code)
        {
            var fam = learningDelivery.LearningDeliveryFAMCodeForType(type);

            return fam != null && fam == code;
        }
    }
}
