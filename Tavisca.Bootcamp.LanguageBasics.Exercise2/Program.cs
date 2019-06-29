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
            int i, j, len = exactPostTime.Length;
            int [ , ]time = new int[len, 3];
            string []calculatedTimes = new string[len];
            string hh, mm, ss;
            for(i = 0; i < len; i++)
            {
                time[i, 0] = Int32.Parse(exactPostTime[i].Substring(0,2));
                time[i, 1] = Int32.Parse(exactPostTime[i].Substring(3,2));
                time[i, 2] = Int32.Parse(exactPostTime[i].Substring(6,2));
            }
            
            for(i = 0; i < showPostTime.Length; i++)
            {
                string []s = showPostTime[i].Split(' ');
                //Console.WriteLine(s.Length+" "+showPostTime[i]);
                if(s[1].Equals("minutes"))
                {
                    time[i, 1] += Int32.Parse(s[0]);
                    if(time[i, 1] >= 60)
                    {
                        time[i, 1] %= 60;
                        time[i, 0]++;
                        if(time[i, 0] >= 24)
                        {
                            time[i, 0] %= 24;
                        }
                    }
                }
                else if(s[1].Equals("hours"))
                {
                    time[i, 0] += Int32.Parse(s[0]);
                    if(time[i, 0] >= 24)
                    {
                        time[i, 0] %= 24;
                    }
                }
                hh = $"{time[i, 0]}".PadLeft(2, '0');
                mm = $"{time[i, 1]}".PadLeft(2, '0');
                ss = $"{time[i, 2]}".PadLeft(2, '0');
                calculatedTimes[i] = $"{hh}:{mm}:{ss}";
                //Console.WriteLine(calculatedTimes[i]);
                //System.Console.WriteLine($"{time[i, 0]}:{time[i, 1]}:{time[i, 2]}");
            }
            for(i = 0; i < len; i++)
            {
                for(j = i + 1; j < len; j++)
                {
                    if(exactPostTime[i].Equals(exactPostTime[j]))
                    {
                        if(!showPostTime[i].Equals(showPostTime[j]))
                        {
                            return "impossible";
                        }
                    }
                }
            }
            Array.Sort(calculatedTimes);
            // for(i = 0; i < len; i++)
            // {
            //     Console.WriteLine(calculatedTimes[i]);
            // }
            return calculatedTimes[len -1];
        }
    }
}
