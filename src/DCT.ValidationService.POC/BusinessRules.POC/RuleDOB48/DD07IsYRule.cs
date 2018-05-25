using BusinessRules.POC.Interfaces;
using BusinessRules.POC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessRules.POC.SharedRules;
using DCT.ILR.Model;

namespace BusinessRules.POC.RuleDOB48
{
    public interface IDD07IsYRule : IShortRule<MessageLearnerLearningDelivery>
    {
    }

    public class DD07IsYRule : IDD07IsYRule
    {
        private IDD07Rule _dd07Rule;

        public DD07IsYRule(IDD07Rule dd07Rule)
        {
            _dd07Rule = dd07Rule;
        }

        public bool Evaluate(MessageLearnerLearningDelivery ObjectToValidate)
        {
            return _dd07Rule.Evaluate(ObjectToValidate) != "Y";
        }
    }
}
