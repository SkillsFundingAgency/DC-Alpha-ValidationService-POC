using BusinessRules.POC.Interfaces;
using BusinessRules.POC.ValidationData.Interface;
using DCT.ILR.Model;
using System;
using System.Linq;

namespace BusinessRules.POC.SharedRules
{
    public interface IDD07Rule : IShortRule<MessageLearnerLearningDelivery, string>
    {
    }

    public class DD07Rule : IDD07Rule
    {
        private readonly IValidationData _validationData;

        public DD07Rule(IValidationData validationData)
        {
            _validationData = validationData;
        }

        public string Evaluate(MessageLearnerLearningDelivery objectToValidate)
        {
            return _validationData.ApprenticeProgTypes.Contains(objectToValidate.ProgType) ? "Y" : "N";
        }
    }
}
