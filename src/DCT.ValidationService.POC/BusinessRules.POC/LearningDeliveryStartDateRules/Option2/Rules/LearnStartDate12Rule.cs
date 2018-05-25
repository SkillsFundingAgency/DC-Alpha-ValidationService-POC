using BusinessRules.POC.Interfaces;
using BusinessRules.POC.ReferenceData;
using BusinessRules.POC.SharedRules;
using DCT.ILR.Model;
using System;

namespace BusinessRules.POC.LearningDeliveryStartDateRules.Option2
{
    public interface ILearnStartDate12Rule : ILearningDeliveryStartDateRule<MessageLearnerLearningDelivery>
    {

    }
    public class LearnStartDate12Rule : BaseLearningDeliveryStartDateRule, ILearnStartDate12Rule
    {
        public LearnStartDate12Rule(IValidationErrorHandler<MessageLearner> validationErrorHandler,
                                           IDD07Rule dd07IsYRule,
                                           IReferenceData<string, string> referenceData) : base(validationErrorHandler, dd07IsYRule, referenceData)
        {

        }
        public override LearningDeliveryStartDateRuleResult Evaluate(MessageLearner learner, MessageLearnerLearningDelivery learningDelivery)
        {
            var dd07Result = Dd07IsYRule.Evaluate(learningDelivery);
            var academicStart = DateTime.Parse(ReferenceData.Get("AcademicYearStart"));

            var result = dd07Result == DDO7_RULE_YES &&
                            learningDelivery.ProgType != 24 &&
                            (learningDelivery.LearnStartDate - academicStart.AddDays(364)).TotalDays > 0;

            return CreateResult(result, "Error - ValidateLearnStartDate12");


        }
    }
}
