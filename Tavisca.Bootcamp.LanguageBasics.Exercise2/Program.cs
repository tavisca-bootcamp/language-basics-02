using System;
using System.Collections.Generic;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class ForumPostEasy
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
            int length = exactPostTime.Length;
            TimeSpan[,] array = new TimeSpan[length, 2];

            Dictionary<string, string> dic = new Dictionary<string, string>();
            for (int i = 0; i < length; i++)
            {
                if (!dic.ContainsKey(exactPostTime[i]))
                {

                    dic.Add(exactPostTime[i], showPostTime[i]);
                }
                else if (!dic.GetValueOrDefault(exactPostTime[i]).Equals(showPostTime[i]))
                {
                    return "impossible";

                }

                TimeSpan timeSpan = TimeSpan.Parse(exactPostTime[i]);
                string[] showPostSplitted = showPostTime[i].Split(" ");


                double a;
                if (showPostSplitted[1] == "minutes")
                {
                    a = double.Parse(showPostSplitted[0]);
                    timeSpan = timeSpan.Add(TimeSpan.FromMinutes(a));
                    timeSpan = new TimeSpan(timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

                    array[i, 0] = timeSpan;
                    timeSpan = timeSpan.Add(TimeSpan.FromSeconds(double.Parse("59")));
                    timeSpan = new TimeSpan(timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

                    array[i, 1] = timeSpan;
                }
                else if (showPostSplitted[1] == "hours")
                {

                    a = double.Parse(showPostSplitted[0]);
                    timeSpan = timeSpan.Add(TimeSpan.FromHours(a));
                    timeSpan = new TimeSpan(timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

                    array[i, 0] = timeSpan;

                    timeSpan = timeSpan.Add(TimeSpan.FromMinutes(double.Parse("59")));
                    timeSpan = new TimeSpan(timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

                    timeSpan = timeSpan.Add(TimeSpan.FromSeconds(double.Parse("59")));
                    timeSpan = new TimeSpan(timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
                    array[i, 1] = timeSpan;
                }
                else
                {
                    array[i, 0] = timeSpan;

                    timeSpan = timeSpan.Add(TimeSpan.FromSeconds(double.Parse("59")));
                    timeSpan = new TimeSpan(timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

                    array[i, 1] = timeSpan;
                }
              

            }

            TimeSpan startTime = array[0, 0];
            TimeSpan EndTime = array[0, 1];

            for (int i = 1; i < length; i++)
            {

                if (array[i, 0] > EndTime || array[i, 1] < startTime)  return "impossible";
                

                else
                {
                    startTime = TimeSpan.Compare(startTime, array[i, 0]) == 1 ? startTime : array[i, 0];
                    EndTime = TimeSpan.Compare(startTime, array[i, 1]) == 1 ? EndTime : array[i, 1];

                }
            }

            return startTime.ToString();
        }
    }
}
