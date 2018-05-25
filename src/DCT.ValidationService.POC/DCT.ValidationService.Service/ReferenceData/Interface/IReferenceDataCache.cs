using DCT.LARS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCT.ValidationService.Service.ReferenceData.Interface
{
    public interface IReferenceDataCache
    {
        HashSet<long> ULNs { get; }

        Dictionary<string, LearningDelivery> LearningDeliveries { get; }

        void Populate(IEnumerable<long> ulns, IEnumerable<string> learnAimRefs);
    }
}
