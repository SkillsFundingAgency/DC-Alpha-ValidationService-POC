using BusinessRules.POC.ExternalData.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.POC.ExternalData.Interface
{
    public interface ILARSCategoryRefData : IExternalData<string, List<int>>
    {
    }
}
