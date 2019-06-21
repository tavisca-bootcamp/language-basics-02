using System;
using System.Collections.Generic;
namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(new[] {"12:12:12"}, new [] { "few seconds ago" }, "12:12:12");
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
            var currTime = new List<TimeSpan>();
            for (int i=0;i<exactPostTime.Length;i++)
            {
                for(int j=i+1;j<exactPostTime.Length;j++)
                {
                    if(exactPostTime[i]==exactPostTime[j])
                    {
                        if (showPostTime[i] != showPostTime[j])
                            return "impossible";
                    }
                }
                TimeSpan timeSpan = TimeSpan.Parse(exactPostTime[i]);
                if (showPostTime[i].Contains("few"))
                {
                    TimeSpan time = new TimeSpan(timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
                    currTime.Add(time);
                }
                else if(showPostTime[i].Contains("minutes"))
                {
                    string minute = showPostTime[i].Split(' ')[0];
                    timeSpan = timeSpan.Add(TimeSpan.FromMinutes(double.Parse(minute)));
                    TimeSpan time = new TimeSpan(timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
                    currTime.Add(time);
                }
                else
                {
                    string hours = showPostTime[i].Split(' ')[0];
                    timeSpan = timeSpan.Add(TimeSpan.FromHours(double.Parse(hours)));
                    TimeSpan time = new TimeSpan(timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
                    currTime.Add(time);
                }
                
            }
            currTime.Sort();
            return currTime[currTime.Count - 1].ToString();
            throw new NotImplementedException();
        }
    }
}
