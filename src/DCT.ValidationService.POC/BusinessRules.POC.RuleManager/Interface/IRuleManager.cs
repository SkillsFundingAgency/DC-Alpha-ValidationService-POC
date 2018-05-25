using BusinessRules.POC.Interfaces;
using BusinessRules.POC.Models;
using DCT.ILR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.POC.RuleManager.Interface
{
    public interface IRuleManager
    {
        IValidationErrorHandler<MessageLearner> ExecuteRules(IEnumerable<MessageLearner> learners);
    }
}
