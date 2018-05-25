using BusinessRules.POC.SharedRules.DD29;
using DCT.ILR.Model;

namespace BusinessRules.POC.RuleLearnDelFAMType66.ExclusionRules
{
    class DD29IsYRule :  ILearnerDelFam66ExclusionRuleForLearner
    {
        private readonly IDD29Rule _dd29Rule;

        public DD29IsYRule(IDD29Rule dd29Rule)
        {
            _dd29Rule = dd29Rule;
        }
        public bool Evaluate(MessageLearner learner)
        {
            return _dd29Rule.Evaluate(learner) == "Y";
        }
    }
}
