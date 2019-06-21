using System;

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
            TimeSpan currentTime = TimeSpan.Parse("0");
            for (int i = 0; i < exactPostTime.Length; i++)
            {
                for (int j = i + 1; j < exactPostTime.Length; j++)
                {
                    if (exactPostTime[i] == exactPostTime[j])
                        if (showPostTime[i] != showPostTime[j])
                            return "impossible";
                }
            }
            for (int i = 0; i < exactPostTime.Length; i++)
            {
                TimeSpan time = TimeSpan.Parse(exactPostTime[i]);
                if (showPostTime[i].Contains("sec"))
                {
                    if (currentTime < time)
                        currentTime = time;
                }
                else
                {
                    int val = int.Parse(showPostTime[i].Substring(0, showPostTime[i].IndexOf(" ")));
                    if (showPostTime[i].Contains("min"))
                    {
                        TimeSpan span = TimeSpan.FromMinutes(val);
                        if (time + span > TimeSpan.Parse("1.00:00:00")) //One Day 24 Hours
                        {
                            time = time + span - TimeSpan.FromDays(1);  //Time is greater so Subtracting
                            if (currentTime < time)
                                currentTime = time;
                        }
                        else
                             if (currentTime < time + span) //selecting the smallest
                            currentTime = time + span;
                    }
                    if (showPostTime[i].Contains("hour"))
                    {
                        TimeSpan span = TimeSpan.FromHours(val);
                        if (time + span > TimeSpan.Parse("1.00:00:00")) //One Day 24 Hours
                        {
                            time = time + span - TimeSpan.FromDays(1); //Time is greater so Subtracting
                            if (currentTime < time)
                                currentTime = time;
                        }
                        else
                            if (currentTime < time + span) //selecting the smallest
                            currentTime = time + span;
                    }
                }
            }
            return currentTime.ToString();
            throw new NotImplementedException();
        }
    }
}