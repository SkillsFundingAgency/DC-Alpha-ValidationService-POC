using BusinessRules.POC.ExternalData.Interface;
using DCT.LARS.Model;
using DCT.ReferenceData.Model;
using DCT.ValidationService.Service.ReferenceData.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.POC.ExternalData
{
    public class LARSNotionalNVQLevelData : ILARSNotionalNVQLevelData, IExternalData<string, List<string>>
    {
        private readonly IReferenceDataCache _referenceDataCache;

        public LARSNotionalNVQLevelData(IReferenceDataCache referenceDataCache)
        {
            _referenceDataCache = referenceDataCache;
        }

        public List<string> Get(string input)
        {
            if (string.IsNullOrEmpty(input) || _referenceDataCache.LearningDeliveries == null) return null;

            _referenceDataCache.LearningDeliveries.TryGetValue(input, out var learningDelivery);

            return learningDelivery == null ? null : new List<string>() { learningDelivery.NotionalNVQLevelv2 };
        }
    }
}
