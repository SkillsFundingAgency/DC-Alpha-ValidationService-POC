using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessRules.POC.Interfaces;
using BusinessRules.POC.Models;
using BusinessRules.POC.SharedRules;
using DCT.ILR.Model;

namespace BusinessRules.POC.RuleLearnDelFAMType66.ExclusionRules
{
    public class DD07IsYRuleDelFam66 : ILearnerDelFam66ExclusionRuleForLearner
    {
        private readonly IDD07Rule _dd07Rule;

        public DD07IsYRuleDelFam66(IDD07Rule dd07Rule)
        {
            _dd07Rule = dd07Rule;
        }
        public bool Evaluate(MessageLearner objectToValidate)
        {
            return objectToValidate.LearningDelivery.Any(ld => _dd07Rule.Evaluate(ld) == "Y");
        }
    }

    public interface ILearnerDelFam66ExclusionRuleForLearner : IShortRule<MessageLearner>
    {
    }
}
