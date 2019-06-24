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
        public static int Time(string posttime,int value) // this function is about retrieving the values from showposttime
        {
            string answer;
            if (posttime.Length == value)
            {
                answer = posttime.Substring(0, 2);
            }
            else
            {
                answer = posttime.Substring(0, 1);
            }
            int x = int.Parse(answer);
            return x;
        }
        public static string convert(TimeSpan x)
        {
            string answer="";
            if (x.Hours >= 0 && x.Hours <= 9)
                answer += ("0" + Convert.ToString(x.Hours));
            else
                answer += Convert.ToString(x.Hours);
            answer += ":";
            if (x.Minutes >= 0 && x.Minutes <= 9)
                answer+= ("0" + Convert.ToString(x.Minutes));
            else
                answer+= Convert.ToString(x.Minutes);
            answer += ":";
            if (x.Seconds >= 0 && x.Seconds <= 9)
                answer+= ("0" + Convert.ToString(x.Seconds));
            else
                answer+= Convert.ToString(x.Seconds);

            return answer;
        }
        public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {
            // Add your code here.
            if (exactPostTime.Length != showPostTime.Length || exactPostTime.Length <= 0)
                return null;
            int i=0;
            string answer;
            string sec = "few";
            string minute = "minute";
            TimeSpan min = TimeSpan.Parse(exactPostTime[0]);
            TimeSpan max = TimeSpan.Parse(exactPostTime[0]);
            TimeSpan min1= TimeSpan.Parse(exactPostTime[0]), max1= TimeSpan.Parse(exactPostTime[0]);

            for (i=0;i<exactPostTime.Length;i++)
            {
                TimeSpan exact = TimeSpan.Parse(exactPostTime[i]);
                
                if (showPostTime[i].Contains(sec))
                {
                     min1 = TimeSpan.Parse("00:00:00");
                     max1 = TimeSpan.Parse("00:00:59");
                }
                else if(showPostTime[i].Contains(minute))
                {
                    int x = Time(showPostTime[i], 14);
                    min1 = new TimeSpan(0, x, 0);
                     max1 = new TimeSpan(0, x, 59);
                }
                else
                {
                    int x = Time(showPostTime[i], 12);
                    min1 = new TimeSpan(x, 0, 0);
                     max1 = new TimeSpan(x, 59, 59);
                }
                if (i != 0)
                {
                    min1 = min1.Add(exact);
                    answer = Convert.ToString(min1.Hours) + ":" + Convert.ToString(min1.Minutes) + ":" + Convert.ToString(min1.Seconds);
                    min1 = TimeSpan.Parse(answer);
                    max1 = max1.Add(exact);
                    answer = Convert.ToString(max1.Hours) + ":" + Convert.ToString(max1.Minutes) + ":" + Convert.ToString(max1.Seconds);
                    max1 = TimeSpan.Parse(answer);
                    if (TimeSpan.Compare(min1, max) == 1)
                        return "impossible";
                    if (TimeSpan.Compare(min1, min) == 1)
                        min = min1;
                    if (TimeSpan.Compare(max1, max) == 1)
                        max = max1;
                }
                else
                {
                    min1 = min1.Add(exact);
                    answer = Convert.ToString(min1.Hours) + ":" + Convert.ToString(min1.Minutes) + ":" + Convert.ToString(min1.Seconds);
                    min1 = TimeSpan.Parse(answer);
                    max1 = max1.Add(exact);
                    answer = Convert.ToString(max1.Hours) + ":" + Convert.ToString(max1.Minutes) + ":" + Convert.ToString(max1.Seconds);
                    max1 = TimeSpan.Parse(answer);
                    min = min1;
                    max = max1;
                    //Console.WriteLine(min);
                }
            }
            return convert(min);
        }
    }
    
}
