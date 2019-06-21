using System;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(new[] {"12:12:12"}, new [] { "few seconds ago" }, "00:00:00");
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
            if (exactPostTime.Length != showPostTime.Length || exactPostTime.Length <= 0)
                return null;
            int i=0;
            string answer;
            string sec = "few";
            string minute = "minute";
            TimeSpan min = TimeSpan.Parse(exactPostTime[0]);
            TimeSpan max = TimeSpan.Parse(exactPostTime[0]);
            if (showPostTime[0].Contains(sec))
            {
                TimeSpan min_sec = TimeSpan.Parse("00:00:00");
                TimeSpan max_sec = TimeSpan.Parse("00:00:59");
                min = min.Add(min_sec);
                 answer = Convert.ToString(min.Hours) + ":" + Convert.ToString(min.Minutes) + ":" + Convert.ToString(min.Seconds);
                min = TimeSpan.Parse(answer);
                max = max.Add(max_sec);
                answer = Convert.ToString(max.Hours) + ":" + Convert.ToString(max.Minutes) + ":" + Convert.ToString(max.Seconds);
                max = TimeSpan.Parse(answer);
            }
            else if (showPostTime[0].Contains(minute))
            {
                string minut;
                if (showPostTime[i].Length == 14)
                {
                    minut = showPostTime[i].Substring(0, 2);
                }
                else
                {
                    minut = showPostTime[i].Substring(0, 1);
                }
                int x = int.Parse(minut);
                TimeSpan min_min = new TimeSpan(0, x, 0);
                TimeSpan max_min = new TimeSpan(0, x, 59);
                min = min.Add(min_min);
                answer = Convert.ToString(min.Hours) + ":" + Convert.ToString(min.Minutes) + ":" + Convert.ToString(min.Seconds);
                min = TimeSpan.Parse(answer);
                max = max.Add(max_min);
                answer = Convert.ToString(max.Hours) + ":" + Convert.ToString(max.Minutes) + ":" + Convert.ToString(max.Seconds);
                max = TimeSpan.Parse(answer);
            }
            else
            {
                string hou;
                if (showPostTime[i].Length == 12)
                {
                    hou = showPostTime[i].Substring(0, 2);
                }
                else
                {
                    hou = showPostTime[i].Substring(0, 1);
                }
                int x = int.Parse(hou);
                TimeSpan min_hour = new TimeSpan(x, 59, 0);
                TimeSpan max_hour = new TimeSpan(x, 59, 59);
                min = min.Add(min_hour);
                answer = Convert.ToString(min.Hours) + ":" + Convert.ToString(min.Minutes) + ":" + Convert.ToString(min.Seconds);
                min = TimeSpan.Parse(answer);
                max = max.Add(max_hour);
                answer = Convert.ToString(max.Hours) + ":" + Convert.ToString(max.Minutes) + ":" + Convert.ToString(max.Seconds);
                max = TimeSpan.Parse(answer);
            }
            //string hour = "hour";
            for (i=1;i<exactPostTime.Length;i++)
            {
                TimeSpan exact = TimeSpan.Parse(exactPostTime[i]);
                if (showPostTime[i].Contains(sec))
                {
                    TimeSpan min_sec = TimeSpan.Parse("00:00:00");
                    TimeSpan max_sec = TimeSpan.Parse("00:00:59");
                     min_sec = min_sec.Add(exact);
                    answer = Convert.ToString(min_sec.Hours) + ":" + Convert.ToString(min_sec.Minutes) + ":" + Convert.ToString(min_sec.Seconds);
                    min_sec = TimeSpan.Parse(answer);
                    max_sec = max_sec.Add(exact);
                    answer = Convert.ToString(max_sec.Hours) + ":" + Convert.ToString(max_sec.Minutes) + ":" + Convert.ToString(max_sec.Seconds);
                    max_sec = TimeSpan.Parse(answer);
                    if (TimeSpan.Compare(min_sec,max) == 1)
                        return "impossible";
                    if (TimeSpan.Compare(min_sec, min) == 1)
                        min = min_sec;
                    if (TimeSpan.Compare(max_sec, max) == 1)
                        max = max_sec;

                }
                else if(showPostTime[i].Contains(minute))
                {
                    string minut;
                    if (showPostTime[i].Length == 14)
                    {
                         minut = showPostTime[i].Substring(0, 2);
                    }
                    else
                    {
                         minut = showPostTime[i].Substring(0, 1);
                    }
                    int x = int.Parse(minut);
                    TimeSpan min_min = new TimeSpan(0, x, 0);
                    TimeSpan max_min = new TimeSpan(0, x, 59);
                    min_min = min_min.Add(exact);
                    answer = Convert.ToString(min_min.Hours) + ":" + Convert.ToString(min_min.Minutes) + ":" + Convert.ToString(min_min.Seconds);
                    min_min = TimeSpan.Parse(answer);
                    max_min = max_min.Add(exact);
                    answer = Convert.ToString(max_min.Hours) + ":" + Convert.ToString(max_min.Minutes) + ":" + Convert.ToString(max_min.Seconds);
                    max_min = TimeSpan.Parse(answer);
                    if (TimeSpan.Compare(min_min, max) == 1)
                        return "impossible";
                    if (TimeSpan.Compare(min_min, min) == 1)
                        min = min_min;
                    if (TimeSpan.Compare(max_min, max) == 1)
                        max = max_min;

                }
                else
                {
                    string hou;
                    if (showPostTime[i].Length == 12)
                    {
                         hou = showPostTime[i].Substring(0, 2);
                    }
                    else
                    {
                         hou = showPostTime[i].Substring(0, 1);
                    }
                    int x = int.Parse(hou);
                    TimeSpan min_hour = new TimeSpan(x, 0, 0);
                    TimeSpan max_hour = new TimeSpan(x, 59, 59);
                    min_hour = min_hour.Add(exact);
                    answer = Convert.ToString(min_hour.Hours) + ":" + Convert.ToString(min_hour.Minutes) + ":" + Convert.ToString(min_hour.Seconds);
                    min_hour = TimeSpan.Parse(answer);
                    max_hour = max_hour.Add(exact);
                    answer = Convert.ToString(max_hour.Hours) + ":" + Convert.ToString(max_hour.Minutes) + ":" + Convert.ToString(max_hour.Seconds);
                    max_hour = TimeSpan.Parse(answer);
                    
                    if (TimeSpan.Compare(min_hour, max) == 1)
                        return "impossible";
                    if (TimeSpan.Compare(min_hour, min) == 1)
                        min = min_hour;
                    if (TimeSpan.Compare(max_hour, max) == 1)
                        max = max_hour;
                }
                
            }
            string hours,minutes,seconds;
             if (min.Hours>=0 && min.Hours<=9)
            {
                hours = "0" + Convert.ToString(min.Hours);
            }
            else
            {
                 hours = Convert.ToString(min.Hours);
            }
             if (min.Minutes >= 0 && min.Minutes<= 9)
            {
                minutes = "0" + Convert.ToString(min.Minutes);
            }
             else
            {
                minutes = Convert.ToString(min.Minutes);
            }
            if (min.Seconds >= 0 && min.Seconds <= 9)
            {
                seconds = "0" + Convert.ToString(min.Seconds);
            }
            else
            {
                seconds = Convert.ToString(min.Seconds);
            }

            answer = hours + ":" + minutes + ":" + seconds;
            //Console.WriteLine(answer);
            return answer;
            //throw new NotImplementedException();
        }
    }
}
