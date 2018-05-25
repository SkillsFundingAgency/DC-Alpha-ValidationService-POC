using BusinessRules.POC.ExternalData;
using BusinessRules.POC.ExternalData.Interface;
using BusinessRules.POC.Interfaces;
using BusinessRules.POC.Models;
using BusinessRules.POC.ReferenceData;
using DCT.ILR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.POC.RuleLearnDelFAMType66
{
    public interface IValidateLARNotionalNVQLevelRule
    {
        bool Evaluate(MessageLearnerLearningDelivery objectToValidate);
    }

    public class ValidateLARNotionalNVQLevelRule : IValidateLARNotionalNVQLevelRule
    {
        private ILARSNotionalNVQLevelData _LARSNotionalNVQLevelData;
        private IReferenceData<string, string> _referenceData;
        private readonly List<string> _AllowedLARSNotionalNVQLevelv2;

        public ValidateLARNotionalNVQLevelRule(ILARSNotionalNVQLevelData lARSNotionalNVQLevelData, IReferenceData<string, string> referenceDataFromSettings )
        {
            _LARSNotionalNVQLevelData = lARSNotionalNVQLevelData;
            _referenceData = referenceDataFromSettings;
            _AllowedLARSNotionalNVQLevelv2 = _referenceData.Get("AllowedLARSNotionalNVQLevelv2").Split(',').ToList();

        }
        public bool Evaluate(MessageLearnerLearningDelivery objectToValidate)
        {
            var larsResult = _LARSNotionalNVQLevelData.Get(objectToValidate.LearnAimRef);

            return larsResult != null && larsResult.Intersect(_AllowedLARSNotionalNVQLevelv2).Any();
        }
    }
}
