using DCT.ILR.Model;

namespace BusinessRules.POC.LearningDeliveryStartDateRules.Option2
{
    public interface ILearningDeliveryStartDateRule<T> where T : MessageLearnerLearningDelivery
    {
        LearningDeliveryStartDateRuleResult Evaluate(MessageLearner learner, T learningDelivery);
    }
}
