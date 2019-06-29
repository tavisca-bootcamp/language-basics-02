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
            int n = exactPostTime.Length;
            int[,] startTime= new int[n,3];
            int[,] endTime = new int[n,3];
            int hours = 0, minutes = 0, seconds = 0, temp = 0;
            for (int i = 0; i < n; i++)
            {
                 Int32.TryParse(exactPostTime[i].Substring(0, 2),out hours);
                 Int32.TryParse(exactPostTime[i].Substring(3, 2),out minutes);
                 Int32.TryParse(exactPostTime[i].Substring(6, 2),out seconds);
                var timeInterval = new TimeInterval(hours, minutes, seconds);
                string postTime = showPostTime[i];
                if(postTime[0] == 'f')
                {
                    timeInterval.CalculateStartTime(0, 0, 0);
                    timeInterval.CalculateEndTime(0, 0, 59);
                }
                else if (postTime[2] == 'm' || postTime[3] == 'm')
                {
                    if (postTime[1] == ' ')
                        Int32.TryParse(postTime.Substring(0, 1),out temp);
                    else
                        Int32.TryParse(postTime.Substring(0, 2),out temp);
                    timeInterval.CalculateStartTime(0, temp, 0);
                    timeInterval.CalculateEndTime(0, 0, 59);
                   
                }
                else if (postTime[2] == 'h' || postTime[3] == 'h')
                {
                    if (postTime[1] == ' ')
                         Int32.TryParse(postTime.Substring(0, 1),out temp);
                    else
                        Int32.TryParse(postTime.Substring(0, 2),out temp);
                    timeInterval.CalculateStartTime(temp,0, 0);
                    timeInterval.CalculateEndTime(0, 59, 59);
                }
                startTime[i,0] = timeInterval.StartTime[0];   endTime[i,0] = timeInterval.EndTime[0];
                startTime[i,1] = timeInterval.StartTime[1];   endTime[i,1] = timeInterval.EndTime[1];
                startTime[i,2] = timeInterval.StartTime[2];   endTime[i,2] = timeInterval.EndTime[2];

            }
            int[] result = new int[2];
            if (TimeInterval.FindIntersection(startTime, endTime, result,n))
                return TimeInterval.GettingStringFormat(result);
            else
                return "impossible";
        }
    }
}
