using BusinessRules.POC.Extensions;
using BusinessRules.POC.Helpers.Interface;
using BusinessRules.POC.Interfaces;
using BusinessRules.POC.SharedRules;
using BusinessRules.POC.ValidationData.Interface;
using DCT.ILR.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessRules.POC.DateOfBirth
{
    public class DateOfBirth_48Rule : IRule<MessageLearner>
    {
        private readonly IDD07Rule _dd07Rule;
        private readonly IDateHelper _dateHelper;
        private readonly IValidationData _validationData;
        private readonly IValidationErrorHandler<MessageLearner> _validationErrorHandler;

        public DateOfBirth_48Rule(IDD07Rule dd007Rule, IDateHelper dateHelper, IValidationData validationData, IValidationErrorHandler<MessageLearner> validationErrorHandler)
        {
            _dd07Rule = dd007Rule;
            _dateHelper = dateHelper;
            _validationData = validationData;
            _validationErrorHandler = validationErrorHandler;
        }

        public void Validate(MessageLearner objectToValidate)
        {
            if (!LearnerConditionMet(objectToValidate.DateOfBirthNullable))
            {
                return;
            }

            var sixteenthBirthday = objectToValidate.DateOfBirth.AddYears(16);
            var lastFridayJuneAcademicYearLearnerSixteen = _dateHelper.GetLastFridayInJuneOfAcademicYear(sixteenthBirthday);

            foreach (var learningDelivery in objectToValidate.LearningDelivery.Where(ld => !Exclude(ld.ProgType)))
            {
                if (DD07ConditionMet(_dd07Rule.Evaluate(learningDelivery))
                    && DD04ConditionMet(DD04(objectToValidate, learningDelivery), _validationData.ApprencticeProgAllowedStartDate, lastFridayJuneAcademicYearLearnerSixteen))
                {
                    _validationErrorHandler.Handle(objectToValidate, RuleNameConstants.DateOfBirth_48);
                }
            }            
        }

        public bool Exclude(long progType)
        {
            return progType == 25;
        }

        public bool LearnerConditionMet(DateTime? dateOfBirth)
        {
            return dateOfBirth.HasValue;
        }

        public bool DD07ConditionMet(string dd07)
        {
            return dd07 == ValidationConstants.Y;
        }

        public bool DD04ConditionMet(DateTime? dd04, DateTime apprenticeshipProgrammeAllowedStartDate, DateTime lastFridayJuneAcademicYearLearnerSixteen)
        {
            return dd04.HasValue
                && dd04 >= apprenticeshipProgrammeAllowedStartDate
                && dd04 <= lastFridayJuneAcademicYearLearnerSixteen;
        }

        public DateTime? DD04(MessageLearner learner, MessageLearnerLearningDelivery learningDelivery)
        {
            return learner.EarliestLearningDeliveryLearnStartDateFor(1, learningDelivery.ProgType, learningDelivery.FworkCode, learningDelivery.PwayCode);
        }
    }
}
