using System.Collections.Generic;
using System.Linq;
using BusinessRules.POC.Interfaces;
using DCT.ILR.Model;

namespace BusinessRules.POC.RuleLearnDelFAMType66.ExclusionRules
{
    /// <summary>
    /// This class calls multiple subrules for checking the valid types in del fams
    /// </summary>
    public class LearnerDelFamExclusionRulesValidator :  ILearnerDelFam66ExclusionRule
    {
        private readonly IEnumerable<ILearnerDelFam66ExclusionRuleForFam> _delFamExclusionRules;
        private readonly IEnumerable<ILearnerDelFam66ExclusionRuleForLearner> _learnerExclusionRules;

        public LearnerDelFamExclusionRulesValidator(IEnumerable<ILearnerDelFam66ExclusionRuleForFam> delFamExclusionRules,
            IEnumerable<ILearnerDelFam66ExclusionRuleForLearner> learnerExclusionRules)
        {
            _delFamExclusionRules = delFamExclusionRules;
            _learnerExclusionRules = learnerExclusionRules;
        }

        public bool Evaluate(MessageLearner learner)
        {
            if (learner?.LearningDelivery == null) return false;

            //check learner exclusion rules if any rule is true return true
            if (_learnerExclusionRules.Any(subRule => subRule.Evaluate(learner))) return true;

            //check learnerdelfam exclusion rules
            foreach (var learningDelivery in learner.LearningDelivery)
            {
                if (learningDelivery.LearningDeliveryFAM == null) continue;

                foreach (var ldFam in learningDelivery.LearningDeliveryFAM)
                {
                    if (_delFamExclusionRules.Any( subrule=> subrule.Evaluate(ldFam)))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

    public interface ILearnerDelFam66ExclusionRule : IShortRule<MessageLearner>
    {
    }
}
