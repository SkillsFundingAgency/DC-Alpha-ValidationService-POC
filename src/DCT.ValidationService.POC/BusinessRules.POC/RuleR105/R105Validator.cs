using BusinessRules.POC.Interfaces;
using DCT.ILR.Model;

namespace BusinessRules.POC.RuleR105
{
    public class R105Validator //: IRule<MessageLearner>
    {
        private readonly IR105PickLdFamACTTypes _r105PickLdFamACTTypes;
        private readonly ILearningDeliveryNoOverlappingDatesRule _learningDeliveryNoOverlappingDatesRule;
        private readonly IValidationErrorHandler<MessageLearner> _validationErrorHandler;
        private const string ErrorMessage = "The learner must not have different Apprenticeship contract types recorded at the same time";

      
        public R105Validator(IR105PickLdFamACTTypes r105PickLdFamACTTypes,
            ILearningDeliveryNoOverlappingDatesRule learningDeliveryNoOverlappingDatesRule,
            IValidationErrorHandler<MessageLearner> validationErrorHandler)
        {
            _r105PickLdFamACTTypes = r105PickLdFamACTTypes;
            _learningDeliveryNoOverlappingDatesRule = learningDeliveryNoOverlappingDatesRule;
            _validationErrorHandler = validationErrorHandler;
        }

        public void Validate(MessageLearner learner)
        {
            var validLearningDeliveriesFAMs = _r105PickLdFamACTTypes.Evaluate(learner);

            if (validLearningDeliveriesFAMs == null)
            {
                return;
            }

           
            if (_learningDeliveryNoOverlappingDatesRule.Evaluate(validLearningDeliveriesFAMs))
            {
                //if found a record that has overlapping dates then return false
                _validationErrorHandler.Handle(learner, RuleNameConstants.R105);
                return;
            }            
        }
    }
}

