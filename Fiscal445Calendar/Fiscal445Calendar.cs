using System;

namespace Fiscal445CalendarLib
{
    public class Fiscal445Calendar
    {
        
        public DateTime ReferenceYearStartDate { get; set; }

        public Fiscal445Calendar()
        {
            ReferenceYearStartDate = new DateTime(2012,12,31);
        }

        private int ReferenceYear()
        {
            if ((ReferenceYearStartDate.Month == 12) && (ReferenceYearStartDate.Day > 20))
            {
                return ReferenceYearStartDate.Year + 1;
            }
            return ReferenceYearStartDate.Year;
        }

        #region Calendar Info Methods
        public DateTime GetFiscalYearStartDate(int fiscalYear)
        {
            var offset =fiscalYear - ReferenceYear();
            return (ReferenceYearStartDate.AddDays(364*offset));
        }

        public DateTime GetFiscalYearEndDate(int fiscalYear)
        {
            var offset = fiscalYear - ReferenceYear();
            return (ReferenceYearStartDate.AddDays(363).AddDays(364*offset));
        }

        public DateTime GetFiscalMonthStartDate(int fiscalYear, int fiscalMonth)
        {
            var extraWeeks = (int) Math.Floor((double)(fiscalMonth-1)/3);
            return GetFiscalYearStartDate(fiscalYear).AddDays(28*(fiscalMonth - 1)).AddDays(7*extraWeeks);
        }

        public DateTime GetFiscalMonthEndDate(int fiscalYear, int fiscalMonth)
        {
            var days = 27 + (fiscalMonth%3 == 0 ? 7 : 0);
            return GetFiscalMonthStartDate(fiscalYear, fiscalMonth).AddDays(days);
        }

        public DateTime GetFiscalWeekStartDate(int fiscalYear, int fiscalWeek)
        {
            return GetFiscalYearStartDate(fiscalYear).AddDays(7*(fiscalWeek - 1));
        }

        public DateTime GetFiscalWeekEndDate(int fiscalYear, int fiscalWeek)
        {
            return GetFiscalWeekStartDate(fiscalYear, fiscalWeek).AddDays(6);
        }


        #endregion

#region "Date Info"


        public int GetFiscalYear(DateTime date)
        {
            var days = date.Subtract(ReferenceYearStartDate).Days;
            return (int) (Math.Floor((double) days/364)) + ReferenceYear();
        }

        // I'm sure there's a better way to do this without looping, but I can't math right now.
        public int GetFiscalMonth(DateTime date)
        {
            var year = GetFiscalYear(date);
            var month = 1;
            while (date < GetFiscalMonthStartDate(year,month) || (date>GetFiscalMonthEndDate(year,month))) month++;
            return month;
        }

        // Same as above, and yes I used math as a verb. 
        public int GetFiscalWeek(DateTime date)
        {
            var year = GetFiscalYear(date);
            var week = 1;
            while (date < GetFiscalWeekStartDate(year, week) || (date > GetFiscalWeekEndDate(year, week))) week++;
            return week;
        }
#endregion
       

    }
}
