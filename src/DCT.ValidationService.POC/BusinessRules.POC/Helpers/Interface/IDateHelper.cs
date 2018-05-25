using System;

namespace BusinessRules.POC.Helpers.Interface
{
    public interface IDateHelper
    {
        DateTime GetLastFridayInMonth(DateTime date);

        int GetAge(DateTime reference, DateTime? doB);

        int GetYearInWhichPersonTurnsTo(int ageTurningTo, DateTime? doB);

        DateTime GetLastFridayInJuneOfAcademicYear(DateTime referenceDate);        
    }
}
