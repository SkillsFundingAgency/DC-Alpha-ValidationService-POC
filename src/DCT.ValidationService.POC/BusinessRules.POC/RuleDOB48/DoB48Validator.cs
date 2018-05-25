using BusinessRules.POC.Helpers;
using BusinessRules.POC.Helpers.Interface;
using BusinessRules.POC.Interfaces;
using DCT.ILR.Model;
using System;
using System.Linq;

namespace BusinessRules.POC.RuleDOB48
{
    public class DoB48Validator //: IRule<MessageLearner>
    {
        private IDD07IsYRule _dd07IsYRule;
        private ISharedRule<MessageLearner, bool> _learnerDoBNotNullRule;
        private IShortRule<MessageLearner> _dd04IsInRangeRule;
        private IShortRule<MessageLearner> _IsLearnerBelowSchoolAge;
        private readonly IValidationErrorHandler<MessageLearner> _validationErrorHandler;
        
        public DoB48Validator(IDD07IsYRule dd07IsYRule, 
            ISharedRule<MessageLearner, bool> learnerDoBNotNullRule,
            IShortRule<MessageLearner> dd04IsInRangeRule,
            IShortRule<MessageLearner> isLearnerBelowSchoolAge,
            IValidationErrorHandler<MessageLearner> validationErrorHandler)
        {
            _dd07IsYRule = dd07IsYRule;
            _learnerDoBNotNullRule = learnerDoBNotNullRule;
            _dd04IsInRangeRule = dd04IsInRangeRule;
            _IsLearnerBelowSchoolAge = isLearnerBelowSchoolAge;
            _validationErrorHandler = validationErrorHandler;
        }

        public bool Validate(MessageLearner ObjectToValidate)
        {
            //check DoB rule, proceed only if the Dob is not null
            if (_learnerDoBNotNullRule.Evaluate(ObjectToValidate)) return true;

            //check agerule, if learner is above 16 then skip this rule
            if (_IsLearnerBelowSchoolAge.Evaluate(ObjectToValidate)) return true;

            //check the DD07 rule and get the valid prog type LDs
            ObjectToValidate.LearningDelivery = ObjectToValidate.LearningDelivery.Where(ld => !_dd07IsYRule.Evaluate(ld)).ToArray();
            
            //execute DD04 and fetch programme start date.
            if (_dd04IsInRangeRule.Evaluate(ObjectToValidate))
            {
                _validationErrorHandler.Handle(ObjectToValidate, RuleNameConstants.DateOfBirth_48);
                return false;
            }

            return true;
        }

        
    }

    public class IsLearnerBelowSchoolAge : IShortRule<MessageLearner>
    {
        private IDateHelper _dateHelper;

        public IsLearnerBelowSchoolAge(IDateHelper dateHelper)
        {
            _dateHelper = dateHelper;
        }

        public bool Evaluate(MessageLearner ObjectToValidate)
        {
            return _dateHelper.GetAge(DateTime.Now, ObjectToValidate.DateOfBirth) > 16;
        }
    }
}
