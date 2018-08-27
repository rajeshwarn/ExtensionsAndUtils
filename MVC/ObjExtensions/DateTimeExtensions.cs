using System;

namespace Shared.Extensions
{
    public static class DateTimeExtensions
    {
        public static string Timestamp(this DateTime source)
        {
            return source.ToString("yyyyMMddHHmmssffff");
        }

        public static int DaysToDate(this DateTime birthDate)
        {
            return DateTime.Now.Subtract(birthDate).Days;
        }

        public static int DaysToDate(this DateTime birthDate, DateTime referenceDate)
        {
            return referenceDate > birthDate ? referenceDate.Subtract(birthDate).Days : -1;
        }

        public static int MonthsToDate(this DateTime birthDate)
        {
           return DateTime.Today.Subtract(birthDate).Days/30;
        }

        public static int YearsToDate(this DateTime birthDate)
        {
            return YearsToDate(birthDate, DateTime.Today);
        }

        private static int YearsToDate(this DateTime birthDate, DateTime referenceDate)
        {
            int age = referenceDate.Year - birthDate.Year;
            if (age > 0)
            {
                age -= Convert.ToInt32(referenceDate.Date < birthDate.Date.AddYears(age));
            }
            else
            {
                age = 0;
            }
            return age;
        }

        public static DateTime FirstDayOfWeek(this DateTime date)
        {
            var daysAhead = (DayOfWeek.Sunday - (int)date.DayOfWeek);
            return date.AddDays((int)daysAhead);
        }

        public static DateTime FirstDayOfWeek(this DateTime date, DayOfWeek dayOfWeek)
        {
            var daysAhead = (dayOfWeek - (int)date.DayOfWeek);
            return date.AddDays((int)daysAhead);
        }

        public static DateTime LastDayOfWeek(this DateTime date, DayOfWeek dayOfWeek)
        {
            var daysAhead = dayOfWeek - (int)date.DayOfWeek;
            return date.AddDays((int)daysAhead);
        }

        public static DateTime LastDayOfWeek(this DateTime date)
        {
            var daysAhead = DayOfWeek.Saturday - (int)date.DayOfWeek;
            return date.AddDays((int)daysAhead);
        }

        public static DateTime FirstDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public static DateTime LastDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1);
        }

        public static DateTime FirstDayOfYear(this DateTime date)
        {
            return new DateTime(date.Year, 1, 1);
        }

        public static DateTime LastDayOfYear(this DateTime date)
        {
            return new DateTime(date.Year, 12, 31);
        }
    }
}