using DCT.ReferenceData.Model;
using System.Collections.Generic;

namespace DCT.ValidationService.Service.ReferenceData.Interface
{
    public interface IReferenceDataCache
    {
        IEnumerable<long> ULNs { get; }

        IDictionary<string, LearningDelivery> LearningDeliveries { get; }

        void Populate(IEnumerable<long> ulns, IEnumerable<string> learnAimRefs);
    }
}
