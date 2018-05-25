using BusinessRules.POC.Interfaces;
using DCT.ILR.Model;
using System.Collections.Concurrent;

namespace DCT.ValidationService.Service.Implementation
{
    public class LearnerValidationErrorHandler : IValidationErrorHandler<MessageLearner>
    {
        private ConcurrentBag<LearnerValidationError> _errorbag = new ConcurrentBag<LearnerValidationError>();
        
        public void Handle(MessageLearner identifier, string errorName)
        {
            _errorbag.Add(new LearnerValidationError(identifier.LearnRefNumber, errorName));
        }

        public ConcurrentBag<LearnerValidationError> ErrorBag
        {
            get
            {
                return _errorbag;
            }
        }

    }
}
