using BusinessRules.POC.Interfaces;
using DCT.ILR.Model;
using System.Collections.Generic;
using System.Linq;

namespace BusinessRules.POC.LearningDeliveryStartDateRules.Option2
{
    public sealed class LearnerStartDateRuleValidator // : IRule<Learner>
    {
        private readonly List<ILearningDeliveryStartDateRule<MessageLearnerLearningDelivery>> _rulesList = null;
        private readonly IValidationErrorHandler<MessageLearner> _validationErrorHandler;


        //TODO: check if in autofac there is a way to inject a list of all instances implemnting an interface
        // that way we can get all the same type rules injected in as list and then can loop through
        public LearnerStartDateRuleValidator(IValidationErrorHandler<MessageLearner> validationErrorHandler, 
                                            ILearnStartDate02Rule learnStartDate02Rule,
                                            ILearnStartDate03Rule learnStartDate03Rule,
                                            ILearnStartDate05Rule learnStartDate05Rule,
                                            ILearnStartDate12Rule learnStartDate12Rule)
        {
            _validationErrorHandler = validationErrorHandler;
            _rulesList = new List<ILearningDeliveryStartDateRule<MessageLearnerLearningDelivery>>();
            _rulesList.Add(learnStartDate02Rule);
            _rulesList.Add(learnStartDate03Rule);
            _rulesList.Add(learnStartDate05Rule);
            _rulesList.Add(learnStartDate12Rule);
            
        }


        public bool Validate(MessageLearner learner)
        {

            if (learner.LearningDelivery == null)
            {
                return true;
            }
       
            foreach (var learningDelivery in learner.LearningDelivery)
            {
                _rulesList.AsParallel().ForAll(x => {
                    var result  = x.Evaluate(learner, learningDelivery);
                    if (!result.Result)
                        _validationErrorHandler.Handle(learner, result.Message);


                });
            }

            return true;
        }

        
    }
}
