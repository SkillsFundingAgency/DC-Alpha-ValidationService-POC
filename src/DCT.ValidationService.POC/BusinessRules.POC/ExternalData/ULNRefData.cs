using BusinessRules.POC.ExternalData.Interface;
using DCT.ValidationService.Service.ReferenceData.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.POC.ExternalData
{
    public class ULNRefData : IULNRefData, IExternalData<long, long?>
    {
        private readonly IReferenceDataCache _referenceDataCache;
        public ULNRefData(IReferenceDataCache referenceDataCache)
        {
            _referenceDataCache = referenceDataCache;
        }

        public long? Get(long input)
        {
            if (_referenceDataCache.ULNs.Contains(input))
            {
                return input;
            }

            return null;
        }
    }
}
