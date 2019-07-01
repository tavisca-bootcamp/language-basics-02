using System;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        public static readonly string SECONDSAGO = "seconds";
        public static readonly string MINUTESAGO = "minutes";
        public static readonly string HOURSAGO = "hours";
        public static readonly int HOURS = 0;
        public static readonly int MINUTES = 1;
        public static readonly int SECONDS = 2;


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
            int n = exactPostTime.Length;
            int[,] startTime = new int[n, 3];
            int[,] endTime = new int[n, 3];
            /* startTimes and EndTimes are used for maintaining every interval
             * index 0 for hours
             * index 1 for minutes
             * index 2 for seconds
             */
            int hours = 0, minutes = 0, seconds = 0, temp = 0;
            for (int i = 0; i < n; i++)
            {
                string[] exactTime = exactPostTime[i].Split(":");
                Int32.TryParse(exactTime[0], out hours);
                Int32.TryParse(exactTime[1], out minutes);
                Int32.TryParse(exactTime[2], out seconds);
                var timeInterval = new TimeInterval(hours, minutes, seconds);
                string[] showTime = showPostTime[i].Split(" ");
                if (showTime[1].Equals(SECONDSAGO))
                {
                    timeInterval.CalculateStartTime(0, 0, 0);
                    timeInterval.CalculateEndTime(0, 0, 59);
                }
                else if (showTime[1].Equals(MINUTESAGO))
                {

                    Int32.TryParse(showTime[0], out temp);
                    timeInterval.CalculateStartTime(0, temp, 0);
                    timeInterval.CalculateEndTime(0, 0, 59);

                }
                else if (showTime[1].Equals(HOURSAGO))
                {

                    Int32.TryParse(showTime[0], out temp);
                    timeInterval.CalculateStartTime(temp, 0, 0);
                    timeInterval.CalculateEndTime(0, 59, 59);
                }
                timeInterval.UpdateTimeInterval(startTime, endTime, i);


            }
            int[] result = new int[2];
            if (FindIntersection(startTime, endTime, result, n))
                return GettingStringFormat(result);
            else
                return "impossible";
        }
        public static bool FindIntersection(int[,] start, int[,] end, int[] result, int n)
        {  //converting into integers to find intersection
            int STARTTIME = 0, ENDTIME = 1;
            int[] temp = new int[2];
            result[STARTTIME] = ConvertingIntoSeconds(start[0,HOURS],start[0,MINUTES],start[0,SECONDS]);
            result[ENDTIME] = ConvertingIntoSeconds(end[0, HOURS], end[0, MINUTES], end[0, SECONDS]);
            /*below code is for finding intersection of all timeintervals and 
             *for evry loop updating intersection in result array
             */
            for (int i = 1; i < n; i++)
            {
                temp[STARTTIME] = ConvertingIntoSeconds(start[i, HOURS], start[i, MINUTES], start[i, SECONDS]);
                temp[ENDTIME] = ConvertingIntoSeconds(end[i, HOURS], end[i, MINUTES], end[i, SECONDS]);
                
                if (temp[STARTTIME] >= result[STARTTIME] && temp[STARTTIME] < result[ENDTIME])
                {
                    result[STARTTIME] = temp[STARTTIME];
                    result[ENDTIME] = Math.Min(temp[ENDTIME], result[ENDTIME]);
                }
                else if (result[STARTTIME] >= temp[STARTTIME] && result[STARTTIME] < temp[ENDTIME])
                {
                    result[ENDTIME] = Math.Min(temp[ENDTIME], result[ENDTIME]);
                }
                else
                    return false;
            }
            return true;
        }
        public static int ConvertingIntoSeconds(int hours,int minutes,int seconds)
        {
            return hours * 3600 + minutes * 60 + seconds;
        }
        public static string GettingStringFormat(int[] tempSeconds)
        {
            string result = "00:00:00";
            
            if (tempSeconds[0] < tempSeconds[1])
            
            {
                 int hours = tempSeconds[0] / 3600;
               int minutes = (tempSeconds[0]/60)% 60;
                int seconds = tempSeconds[0] % 60;
                TimeSpan time = new TimeSpan(hours, minutes,seconds);
               result = string.Format("{0:D2}:{1:D2}:{2:D2}", time.Hours, time.Minutes, time.Seconds);
            }
            return result;
         }
    }
}
