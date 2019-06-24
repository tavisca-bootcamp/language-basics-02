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
            var length = exactPostTime.Length;
            var array = new List<string>();

            for(var i=0;i<length;i++)
            {
                // Check whether for same exactPostTime we have different showPostTime

                if(i!=length-1)
                {
                    if(exactPostTime[i]==exactPostTime[i+1] && showPostTime[i]!=showPostTime[i+1])
                        return "impossible";
                }

                 var postTime = TimeSpan.Parse(exactPostTime[i]);

                // add seconds in exactPostTime
                if(showPostTime[i].Contains("seconds"))
                {
                    array.Add(new TimeSpan(postTime.Hours,postTime.Minutes,postTime.Seconds).ToString());
                }

                // add minutes in exactPostTime
                else if(showPostTime[i].Contains("minutes"))
                {
                    var minutes = showPostTime[i].Split(" ")[0];
                    postTime = postTime.Add(TimeSpan.FromMinutes(Double.Parse(minutes)));
                    array.Add(new TimeSpan(postTime.Hours,postTime.Minutes,postTime.Seconds).ToString());
                }

                // add hours in exactPostTime
                else if(showPostTime[i].Contains("hours"))
                {
                    var hours = showPostTime[i].Split(" ")[0];
                    postTime = postTime.Add(TimeSpan.FromHours(Double.Parse(hours)));
                    array.Add(new TimeSpan(postTime.Hours,postTime.Minutes,postTime.Seconds).ToString());
                }
                else
                {
                    return "impossible";
                }

            }

            // Sort array and return the largest one

            array.Sort();
            return array[array.Count-1];

            
        }
    }
}
