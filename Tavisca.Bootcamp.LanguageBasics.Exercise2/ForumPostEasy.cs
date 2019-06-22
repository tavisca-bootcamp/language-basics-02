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

            if (showPostTime[0].Contains("second"))
            {
                minTimeSpan[0] = new TimeSpan(Int32.Parse(exactPostTime[0].Split(':')[0]), Int32.Parse(exactPostTime[0].Split(':')[1]), Int32.Parse(exactPostTime[0].Split(':')[2]));
                maxTimeSpan[0] = new TimeSpan(0, 0, 59)+minTimeSpan[0];
                if (minTimeSpan[0].ToString().Contains("."))
                {
                    String[] tempString = minTimeSpan[0].ToString().Split('.')[1].Split(':');
                    minTimeSpan[0] = new TimeSpan(Int32.Parse(tempString[0]), Int32.Parse(tempString[1]), Int32.Parse(tempString[2]));
                }
                if (maxTimeSpan[0].ToString().Contains("."))
                {
                    String[] tempString = maxTimeSpan[0].ToString().Split('.')[1].Split(':');
                    maxTimeSpan[0] = new TimeSpan(Int32.Parse(tempString[0]), Int32.Parse(tempString[1]), Int32.Parse(tempString[2]));
                }
            }
            if (showPostTime[0].Contains("minute"))
            {
                TimeSpan temp = new TimeSpan(Int32.Parse(exactPostTime[0].Split(':')[0]), Int32.Parse(exactPostTime[0].Split(':')[1]), Int32.Parse(exactPostTime[0].Split(':')[2]));
                minTimeSpan[0] = new TimeSpan(0, Int32.Parse(showPostTime[0].Split(' ')[0]), 0) + temp;
                maxTimeSpan[0] = new TimeSpan(0, 0, 59) + minTimeSpan[0];
                if (minTimeSpan[0].ToString().Contains("."))
                {
                    String[] tempString = minTimeSpan[0].ToString().Split('.')[1].Split(':');
                    minTimeSpan[0] = new TimeSpan(Int32.Parse(tempString[0]), Int32.Parse(tempString[1]), Int32.Parse(tempString[2]));
                }
                if (maxTimeSpan[0].ToString().Contains("."))
                {
                    String[] tempString = maxTimeSpan[0].ToString().Split('.')[1].Split(':');
                    maxTimeSpan[0] = new TimeSpan(Int32.Parse(tempString[0]), Int32.Parse(tempString[1]), Int32.Parse(tempString[2]));
                }
            }
            if (showPostTime[0].Contains("hour"))
            {
                TimeSpan temp = new TimeSpan(Int32.Parse(exactPostTime[0].Split(':')[0]), Int32.Parse(exactPostTime[0].Split(':')[1]), Int32.Parse(exactPostTime[0].Split(':')[2]));
                minTimeSpan[0] = new TimeSpan(Int32.Parse(showPostTime[0].Split(' ')[0]), 0, 0) + temp;
                maxTimeSpan[0] = new TimeSpan(0, 59, 59) + minTimeSpan[0];
                if (minTimeSpan[0].ToString().Contains("."))
                {
                    String[] tempString = minTimeSpan[0].ToString().Split('.')[1].Split(':');
                    minTimeSpan[0] = new TimeSpan(Int32.Parse(tempString[0]), Int32.Parse(tempString[1]), Int32.Parse(tempString[2]));
                }
                if (maxTimeSpan[0].ToString().Contains("."))
                {
                    String[] tempString = maxTimeSpan[0].ToString().Split('.')[1].Split(':');
                    maxTimeSpan[0] = new TimeSpan(Int32.Parse(tempString[0]), Int32.Parse(tempString[1]), Int32.Parse(tempString[2]));
                }
            }
            for(int i = 1;i < exactPostTime.Length; i++)
            {
                if (showPostTime[i].Contains("second"))
                {
                    minTimeSpan[i] = new TimeSpan(Int32.Parse(exactPostTime[i].Split(':')[0]), Int32.Parse(exactPostTime[i].Split(':')[1]), Int32.Parse(exactPostTime[i].Split(':')[2]));
                    maxTimeSpan[i] = new TimeSpan(0, 0, 59) + minTimeSpan[i];
                    if (minTimeSpan[i].ToString().Contains("."))
                    {
                        String[] tempString = minTimeSpan[i].ToString().Split('.')[1].Split(':');
                        minTimeSpan[i] = new TimeSpan(Int32.Parse(tempString[0]), Int32.Parse(tempString[1]), Int32.Parse(tempString[2]));
                    }
                    if (maxTimeSpan[i].ToString().Contains("."))
                    {
                        String[] tempString = maxTimeSpan[i].ToString().Split('.')[1].Split(':');
                        maxTimeSpan[i] = new TimeSpan(Int32.Parse(tempString[0]), Int32.Parse(tempString[1]), Int32.Parse(tempString[2]));
                    }
                }
                if (showPostTime[i].Contains("minute"))
                {
                    TimeSpan temp = new TimeSpan(Int32.Parse(exactPostTime[i].Split(':')[0]), Int32.Parse(exactPostTime[i].Split(':')[1]), Int32.Parse(exactPostTime[i].Split(':')[2]));
                    minTimeSpan[i] = new TimeSpan(0, Int32.Parse(showPostTime[i].Split(' ')[0]), 0) + temp;
                    maxTimeSpan[i] = new TimeSpan(0, 0, 59) + minTimeSpan[i];
                    if (minTimeSpan[i].ToString().Contains("."))
                    {
                        String[] tempString = minTimeSpan[i].ToString().Split('.')[1].Split(':');
                        minTimeSpan[i] = new TimeSpan(Int32.Parse(tempString[0]), Int32.Parse(tempString[1]), Int32.Parse(tempString[2]));
                    }
                    if (maxTimeSpan[i].ToString().Contains("."))
                    {
                        String[] tempString = maxTimeSpan[i].ToString().Split('.')[1].Split(':');
                        maxTimeSpan[i] = new TimeSpan(Int32.Parse(tempString[0]), Int32.Parse(tempString[1]), Int32.Parse(tempString[2]));
                    }
                }
                if (showPostTime[i].Contains("hour"))
                {
                    TimeSpan temp = new TimeSpan(Int32.Parse(exactPostTime[i].Split(':')[0]), Int32.Parse(exactPostTime[i].Split(':')[1]), Int32.Parse(exactPostTime[i].Split(':')[2]));
                    minTimeSpan[i] = new TimeSpan(Int32.Parse(showPostTime[i].Split(' ')[0]), 0, 0) + temp;
                    maxTimeSpan[i] = new TimeSpan(0, 59, 59) + minTimeSpan[i];
                    if (minTimeSpan[i].ToString().Contains("."))
                    {
                        String[] tempString = minTimeSpan[i].ToString().Split('.')[1].Split(':');
                        minTimeSpan[i] = new TimeSpan(Int32.Parse(tempString[0]), Int32.Parse(tempString[1]), Int32.Parse(tempString[2]));
                    }
                    if (maxTimeSpan[i].ToString().Contains("."))
                    {
                        String[] tempString = maxTimeSpan[i].ToString().Split('.')[1].Split(':');
                        maxTimeSpan[i] = new TimeSpan(Int32.Parse(tempString[0]), Int32.Parse(tempString[1]), Int32.Parse(tempString[2]));
                    }
                }
            }
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
            throw new NotImplementedException();
        }
    }
}
