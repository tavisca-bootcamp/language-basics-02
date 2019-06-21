using System;
using System.Collections.Generic;
using System.Linq;
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

        //Function to add hours, minutes, seconds to given time
        public static string AddTime(string postTime, int hours, int minutes, int seconds)
        {
            string[] splittedTime = postTime.Split(':');
            int currentSeconds = Convert.ToInt32(splittedTime[2]) + seconds;
            int carryMinutes = currentSeconds/60;
            currentSeconds %= 60;
            int currentMinutes = Convert.ToInt32(splittedTime[1]) + minutes + carryMinutes;
            int carryHours = currentMinutes/60;
            currentMinutes %= 60;
            int currentHours = Convert.ToInt32(splittedTime[0]) + hours + carryHours;
            currentHours %= 24;
            return new TimeSpan(currentHours, currentMinutes, currentSeconds).ToString();
        }

        public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {
            List<string> startTime = new List<string>();
            List<string> endTime = new List<string>();
            System.Console.WriteLine(startTime.Max());
            int i;
            for(i = 0; i < exactPostTime.Length; i++)
            {
                if(showPostTime[i].Equals("few seconds ago"))
                {
                    startTime.Add(AddTime(exactPostTime[i], 0, 0, 0));
                    endTime.Add(AddTime(exactPostTime[i], 0, 0, 59));
                }
                else if(showPostTime[i].Contains("minutes ago"))
                {
                    int minutes = Convert.ToInt32(showPostTime[i].Split(' ')[0]);
                    startTime.Add(AddTime(exactPostTime[i], 0, minutes, 0));
                    endTime.Add(AddTime(exactPostTime[i], 0, minutes, 59));
                }
                else if(showPostTime[i].Contains("hours ago"))
                {
                    int hours = Convert.ToInt32(showPostTime[i].Split(' ')[0]);
                    startTime.Add(AddTime(exactPostTime[i], hours, 0, 0));
                    endTime.Add(AddTime(exactPostTime[i], hours, 59, 59));
                }
            }
            //Current Time should be between maximum startTime and minimum endTime
            string currentStartTime = startTime.Max();
            string currentEndTime = endTime.Min();

            //Case 1: StartTime is after 23:00:00 and EndTime is before 01:00:00
            if(string.Compare(currentStartTime, "23:00:00") > 0 && string.Compare(currentEndTime, "01:00:00") < 0)
            {
                //Check whether there is any maximum startTime between 00:00:00 and currentEndTime
                currentStartTime = startTime.Where(s => string.Compare(s, "00:00:00") >= 0 && string.Compare(s, currentEndTime) == -1).Max();
                
                //If there is no startTime after 00:00:00, then currentEndTime is lexicographically smaller
                if(currentStartTime == null)
                    return currentEndTime;
                //If there is startTime after 00:00:00, then currentStartTime is lexicographically smaller
                else
                    return currentStartTime;
            }

            //Case 2: StartTime is after 00:00:00 and EndTime is before 23:59:59
            else 
            {
                //If startTime is greater than endTime then currentTime is impossible 
                if(string.Compare(currentStartTime, currentEndTime) > 0)
                    return "impossible";
                //StartTime is always lexicographically smaller than EndTime
                else
                    return currentStartTime;
            }
        }
    }
}
