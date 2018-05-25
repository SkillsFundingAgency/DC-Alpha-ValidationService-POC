using BusinessRules.POC.ValidationData.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.POC.ValidationData
{
    public class ValidationData : IValidationData
    {
        private readonly DateTime _academicYearEnd = new DateTime(2018, 7, 31);
        private readonly DateTime _academicYearStart = new DateTime(2017, 8, 1);
        private readonly IEnumerable<long> _apprenticeshipProgTypes = new HashSet<long>() { 2, 3, 20, 21, 2, 23, 25 };
        private readonly DateTime _apprenticeshipProgAllowedStartDate = new DateTime(2016, 08, 01);
        private readonly DateTime _validationStartDateTime = DateTime.UtcNow;

        public DateTime AcademicYearEnd {  get { return _academicYearEnd; } }

     //   public DateTime AcademicYearLastFridayJune { get return _ }

        public DateTime AcademicYearStart { get { return _academicYearStart; } }

        public IEnumerable<long> ApprenticeProgTypes { get { return _apprenticeshipProgTypes; } }

        public DateTime ApprencticeProgAllowedStartDate { get { return _apprenticeshipProgAllowedStartDate; } }

        public DateTime ValidationStartDateTime { get { return _validationStartDateTime; } }
    }
}
