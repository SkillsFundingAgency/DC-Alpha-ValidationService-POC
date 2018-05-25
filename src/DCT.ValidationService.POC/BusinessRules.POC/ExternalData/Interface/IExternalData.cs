using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.POC.ExternalData.Interface
{
    public interface IExternalData<T, TResult>
    {
        TResult Get(T input);
    }
}
