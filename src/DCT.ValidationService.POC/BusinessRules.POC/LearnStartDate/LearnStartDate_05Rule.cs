using BusinessRules.POC.Interfaces;
using DCT.ILR.Model;
using System;

namespace BusinessRules.POC.LearnStartDate
{
    public class LearnStartDate_05Rule : IRule<MessageLearner>
    {
        private readonly IValidationErrorHandler<MessageLearner> _validationErrorHandler;

        public LearnStartDate_05Rule(IValidationErrorHandler<MessageLearner> validationErrorHandler)
        {
            _validationErrorHandler = validationErrorHandler;
        }

        public void Validate(MessageLearner objectToValidate)
        {
            foreach (var learningDelivery in objectToValidate.LearningDelivery)
            {
                if (ConditionMet(objectToValidate.DateOfBirthNullable, learningDelivery.LearnStartDate))
                {
                    _validationErrorHandler.Handle(objectToValidate, RuleNameConstants.LearnStartDate_05);
                }
            }
        }

        public bool ConditionMet(DateTime? dateOfBirth, DateTime learnStartDate)
        {
            return dateOfBirth.HasValue
                && dateOfBirth.Value >= learnStartDate;
        }
    }
}
