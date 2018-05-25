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
using DCT.ILR.Model;

namespace BusinessRules.POC.ExternalData
{
    public class LARSFrameworkAimComponentTypeRefData : ILARSFrameworkAimComponentTypeRefData, IExternalData<MessageLearnerLearningDelivery, int?>
    {
        private readonly IReferenceDataCache _referenceDataCache;

        public LARSFrameworkAimComponentTypeRefData(IReferenceDataCache referenceDataCache)
        {
            _referenceDataCache = referenceDataCache;
        }
        
        public int? Get(MessageLearnerLearningDelivery input)
        {
            if (input == null)
            {
                return null;
            }                    

            _referenceDataCache.LearningDeliveries.TryGetValue(input.LearnAimRef, out var learningDelivery);

            return learningDelivery?.FrameworkAims
                .Where(
                fa =>
                fa.LearnAimRef == input.LearnAimRef &&
                fa.FworkCode == input.FworkCode &&
                fa.ProgType == input.ProgType &&
                fa.PwayCode == input.PwayCode)
                .Select(fa => fa.FrameworkComponentType)
                .FirstOrDefault();
        }
    }
}
