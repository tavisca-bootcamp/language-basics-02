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
            var currentTime = new List<string>();

            for (var i = 0; i < exactPostTime.Length; i++)
            {
                if (i != exactPostTime.Length - 1)
                {
                    if (exactPostTime[i] == exactPostTime[i + 1] && showPostTime[i] != showPostTime[i + 1])
                        return "impossible";
                }

                var postTime = TimeSpan.Parse(exactPostTime[i]);

                if (showPostTime[i].Contains("seconds"))
                {
                    currentTime.Add(new TimeSpan(postTime.Hours, postTime.Minutes, postTime.Seconds).ToString());
                }

                else if (showPostTime[i].Contains("minutes"))
                {
                    var minutesToBeAdded = showPostTime[i].Split(" ")[0];
                    postTime = postTime.Add(TimeSpan.FromMinutes(Double.Parse(minutesToBeAdded)));
                    currentTime.Add(new TimeSpan(postTime.Hours, postTime.Minutes, postTime.Seconds).ToString());
                }

                else if (showPostTime[i].Contains("hours"))
                {
                    var hoursToBeAdded = showPostTime[i].Split(" ")[0];
                    postTime = postTime.Add(TimeSpan.FromHours(Double.Parse(hoursToBeAdded)));
                    currentTime.Add(new TimeSpan(postTime.Hours, postTime.Minutes, postTime.Seconds).ToString());
                }
            }

            if (currentTime.Count != 0)
            {
                currentTime.Sort();
                return currentTime[currentTime.Count - 1];
            }
            else return "impossible";
            //throw new NotImplementedException();
        }
    }
}