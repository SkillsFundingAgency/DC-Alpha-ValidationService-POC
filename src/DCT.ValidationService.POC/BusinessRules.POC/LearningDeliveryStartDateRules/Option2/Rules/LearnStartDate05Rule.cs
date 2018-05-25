using BusinessRules.POC.Interfaces;
using BusinessRules.POC.ReferenceData;
using BusinessRules.POC.SharedRules;
using DCT.ILR.Model;

namespace BusinessRules.POC.LearningDeliveryStartDateRules.Option2
{

    public interface ILearnStartDate05Rule : ILearningDeliveryStartDateRule<MessageLearnerLearningDelivery>
    {

    }
    public class LearnStartDate05Rule : BaseLearningDeliveryStartDateRule, ILearnStartDate05Rule
    {
        public LearnStartDate05Rule(IValidationErrorHandler<MessageLearner> validationErrorHandler,
                                           IDD07Rule dd07IsYRule,
                                           IReferenceData<string, string> referenceData) : base(validationErrorHandler, dd07IsYRule, referenceData)
        {

        }

        public override LearningDeliveryStartDateRuleResult Evaluate(MessageLearner learner, MessageLearnerLearningDelivery learningDelivery)
        {

            var result = learner.DateOfBirth >= learningDelivery.LearnStartDate;
            return CreateResult(result, "Error - ValidateLearnStartDate05");


        }
    }
}
