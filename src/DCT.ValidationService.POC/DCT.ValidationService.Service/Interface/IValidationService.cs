using BusinessRules.POC.Interfaces;
using DCT.ILR.Model;
using System.Collections.Generic;

namespace DCT.ValidationService.Service.Interface
{
    public interface IValidationService
    {
        IEnumerable<LearnerValidationError> Validate(Message message);
    }
}
