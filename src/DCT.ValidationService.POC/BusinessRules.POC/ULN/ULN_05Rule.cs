using BusinessRules.POC.ExternalData.Interface;
using BusinessRules.POC.Interfaces;
using DCT.ILR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.POC.ULN
{
    public class ULN_05Rule : IRule<MessageLearner>
    {
        private readonly IULNRefData _ulnRefData;
        private readonly IValidationErrorHandler<MessageLearner> _validationErrorHandler;

        private const long _uln = 9999999999;

        public ULN_05Rule(IULNRefData ulnRefData, IValidationErrorHandler<MessageLearner> validationErrorHandler)
        {
            _ulnRefData = ulnRefData;
            _validationErrorHandler = validationErrorHandler;
        }

        public void Validate(MessageLearner objectToValidate)
        {
            var uln = objectToValidate.ULN;

            if (ConditionMet(uln, _ulnRefData.Get(uln) != null))
            {
                _validationErrorHandler.Handle(objectToValidate, "ULN_05");
            }            
        }

        public bool ConditionMet(long uln, bool ulnInReferenceData)
        {
            return uln != _uln && ulnInReferenceData;
        }        
    }
}
