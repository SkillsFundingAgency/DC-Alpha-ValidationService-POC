using System.Collections.Generic;
using System.Linq;
using DCT.ILR.Model;
using BusinessRules.POC.RuleManager.Interface;
using DCT.ValidationService.Service.Interface;
using BusinessRules.POC.FileData.Interface;
using BusinessRules.POC.Interfaces;

namespace DCT.ValidationService.Service.Implementation
{
    public class RuleManagerValidationService : IValidationService
    {
        private readonly IRuleManager _ruleManager;
        private readonly IFileData _fileData;

        public RuleManagerValidationService(IRuleManager ruleManager, IFileData fileData)
        {
            _ruleManager = ruleManager;
            _fileData = fileData;
        }

        public IEnumerable<LearnerValidationError> Validate(Message message)
        {
            _fileData.Populate(message);

            var validationErrorHandler = _ruleManager.ExecuteRules(message.Learner);

            return (validationErrorHandler as LearnerValidationErrorHandler).ErrorBag;
        }
    }
}
