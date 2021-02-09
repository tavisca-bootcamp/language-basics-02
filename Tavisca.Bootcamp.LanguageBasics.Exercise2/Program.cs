using System;
using System.Collections.Generic;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    // this class's object will store 
    // the minimum and the maximum of the post that can be made
    public class TimeRange
    {
        public string InitialTime;
        public string MinTime;
        public string MaxTime;
        public TimeRange(){}
        public TimeRange(string t1, string t2, string t3)
        {
            InitialTime = t1;
            MinTime = t2;
            MaxTime = t3;
        }
    }
    public static class Program
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
            List<TimeRange> RangedTime = new List<TimeRange>();

            // storing minimum and maximum post time 
            for (int index = 0; index < exactPostTime.Length; ++index)
            {
                TimeRange time = new TimeRange();
                time = Program.ExtractShowTime(showPostTime[index], exactPostTime[index]);
                RangedTime.Add(time);
            }
            TimeSpan leftPointer = TimeSpan.Parse(RangedTime[0].MinTime);
            TimeSpan rightPointer = TimeSpan.Parse(RangedTime[0].MaxTime);

            // finding intersecting time period
            for (int i = 1; i < RangedTime.Count; ++i)
            {
                TimeSpan MinTime = TimeSpan.Parse(RangedTime[i].MinTime);
                TimeSpan MaxTime = TimeSpan.Parse(RangedTime[i].MaxTime);
                if (TimeSpan.Compare(MinTime, rightPointer) > 0 || TimeSpan.Compare(MaxTime, leftPointer) < 0)
                {
                    return "impossible";
                }
                else
                {
                    if (TimeSpan.Compare(MinTime, leftPointer) >= 0)
                    {
                        leftPointer = MinTime;
                    }
                    if (TimeSpan.Compare(MaxTime, rightPointer) <= 0)
                    {
                        rightPointer = MaxTime;
                    }
                }
            }
            string result = leftPointer.ToString();

            return result;
            throw new NotImplementedException();
        }

        static TimeRange ExtractShowTime(string showPostTimeMsg, string time1)
        {
            string MinRange = "";
            string MaxRange = "";

            if (showPostTimeMsg[0] == 'f')
            {
                MinRange = "00:00:00";
                MaxRange += "00:00:59";
            }
            if (showPostTimeMsg[3] == 'm' || showPostTimeMsg[3] == 'i')
            {
                string tempMinite = "";
                MinRange += "00:";
                if (showPostTimeMsg[0] >= '0' && showPostTimeMsg[0] <= '9')
                    tempMinite += showPostTimeMsg[0];
                if (showPostTimeMsg[1] >= '0' && showPostTimeMsg[1] <= '9')
                    tempMinite += showPostTimeMsg[1];
                MinRange += tempMinite + ":00";

                MaxRange += "00:" + tempMinite + ":59";
            }

            if (showPostTimeMsg[3] == 'h' || showPostTimeMsg[3] == 'o')
            {
                string tempHour = "";
                if (showPostTimeMsg[0] >= '0' && showPostTimeMsg[0] <= '9')
                {
                    tempHour += showPostTimeMsg[0];
                }
                if (showPostTimeMsg[1] >= '0' && showPostTimeMsg[1] <= '9')
                {
                    tempHour += showPostTimeMsg[1];
                }
                MinRange += tempHour + ":00:00";

                MaxRange += tempHour + ":59:59";
            }

            TimeSpan t1 = TimeSpan.Parse(time1);
            TimeSpan t2 = TimeSpan.Parse(MinRange);
            TimeSpan t3 = TimeSpan.Parse(MaxRange);


            t2 = t1.Add(t2);
            t3 = t1.Add(t3);

            string MinTimeX = t2.Hours.ToString() + ":" + t2.Minutes.ToString() + ":" + t2.Seconds.ToString();
            string MaxTimeY = t3.Hours.ToString() + ":" + t3.Minutes.ToString() + ":" + t3.Seconds.ToString();

            TimeRange time = new TimeRange(time1, MinTimeX, MaxTimeY);

            return time;
        }
    }
}
