using Brilliancy.Soccer.Common.Enums;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Brilliancy.Soccer.Common.Helpers
{

    public static class NextMatchHelper
    {
        /// <summary>
        /// Get the next match date according to the given date
        /// </summary>
        /// <param name="fromDate">given date</param>
        /// <param name="day"> day of the week (1 - Monday, 7- Sunday)</param>
        /// <param name="ts">match time</param>
        /// <returns></returns>
        public static DateTime GetMatchDate(DateTime fromDate, int? day, TimeSpan? ts)
        {
            if(day.HasValue)
            {
                int targetDay = (day.Value % 7);
                int myDay = (int)fromDate.DayOfWeek;
                var newDate = fromDate;
                if (ts.HasValue)
                {
                    var time = fromDate.TimeOfDay;
                    if (time > ts)
                    {
                        newDate = newDate.AddDays(1);
                        myDay = (int)newDate.DayOfWeek;
                    }
                    newDate = new DateTime(newDate.Year, newDate.Month, newDate.Day, ts.Value.Hours, ts.Value.Minutes, 0);
                }
                else
                {
                    newDate = new DateTime(newDate.Year, newDate.Month, newDate.Day, 18, 0, 0);
                }
                int diff = targetDay - myDay;
                if(diff < 0)
                {
                    diff += 7;
                }
                newDate = newDate.AddDays(diff);
                return newDate;
            }
            else
            {
                var newDate = fromDate;
                if (ts.HasValue)
                {
                    var time = fromDate.TimeOfDay;
                    if (time < ts)
                    {
                        newDate = newDate.AddDays(1);
                    }
                    newDate = new DateTime(newDate.Year, newDate.Month, newDate.Day, ts.Value.Hours, ts.Value.Minutes, 0);
                    return newDate;
                }
                else
                {
                    newDate = newDate.AddDays(1);
                    newDate = new DateTime(newDate.Year, newDate.Month, newDate.Day, 18, 0, 0);
                    return newDate;
                }
            }
        }
    }
}
