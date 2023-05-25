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
    }
}
