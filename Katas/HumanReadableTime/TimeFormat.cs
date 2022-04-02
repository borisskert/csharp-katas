namespace Katas.HumanReadableTime
{
    using System;

    /**
     * https://www.codewars.com/kata/52685f7382004e774f0001f7/train/csharp
     */
    public static class TimeFormat
    {
        public static string GetReadableTime(int seconds)
        {
            var timespan = TimeSpan.FromSeconds(seconds);
            return $"{(int) timespan.TotalHours:d2}:{timespan.Minutes:d2}:{timespan.Seconds:d2}";
        }
    }
}
