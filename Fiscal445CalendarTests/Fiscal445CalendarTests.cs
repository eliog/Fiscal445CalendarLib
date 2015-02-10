using System;
using Fiscal445CalendarLib;
using NUnit.Framework;

namespace Fiscal445CalendarTests
{
    [TestFixture]
    public class Fiscal445CalendarTests
    {
        private DateTime _D(string dt)
        {
            return DateTime.Parse(dt);
        }

        [Test]
        public void SetsCorrectReferenceYear()
        {
            var cal = new Fiscal445Calendar();
            cal.ReferenceYearStartDate = _D("1-1-2013");
            Assert.AreEqual(_D("1-1-2013"), cal.GetFiscalYearStartDate(2013));
            cal.ReferenceYearStartDate = _D("12-31-2012");
            Assert.AreEqual(_D("12-31-2012"), cal.GetFiscalYearStartDate(2013));
        }

        [Test]
        [TestCase("12/31/2012", "12/29/13",2013)]
        [TestCase("12/30/2013", "12/28/14", 2014)]
        [TestCase("12/29/2014", "12/27/15", 2015)]
        public void CalculatesCorrectFiscalYearStartAndEndDates(string startDate, string endDate, int year)
        {
            var cal = new Fiscal445Calendar();
            Assert.AreEqual(_D(startDate), cal.GetFiscalYearStartDate(year));
            Assert.AreEqual(_D(endDate), cal.GetFiscalYearEndDate(year));
        }

        [Test]
        [TestCase(2013,1,"12/31/12","1/06/13")]
        [TestCase(2013,31, "7/29/13", "8/4/13")]
        [TestCase(2014, 1, "12/30/13", "1/05/14")]
        [TestCase(2015, 1, "12/29/14", "1/04/15")]
        public void CalculatesCorrectFiscalWeekStartAndEndDates(int fiscalYear, int fiscalWeek, string expectedStartDate, string expectedEndDate)
        {
            var cal = new Fiscal445Calendar(); 
            Assert.AreEqual(_D(expectedStartDate), cal.GetFiscalWeekStartDate(fiscalYear,fiscalWeek));
            Assert.AreEqual(_D(expectedEndDate), cal.GetFiscalWeekEndDate(fiscalYear, fiscalWeek));
        }

        [Test]
        [TestCase(2013,1,"12/31/12","1/27/13")]
        [TestCase(2013,2, "1/28/13", "2/24/13")]
        [TestCase(2013,3, "2/25/13", "3/31/13")]
        [TestCase(2013,4, "4/1/13", "4/28/13")]

        public void CalculatesCorrectFiscalMonthStartAndEndDates(int fiscalYear, int fiscalMonth, string expectedStartDate, string expectedEndDate)
        {
            var cal = new Fiscal445Calendar();
            Assert.AreEqual(_D(expectedStartDate), cal.GetFiscalMonthStartDate(fiscalYear,fiscalMonth));
            Assert.AreEqual(_D(expectedEndDate), cal.GetFiscalMonthEndDate(fiscalYear,fiscalMonth));
        }
        
        [Test]
        [TestCase("12/31/2012",2013)]
        [TestCase("2/10/2013",2013)]
        [TestCase("12/30/2013", 2014)]
        [TestCase("2/10/2014", 2014)]
        [TestCase("2/10/2015", 2015)]
        public void PullsCorrectFiscalYear(string testDate, int expectedFiscalYear)
        {
            var cal = new Fiscal445Calendar();
            Assert.AreEqual(expectedFiscalYear, cal.GetFiscalYear(_D(testDate)));

        }
        [Test]
        [TestCase("4/30/13",5)]
        [TestCase("12/29/13",12)]
        [TestCase("12/30/13", 1)]
        public void PullsCorrectFiscalMonth(string testDate, int expectedMonth)
        {
            var cal = new Fiscal445Calendar();
            Assert.AreEqual(expectedMonth, cal.GetFiscalMonth(_D(testDate)));
        }

        [Test]
        [TestCase("4/30/13", 18)]
        [TestCase("12/29/13", 52)]
        [TestCase("12/30/13", 1)]
        public void PullsCorrectFiscalWeek(string testDate, int expectedMonth)
        {
            var cal = new Fiscal445Calendar();
            Assert.AreEqual(expectedMonth, cal.GetFiscalWeek(_D(testDate)));
        }
    } 
}
