using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.POC.ValidationData.Interface
{
    public interface IValidationData
    {
        DateTime AcademicYearEnd { get; }
        DateTime AcademicYearStart { get; }
        IEnumerable<long> ApprenticeProgTypes { get; }
        DateTime ApprencticeProgAllowedStartDate { get; }
        DateTime ValidationStartDateTime { get; }
    }
}
