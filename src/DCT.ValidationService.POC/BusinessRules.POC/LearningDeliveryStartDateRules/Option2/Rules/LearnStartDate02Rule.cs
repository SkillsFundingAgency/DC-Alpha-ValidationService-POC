using BusinessRules.POC.Interfaces;
using BusinessRules.POC.ReferenceData;
using BusinessRules.POC.SharedRules;
using DCT.ILR.Model;
using System;

namespace BusinessRules.POC.LearningDeliveryStartDateRules.Option2
{
    public interface ILearnStartDate02Rule : ILearningDeliveryStartDateRule<MessageLearnerLearningDelivery>
    {

    }
    public class LearnStartDate02Rule : BaseLearningDeliveryStartDateRule, ILearnStartDate02Rule
    {
        public LearnStartDate02Rule(IValidationErrorHandler<MessageLearner> validationErrorHandler,
                                              IDD07Rule dd07IsYRule,
                                              IReferenceData<string, string> referenceData):base(validationErrorHandler,dd07IsYRule,referenceData)
        {

        }

        public override LearningDeliveryStartDateRuleResult Evaluate(MessageLearner learner, MessageLearnerLearningDelivery learningDelivery)
        {
            var academicStart = DateTime.Parse(ReferenceData.Get("AcademicYearStart"));
            var result = (learningDelivery.LearnStartDate.AddYears(10) - academicStart).TotalDays > 0;
            return CreateResult(result, "Error - ValidateLearnStartDate02" );                
        }
    }
}
