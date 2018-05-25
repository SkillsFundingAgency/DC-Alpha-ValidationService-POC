using BusinessRules.POC.ExternalData.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.POC.ExternalData.Interface
{
    public interface ILARSNotionalNVQLevelData : IExternalData<string, List<string>>
    {
    }
}
