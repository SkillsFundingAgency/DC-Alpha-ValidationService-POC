using BusinessRules.POC.Interfaces;
using BusinessRules.POC.SharedRules;
using BusinessRules.POC.ValidationData.Interface;
using DCT.ILR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.POC.LearnStartDate
{
    public class LearnStartDate_03Rule : IRule<MessageLearner>
    {
        private readonly IValidationErrorHandler<MessageLearner> _validationErrorHandler;
        private readonly IValidationData _validationData;
        private readonly IDD07Rule _dd07Rule;

        public LearnStartDate_03Rule(IDD07Rule dd07Rule, IValidationData validationData, IValidationErrorHandler<MessageLearner> validationErrorHandler)
        {
            _dd07Rule = dd07Rule;
            _validationData = validationData;
            _validationErrorHandler = validationErrorHandler;
        }

        public void Validate(MessageLearner objectToValidate)
        {
            foreach (var learningDelivery in objectToValidate.LearningDelivery)
            {
                if (ConditionMet(learningDelivery.LearnStartDate, _validationData.AcademicYearEnd, learningDelivery.ProgType, _dd07Rule.Evaluate(learningDelivery)))
                {
                    _validationErrorHandler.Handle(objectToValidate, RuleNameConstants.LearnStartDate_03);
                }
            }
        }

        public bool ConditionMet(DateTime learnStartDate, DateTime academicYearEnd, long progType, string dd07)
        {
            return dd07 == ValidationConstants.N
                && progType != 24
                && learnStartDate > academicYearEnd;
        }        
    }
}
