using Application.Validation;
using System;

namespace Presentation.Intranet.Api.Mappers.EventMappers
{
    public static class StringToDateMapper
    {
        public static DateTime MapStringToDate(this string dateTime)
        {
            List<int> dates = new List<int>();
            foreach(string date in dateTime.Split(".").ToList())
            {
                dates.Add(Convert.ToInt32(date));
            }
            if (dates.Count == 5)
            {
                return new DateTime(dates[0], dates[1], dates[2], dates[3], dates[4], 0);
            }
            return new DateTime();
        }
        public static ValidationResult StringValidationToDate(this string dateTime)
        {
            if (!dateTime.Contains('.'))
            {
                return ValidationResult.Fail("The time data should be separated by the sign '.'");
            }

            List<string> dates = dateTime.Split(".").ToList();
            if (dates.Count != 5)
            {
                return ValidationResult.Fail("The following data must be entered: year, month, day, hour, minute");
            }
            
            if (Convert.ToInt32(dates[0]) < 0)
            {
                return ValidationResult.Fail("A year cannot be less than zero");
            }

            if (Convert.ToInt32(dates[1]) < 0 || Convert.ToInt32(dates[1]) > 12)
            {
                return ValidationResult.Fail("A month cannot be less than zero and more than 12");
            }

            if (Convert.ToInt32(dates[2]) < 0 || Convert.ToInt32(dates[2]) > 31)
            {
                return ValidationResult.Fail("A day cannot be less than zero and more than 31");
            }

            if (Convert.ToInt32(dates[3]) < 0 || Convert.ToInt32(dates[2]) > 24)
            {
                return ValidationResult.Fail("A hour cannot be less than zero and more than 24");
            }

            if (Convert.ToInt32(dates[4]) < 0 || Convert.ToInt32(dates[4]) > 60)
            {
                return ValidationResult.Fail("A minute cannot be less than zero and more than 60");
            }

            return ValidationResult.Ok();
        }
    }
}
