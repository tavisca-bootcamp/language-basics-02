using System;
using System.Collections.Generic;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(new[] { "12:12:12" }, new[] { "few seconds ago" }, "12:12:12");
            Test(new[] { "23:23:23", "23:23:23" }, new[] { "59 minutes ago", "59 minutes ago" }, "00:22:23");
            Test(new[] { "00:10:10", "00:10:10" }, new[] { "59 minutes ago", "1 hours ago" }, "impossible");
            Test(new[] { "11:59:13", "11:13:23", "12:25:15" }, new[] { "few seconds ago", "46 minutes ago", "23 hours ago" }, "11:59:23");
            Console.ReadKey(true);
        }

        private static void Test(string[] postTimes, string[] showTimes, string expected)
        {
            var result = GetCurrentTime(postTimes, showTimes).Equals(expected) ? "PASS" : "FAIL";
            var postTimesCsv = string.Join(", ", postTimes);
            var showTimesCsv = string.Join(", ", showTimes);
            Console.WriteLine($"[{postTimesCsv}], [{showTimesCsv}] => {result}");
        }

        public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {
            int l = exactPostTime.Length;
            DateTime[][] currentTimeRange = new DateTime[l][];
            for (int i = 0; i < l; i++)
            {
                DateTime lower, upper;
                (lower, upper) = getCurrentTimeRange(showPostTime[i],exactPostTime[i]);
                currentTimeRange[i] =new DateTime[] { lower,upper };
            }
            return findCurrentTime(currentTimeRange,l);
        }

        private static string findCurrentTime(DateTime[][] currentTimeRange, int l)
        {
            foreach(DateTime[] selectedRange in currentTimeRange)
            {
                int cnt = 0;
                foreach (DateTime[] selectedRange2 in currentTimeRange)
                {
                    if (IsSelectedRangeValid(selectedRange,selectedRange2))
                    {
                        cnt++;
                    }
                    else
                    {
                        break;
                    }
                }
                if (cnt == l)
                {
                    return selectedRange[0].ToString("HH:mm:ss");
                }
            }
            return "impossible";
        }

        private static bool IsSelectedRangeValid(DateTime[] selectedRange, DateTime[] selectedRange2)
        {
            if (selectedRange[0] >= selectedRange2[0] && selectedRange[0] <= selectedRange2[1])
            {
                return true;
            }
            return false;
        }

        public static (DateTime lower, DateTime upper) getCurrentTimeRange(string showPostTime, string exactPostTime)
        {
            DateTime lower, upper;
            lower = DateTime.Parse(exactPostTime);
            upper = lower.AddSeconds(59);
            if (showPostTime.Contains("minutes"))
            {
                int min = Convert.ToInt32(showPostTime.Substring(0, showPostTime.IndexOf('m') - 1));
                lower = lower.AddMinutes(min);
                upper = lower.AddSeconds(59);
            }
            else if (showPostTime.Contains("hours"))
            {
                int hr = Convert.ToInt32(showPostTime.Substring(0, showPostTime.IndexOf('h') - 1));
                lower = lower.AddHours(hr);
                upper = lower.AddMinutes(59);
                upper = upper.AddSeconds(59);
            }
            lower = DateTime.Parse(lower.ToString("HH:mm:ss"));
            upper = DateTime.Parse(upper.ToString("HH:mm:ss"));
            return (lower, upper); 
        }
    }
}