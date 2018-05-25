using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessRules.POC.ReferenceData;
using BusinessRules.POC.ExternalData.Interface;
using DCT.ValidationService.Service.ReferenceData.Interface;
using DCT.LARS.Model;
using DCT.ReferenceData.Model;

namespace BusinessRules.POC.ExternalData
{
    public class LARSCategoryRefData : ILARSCategoryRefData, IExternalData<string, List<int>>
    {
        private readonly IReferenceDataCache _referenceDataCache;

        public LARSCategoryRefData(IReferenceDataCache referenceDataCache)
        {
            _referenceDataCache = referenceDataCache;
        }
        public List<int> Get(string learnAimRef)
        {
            if (string.IsNullOrEmpty(learnAimRef) || _referenceDataCache.LearningDeliveries == null) return null;

            _referenceDataCache.LearningDeliveries.TryGetValue(learnAimRef, out var learningDelivery);
                
            return learningDelivery?.LearningDeliveryCategories.Select(ldc => ldc.CategoryRef).ToList();
        }
    }
}
