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
        public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime){
            int n = exactPostTime.Length;
            String[] currentTime = new String[n];
            for (var i = 0; i < n; i++) {
                for (int j = i + 1; j < exactPostTime.Length; j++) {
                    if (exactPostTime[i] == exactPostTime[j] && showPostTime[i] != showPostTime[j])
                            return "impossible";
                }
                TimeSpan exact_i = TimeSpan.Parse(exactPostTime[i]);
                if (showPostTime[i].Contains("seconds")) {
                    currentTime[i] = exactPostTime[i];
                }
                else if (showPostTime[i].Contains("minutes")) {
                    int minute_count = int.Parse(showPostTime[i].Split(' ')[0]);
                    TimeSpan t = TimeSpan.FromMinutes(minute_count);
                    currentTime[i] = exact_i.Add(t).ToString(@"hh\:mm\:ss");
                }
                else if (showPostTime[i].Contains("hours")) {
                    int hour_count = int.Parse(showPostTime[i].Split(' ')[0]);
                    TimeSpan t = TimeSpan.FromHours(hour_count);
                    currentTime[i] = exact_i.Add(t).ToString(@"hh\:mm\:ss");
                }
            }
            Array.Sort(currentTime);
            return currentTime[n-1];
        }
    }
}
