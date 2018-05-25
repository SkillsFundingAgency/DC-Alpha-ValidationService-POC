using BusinessRules.POC.SharedRules.DD21;
using DCT.ILR.Model;

namespace BusinessRules.POC.RuleLearnDelFAMType66.ExclusionRules
{
    public class DD21IsYRule : ILearnerDelFam66ExclusionRuleForLearner
    {
        private readonly IDD21Rule _dd21Rule;
        
        public DD21IsYRule(IDD21Rule dd21Rule)
        {
            _dd21Rule = dd21Rule;
        }

        public bool Evaluate(MessageLearner learner)
        {            
            return  _dd21Rule.Evaluate(learner) == "Y";
        }
    }
}
