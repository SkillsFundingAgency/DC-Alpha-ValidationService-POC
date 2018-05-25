using BusinessRules.POC.Interfaces;
using BusinessRules.POC.SimpleRuleValidators;
using DCT.ILR.Model;

namespace BusinessRules.POC.SimpleValidators
{
    public class SimpleLearnerRuleValidator 
    {
        private readonly IValidationErrorHandler<MessageLearner> _validationErrorHandler;
        private readonly ISimpleRuleLearningDeliveryValidator _simpleRuleLearningDeliveryValidator ;

        public SimpleLearnerRuleValidator(IValidationErrorHandler<MessageLearner> validationErrorHandler,
                                          ISimpleRuleLearningDeliveryValidator simpleRuleLearningDeliveryValidator)
        {
            _validationErrorHandler = validationErrorHandler;
            _simpleRuleLearningDeliveryValidator = simpleRuleLearningDeliveryValidator;
        }

        public bool Validate(MessageLearner learner)
        {
            
            if (learner.LearningDelivery == null)
            {
                return true;
            }

            foreach (var learningDelivery in learner.LearningDelivery)
            {
                if (!_simpleRuleLearningDeliveryValidator.Evaluate(learningDelivery)) continue;

                _validationErrorHandler.Handle(learner, RuleNameConstants.R105);
                return false;
            }

            return true;
        }
    }
    
    
}
