using BusinessRules.POC.Interfaces;
using BusinessRules.POC.ValidationData.Interface;
using DCT.ILR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.POC.LearnStartDate
{
    public class LearnStartDate_02Rule : IRule<MessageLearner>
    {
        private readonly IValidationErrorHandler<MessageLearner> _validationErrorHandler;
        private readonly IValidationData _validationData;

        public LearnStartDate_02Rule(IValidationData validationData, IValidationErrorHandler<MessageLearner> validationErrorHandler)
        {
            _validationErrorHandler = validationErrorHandler;
            _validationData = validationData;
        }

        public void Validate(MessageLearner objectToValidate)
        {
            foreach (var learningDelivery in objectToValidate.LearningDelivery)
            {
                if (ConditionMet(learningDelivery.LearnStartDate, _validationData.AcademicYearStart))
                {
                    _validationErrorHandler.Handle(objectToValidate, RuleNameConstants.LearnStartDate_02);
                }
            }
        }

        public bool ConditionMet(DateTime learnStartDate, DateTime academicYearStart)
        {
            return learnStartDate < academicYearStart.AddYears(-10);
        }
    }
}
