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
            //Console.ReadKey(true);
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
            DateTime min_t,max_t;
            int i;
            DateTime[] exactPostTime_d=new DateTime[exactPostTime.Length];
            for(i=0;i<exactPostTime.Length;i++)
            {
              exactPostTime_d[i]=DateTime.ParseExact(exactPostTime[i],"HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture);
            }
            min_t=findMinTime(exactPostTime_d[0],showPostTime[0]);
            max_t=findMaxTime(exactPostTime_d[0],showPostTime[0]);
            for(i=1;i<exactPostTime.Length;i++)
            {
                DateTime min_temp,max_temp;
                min_temp=findMinTime(exactPostTime_d[i],showPostTime[i]);
                max_temp=findMaxTime(exactPostTime_d[i],showPostTime[i]);
        
                if(max_t<min_temp && min_t<min_temp)
                {
                    return "impossible";
                }
                else if(min_t>max_temp)
                {
                    return "impossible";
                }
                else if(min_t<=min_temp && min_temp<=max_t)
                {
                    if(min_temp>min_t)
                        min_t=min_temp;
                    if(max_temp<max_t)
                        max_t=max_temp;
                }
                else if(min_temp<=min_t && min_t<=max_temp)
                {
                    if(max_temp<max_t)
                        max_t=max_temp;
                    if(min_temp>min_t)
                        min_t=min_temp;
                }

            }
            
            return min_t.ToString("HH:mm:ss",System.Globalization.CultureInfo.InvariantCulture);
        }
        public static DateTime findMinTime(DateTime exactPostTime,string showPostTime)
        {
            if(showPostTime[0]=='f')
            {
                return exactPostTime;
            }
            else if(showPostTime[2]=='m' || showPostTime[3]=='m')
            {
                int t = Convert.ToInt32(showPostTime.Substring(0,2));
                exactPostTime=exactPostTime.AddMinutes(t);
                return exactPostTime;
            }
            else
            {
                int t = Convert.ToInt32(showPostTime.Substring(0,2));
                int day = exactPostTime.Day;
                exactPostTime=exactPostTime.AddHours(t);
                if(exactPostTime.Day!=day)
                    exactPostTime=exactPostTime.AddDays(-1);
                return exactPostTime;
            }
        }
        public static DateTime findMaxTime(DateTime exactPostTime,string showPostTime)
        {
            if(showPostTime[0]=='f')
            {
                exactPostTime=exactPostTime.AddSeconds(59);
                return exactPostTime;
            }
            else if(showPostTime[2]=='m' || showPostTime[3]=='m')
            {
                int t = Convert.ToInt32(showPostTime.Substring(0,2));
                exactPostTime=exactPostTime.AddMinutes(t);
                exactPostTime=exactPostTime.AddSeconds(59);
                return exactPostTime;
            }
            else
            {
                int t = Convert.ToInt32(showPostTime.Substring(0,2));
                int day = exactPostTime.Day;
                exactPostTime=exactPostTime.AddHours(t);
                exactPostTime=exactPostTime.AddMinutes(59);
                exactPostTime=exactPostTime.AddSeconds(59);
                if(exactPostTime.Day!=day)
                    exactPostTime=exactPostTime.AddDays(-1);
                return exactPostTime;
            }
        }
    }
}
