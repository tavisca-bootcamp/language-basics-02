using System;
using System.Linq;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class ForumPostEasy
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
            // Add your code here.
            TimeSpan[] minTimeSpan = new TimeSpan[exactPostTime.Length], maxTimeSpan = new TimeSpan[exactPostTime.Length];
            CalculateMinAndMaxTimePossible(minTimeSpan, maxTimeSpan, 0, exactPostTime, showPostTime);
            for (int i = 1;i < exactPostTime.Length; i++)
            {
                CalculateMinAndMaxTimePossible(minTimeSpan, maxTimeSpan, i, exactPostTime, showPostTime);
            }
            /*boundary condition to check if the given time is 23:59:59 few seconds ago
             * then our minTimeSpan will be 23:59:59 and maxTimeSpan will be 00:00:58 then
             * time 00:00:00 is lexicographically smaller
             */
            if(TimeSpan.Compare(maxTimeSpan.Min(), minTimeSpan.Max())==-1)
            {
                if (TimeSpan.Compare(minTimeSpan.Max(), new TimeSpan(0, 0, 0)) == -1 && (TimeSpan.Compare(maxTimeSpan.Min(), new TimeSpan(0, 0, 0)) == 1 || TimeSpan.Compare(maxTimeSpan.Min(), new TimeSpan(0, 0, 0)) == 0))
                {
                    return "00:00:00";
                }
                else
                {
                    return "impossible";
                }
            }
            else
            {
                return minTimeSpan.Max().ToString();
            }
        }

        private static void CalculateMinAndMaxTimePossible(TimeSpan[] minTimeSpan, TimeSpan[] maxTimeSpan, int i, string[] exactPostTime, string[] showPostTime)
        {
            String[] splitGivenTime = exactPostTime[i].Split(':');
            TimeSpan temp = new TimeSpan(Int32.Parse(splitGivenTime[0]), Int32.Parse(splitGivenTime[1]), Int32.Parse(splitGivenTime[2]));
            if (showPostTime[i].Contains("second"))
            {
                minTimeSpan[i] = temp;
                maxTimeSpan[i] = new TimeSpan(0, 0, 59) + minTimeSpan[i];
                minTimeSpan[i] = RemoveDaysIfTimeExceedsTwetyFourHours(minTimeSpan[i]);
                maxTimeSpan[i] = RemoveDaysIfTimeExceedsTwetyFourHours(maxTimeSpan[i]);
            }
            if (showPostTime[i].Contains("minute"))
            {
                minTimeSpan[i] = new TimeSpan(0, Int32.Parse(showPostTime[i].Split(' ')[0]), 0) + temp;
                maxTimeSpan[i] = new TimeSpan(0, 0, 59) + minTimeSpan[i];
                minTimeSpan[i] = RemoveDaysIfTimeExceedsTwetyFourHours(minTimeSpan[i]);
                minTimeSpan[i] = RemoveDaysIfTimeExceedsTwetyFourHours(minTimeSpan[i]);
                maxTimeSpan[i] = RemoveDaysIfTimeExceedsTwetyFourHours(maxTimeSpan[i]);
            }
            if (showPostTime[i].Contains("hour"))
            {
                minTimeSpan[i] = new TimeSpan(Int32.Parse(showPostTime[i].Split(' ')[0]), 0, 0) + temp;
                maxTimeSpan[i] = new TimeSpan(0, 59, 59) + minTimeSpan[i];
                minTimeSpan[i] = RemoveDaysIfTimeExceedsTwetyFourHours(minTimeSpan[i]);
                maxTimeSpan[i] = RemoveDaysIfTimeExceedsTwetyFourHours(maxTimeSpan[i]);
            }
        }
        /* when time is 23:59:59 few seconds ago then adding
         * 59 seconds will give 1.00:00:58 where 1 denotes
         * days so removing the days from the time
        */
        private static TimeSpan RemoveDaysIfTimeExceedsTwetyFourHours(TimeSpan timeSpan)
        {
            if (timeSpan.ToString().Contains("."))
            {
                String[] tempString = timeSpan.ToString().Split('.')[1].Split(':');
                timeSpan = new TimeSpan(Int32.Parse(tempString[0]), Int32.Parse(tempString[1]), Int32.Parse(tempString[2]));
            }
            return timeSpan;
        }
    }
}
