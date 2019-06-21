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
            int length = exactPostTime.Length;
            string[] array = new string[length];
            for (var i = 0 ; i < length ; i++ ){
                if(i < length-1 && exactPostTime[i] == exactPostTime[i+1] && showPostTime[i] != showPostTime[i+1]){
                    return "impossible";
                }
                TimeSpan stime = TimeSpan.Parse(exactPostTime[i]);
                TimeSpan etime;
                if (showPostTime[i].Contains("seconds")){
                    etime = new TimeSpan(stime.Hours, stime.Minutes, stime.Seconds);
                    array[i] = etime.ToString();
                }else if(showPostTime[i].Contains("minutes")){
                    var minutes_num = showPostTime[i].Split(' ')[0];
                    var double_minute = double.Parse(minutes_num);
                    var time2 = TimeSpan.FromMinutes(double_minute);
                    stime = stime.Add(time2);
                    etime = new TimeSpan(stime.Hours,stime.Minutes,stime.Seconds);
                    array[i] = etime.ToString();
                }else if(showPostTime[i].Contains("hours")){
                    var hours_num = showPostTime[i].Split(' ')[0];
                    var double_hours = double.Parse(hours_num);
                    var time2 = TimeSpan.FromHours(double_hours);
                    stime = stime.Add(time2);
                    etime = new TimeSpan(stime.Hours,stime.Minutes,stime.Seconds);
                    array[i] = stime.ToString();
                }else{
                    return "impossible";
                }

            }
            Array.Sort(array);
            return array[array.Length-1];
        }
    }
}
