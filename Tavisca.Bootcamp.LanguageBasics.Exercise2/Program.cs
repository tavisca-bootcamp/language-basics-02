using System;
using System.Text.RegularExpressions;
using System.Linq;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise2
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(new[] { "12:12:12" }, new[] { "few seconds ago" }, "12:12:12");
            Test(new[] { "23:23:23", "23:23:23" }, new[] { "59 minutes ago", "59 minutes ago" }, "00:22:23");
            Test(new[] { "00:10:10", "00:10:10" }, new[] { "59 minutes ago", "1 hours ago" }, "impossible");
            Test(new[] { "11:59:13", "11:13:23", "12:25:15" }, new[] { "few seconds ago", "46 minutes ago", "23 hours ago" }, "11:59:23");
            `Console.ReadKey(true);
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
            var result = new TimeSpan();
            var resultPrevious = new TimeSpan();

            var cTime = new CustomTime[exactPostTime.Length];

            for (int i = 0; i < exactPostTime.Length; i++)
            {
                cTime[i] = new CustomTime(exactPostTime[i], showPostTime[i]);

                if (i > 0)
                {
                    if (DifferentReadingsForSameTime(exactPostTime, showPostTime))
                    {
                        return "impossible";
                    }
                }

                result = CalculateTime(cTime, i);
                result = MoreThanDay(result);

                GetLowestTime(ref result, ref resultPrevious);

            }
            return result.ToString();
        }

        private static TimeSpan CalculateTime(CustomTime[] t, int i)
        {
            TimeSpan result;
            if (t[i].readAs.Contains("seconds"))
            {
                result = CalculateForSeconds(t, i);
            }
            else if (t[i].readAs.Contains("minutes"))
            {
                result = CalculateForMinutes(t, i);
            }
            else
            {
                result = CalculateForHours(t, i);
            }

            return result;
        }

        private static bool DifferentReadingsForSameTime(string[] exactPostTime, string[] showPostTime)
        {
            return !int.Equals(exactPostTime.Distinct().Count(), showPostTime.Distinct().Count());
        }

        private static void GetLowestTime(ref TimeSpan result, ref TimeSpan resultPrevious)
        {
            if (result < resultPrevious)
            {
                result = resultPrevious;
            }
            else
            {
                resultPrevious = result;
            }
        }

        private static TimeSpan MoreThanDay(TimeSpan result)
        {
            if (result > TimeSpan.Parse("1.00:00:00"))
            {
                result = (result - TimeSpan.FromDays(1)).Duration();
            }

            return result;
        }

        private static TimeSpan CalculateForHours(CustomTime[] cTime, int index)
        {
            TimeSpan result;
            var hr = new TimeSpan(Int32.Parse(Regex.Split(cTime[index].readAs, @"\s")[0]), 0, 0);
            result = cTime[index].timeSpan.Add(hr);
            return result;
        }

        private static TimeSpan CalculateForMinutes(CustomTime[] cTime, int index)
        {
            TimeSpan result;
            var min = new TimeSpan(0, Int32.Parse(Regex.Split(cTime[index].readAs, @"\s")[0]), 0);
            result = cTime[index].timeSpan.Add(min);
            return result;
        }

        private static TimeSpan CalculateForSeconds(CustomTime[] cTime, int index)
        {
            return cTime[index].timeSpan;
        }
    }
}
