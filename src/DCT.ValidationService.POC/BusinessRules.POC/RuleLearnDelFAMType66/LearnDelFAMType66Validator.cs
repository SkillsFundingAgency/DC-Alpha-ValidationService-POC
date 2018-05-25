using BusinessRules.POC.Interfaces;
using System.Linq;
using Autofac.Features.AttributeFilters;
using BusinessRules.POC.RuleLearnDelFAMType66.ExclusionRules;
using DCT.ILR.Model;

namespace BusinessRules.POC.RuleLearnDelFAMType66
{
    public class LearnDelFAMType66Validator : IRule<MessageLearner>
    {
        private readonly ISharedRule<MessageLearner, bool> _learnerDoBShouldNotbeNull;
        private readonly IFetchSpecificFundModelsLDsWithLearnStartDate _fetchSpecificFundModelsLDsWithLearnStartDate;
        private readonly IPickValidLdsWithAgeLimitFamTypeAndCode _pickValidLdsWithAgeLimitFamTypeAndCode;
        private readonly ILearnerDelFam66ExclusionRule _learnerDelFamExclusionRulesValidator;
        private readonly IValidationErrorHandler<MessageLearner> _validationErrorHandler;

        public LearnDelFAMType66Validator(
            [KeyFilter(RuleNameConstants.LearnerDoBShouldNotBeNull)] ISharedRule<MessageLearner, bool> learnerDoBShouldNotbeNull,
            IFetchSpecificFundModelsLDsWithLearnStartDate fetchSpecificFundModelsLDsWithLearnStartDate,
            IPickValidLdsWithAgeLimitFamTypeAndCode pickValidLdsWithAgeLimitFamTypeAndCode,
            ILearnerDelFam66ExclusionRule learnerDelFamExclusionRulesValidator,
            IValidationErrorHandler<MessageLearner> validationErrorHandler)
        {
            _learnerDoBShouldNotbeNull = learnerDoBShouldNotbeNull;
            _fetchSpecificFundModelsLDsWithLearnStartDate = fetchSpecificFundModelsLDsWithLearnStartDate;
            _pickValidLdsWithAgeLimitFamTypeAndCode = pickValidLdsWithAgeLimitFamTypeAndCode;
            _learnerDelFamExclusionRulesValidator = learnerDelFamExclusionRulesValidator;
            _validationErrorHandler = validationErrorHandler;
        }


        public void Validate(MessageLearner learner)
        {
            // check exclusion rules first, if any exclusion rule is returning true then skip this rule.
            if (_learnerDelFamExclusionRulesValidator.Evaluate(learner)) return;

            //check DoB rule, proceed only if the Dob is not null
            if (_learnerDoBShouldNotbeNull.Evaluate(learner)) return;

            //fetch fundmodel 35 LDs for this academic year .
            var eligibleLDs = _fetchSpecificFundModelsLDsWithLearnStartDate.Evaluate(learner);
            if (eligibleLDs.LearningDelivery.Count() == 0) return;

            //check the learner age is 24 or more rule & LFAMType FAMCODE
            var validLDsWithCorrectAgeandFAMtypesAndLars = _pickValidLdsWithAgeLimitFamTypeAndCode.Evaluate(eligibleLDs);
            if (validLDsWithCorrectAgeandFAMtypesAndLars == null ||
                validLDsWithCorrectAgeandFAMtypesAndLars.Count == 0) return;

            _validationErrorHandler.Handle(learner, RuleNameConstants.LearnDelFam66);
        }       
    }
}
