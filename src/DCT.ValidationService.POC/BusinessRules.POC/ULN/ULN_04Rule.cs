using BusinessRules.POC.DerivedData.Interface;
using BusinessRules.POC.Extensions;
using BusinessRules.POC.Interfaces;
using DCT.ILR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.POC.ULN
{
    public class ULN_04Rule : IRule<MessageLearner>
    {
        private readonly IDD01 _dd01;
        private readonly IValidationErrorHandler<MessageLearner> _validationErrorHandler;
        
        public ULN_04Rule(IDD01 dd01, IValidationErrorHandler<MessageLearner> validationErrorHandler)
        {
            _dd01 = dd01;
            _validationErrorHandler = validationErrorHandler;
        }

        public void Validate(MessageLearner objectToValidate)
        {
            if (ConditionMet(objectToValidate.ULN))
            {
                _validationErrorHandler.Handle(objectToValidate, "ULN_04");
            }            
        }

        public bool ConditionMet(long uln)
        {
            var dd_01 = _dd01.Derive(uln);

            var ulnString = uln.ToString();

            if (dd_01 == ValidationConstants.N || (dd_01 != ValidationConstants.Y && ulnString.Length >= 10 && dd_01 != ulnString.ElementAt(9).ToString()))
            {
                return true;
            }

            return false;
        }        
    }
}
