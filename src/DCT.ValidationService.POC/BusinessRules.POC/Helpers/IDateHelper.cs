using BusinessRules.POC.Helpers.Interface;
using System;

namespace BusinessRules.POC.Helpers
{
    public class DateHelper : IDateHelper
    {
        public int GetAge(DateTime current, DateTime? doB)
        {
            if (doB == null)
            {
                return 0;
            }

            int age = current.Year - doB.Value.Year;

            return current < doB.Value.AddYears(age) ? age - 1 : age; 
        }

        public DateTime GetLastFridayInMonth(DateTime date)
        {
            var firstDayOfNextMonth = new DateTime(date.Year, date.Month, 1).AddMonths(1);
            int vector = (((int)firstDayOfNextMonth.DayOfWeek + 1) % 7) + 1;
            return firstDayOfNextMonth.AddDays(-vector);
        }

        public int GetYearInWhichPersonTurnsTo(int ageTurningTo, DateTime? doB)
        {
            if (doB == null) return 0;
            return doB.Value.Year + ageTurningTo;
        }

        public DateTime GetLastFridayInJuneOfAcademicYear(DateTime referenceDate)
        {
            //before 31/aug return current year last friday date
            if(referenceDate.Month <= 8 && referenceDate.Day <= 31)
            {
                return GetLastFridayInMonth(new DateTime(referenceDate.Year, 6, 1));
            }

            return GetLastFridayInMonth(new DateTime(referenceDate.Year + 1, 6, 1));
        }
    }
}
