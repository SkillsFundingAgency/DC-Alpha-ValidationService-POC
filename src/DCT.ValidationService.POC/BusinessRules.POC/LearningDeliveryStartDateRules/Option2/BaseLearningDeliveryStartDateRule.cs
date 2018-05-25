using BusinessRules.POC.Interfaces;
using BusinessRules.POC.ReferenceData;
using BusinessRules.POC.SharedRules;
using DCT.ILR.Model;

namespace BusinessRules.POC.LearningDeliveryStartDateRules.Option2
{
    public abstract class BaseLearningDeliveryStartDateRule : ILearningDeliveryStartDateRule<MessageLearnerLearningDelivery>
    {
        protected readonly IValidationErrorHandler<MessageLearner> ValidationErrorHandler = null;
        protected readonly IReferenceData<string, string> ReferenceData = null;
        protected readonly IDD07Rule Dd07IsYRule = null;
        protected const string DDO7_RULE_NO = "N";
        protected const string DDO7_RULE_YES = "Y";

        #region Constructors

        //public BaseLearningDeliveryStartDateRule()
        //{

        //}
        public BaseLearningDeliveryStartDateRule(IValidationErrorHandler<MessageLearner> validationErrorHandler,
                                              IDD07Rule dd07IsYRule,
                                              IReferenceData<string, string> referenceData)
        {
            ValidationErrorHandler = validationErrorHandler;
            ReferenceData = referenceData;
            Dd07IsYRule = dd07IsYRule;
        }

        //public BaseLearningDeliveryStartDateRule(IValidationErrorHandler<Learner> validationErrorHandler,IReferenceData<string, string> referenceData)
        //{
        //    ValidationErrorHandler = validationErrorHandler;
        //    ReferenceData = referenceData;
            
        //}

        //public BaseLearningDeliveryStartDateRule(IValidationErrorHandler<Learner> validationErrorHandler)
        //{
        //    ValidationErrorHandler = validationErrorHandler;

        //}

        #endregion
        public abstract LearningDeliveryStartDateRuleResult Evaluate(MessageLearner learner, MessageLearnerLearningDelivery learningDelivery);

        protected LearningDeliveryStartDateRuleResult CreateResult(bool result,string message)
        {
            return new LearningDeliveryStartDateRuleResult() { Result = result, Message = message };
        }


    }
}
