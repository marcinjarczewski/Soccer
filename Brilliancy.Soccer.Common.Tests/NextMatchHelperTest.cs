using Brilliancy.Soccer.Common.Helpers;
using NUnit.Framework;
using System;

namespace Brilliancy.Soccer.Common.Tests
{
    public class NextMatchHelperTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void NextMatchHelper_Simple()
        {
            Assert.AreEqual(new DateTime(2022,03,31,12,0,0),
                NextMatchHelper.GetMatchDate(new DateTime(2022, 03, 29), 4, new TimeSpan(12, 00, 00)));
        }

        [Test]
        public void NextMatchHelper_DefaultTimespan()
        {
            Assert.AreEqual(new DateTime(2022, 03, 31, 18, 0, 0),
                NextMatchHelper.GetMatchDate(new DateTime(2022, 03, 29), 4,default(TimeSpan?)));
        }

        [Test]
        public void NextMatchHelper_PrevoiusDay()
        {
            Assert.AreEqual(new DateTime(2022, 04, 04, 18, 0, 0),
                NextMatchHelper.GetMatchDate(new DateTime(2022, 03, 29), 1, default(TimeSpan?)));
        }

        [Test]
        public void NextMatchHelper_SameDayNextWeek()
        {
            Assert.AreEqual(new DateTime(2022, 04, 05, 18, 0, 0),
                NextMatchHelper.GetMatchDate(new DateTime(2022, 03, 29, 19, 0, 0), 2, new TimeSpan(18,0,0)));
        }

        [Test]
        public void NextMatchHelper_SameDay()
        {
            Assert.AreEqual(new DateTime(2022, 03, 29, 16, 0, 0),
                NextMatchHelper.GetMatchDate(new DateTime(2022, 03, 29, 13, 0, 0), 2, new TimeSpan(16, 0, 0)));
        }
    }
}