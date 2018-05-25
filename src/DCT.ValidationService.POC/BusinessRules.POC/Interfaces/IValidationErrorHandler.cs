using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.POC.Interfaces
{
    public interface IValidationErrorHandler<T>
    {
        void Handle(T identifier, string errorName);
    }
}
