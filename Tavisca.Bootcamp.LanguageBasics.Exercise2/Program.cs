using System;
using System.Text.RegularExpressions;

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
            //Console.ReadKey(true);
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
            var result_prev = new TimeSpan();

            var t = new CustomTime[exactPostTime.Length];

            for (int i = 0; i < exactPostTime.Length; i++)
            {
                t[i] = new CustomTime(exactPostTime[i], showPostTime[i]);

                if (i > 0)
                    if (t[i].t_span.Equals(t[i - 1].t_span) && !t[i].read_as.Equals(t[i - 1].read_as))
                        return "impossible";

                // Seconds 
                if (t[i].read_as.Contains("seconds"))
                {
                    result = t[i].t_span;
                }
                //mins
                else if (t[i].read_as.Contains("minutes"))
                {
                    var min = new TimeSpan(0, Int32.Parse(Regex.Split(t[i].read_as, @"\s")[0]), 0);
                    result = t[i].t_span.Add(min);
                }
                //Hours
                else
                {
                    var hr = new TimeSpan(Int32.Parse(Regex.Split(t[i].read_as, @"\s")[0]), 0, 0);
                    result = t[i].t_span.Add(hr);
                }

                // More than a Day
                if (result > TimeSpan.Parse("1.00:00:00"))
                {
                    result = (result - TimeSpan.FromDays(1)).Duration();
                }

                // Getting the Lowest
                if (result < result_prev)
                {
                    result = result_prev;
                }
                else
                {
                    result_prev = result;
                }


            }
            return result.ToString();
        }

    }
}
