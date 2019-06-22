using System;
using System.Collections;

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
           int len=exactPostTime.Length;
           int i=0;
           Hashtable h=new Hashtable();
           TimeSpan currentTime=new TimeSpan(00,00,00);
            while(i<len)
            {
             if(!h.ContainsKey(exactPostTime[i]))
                h.Add(exactPostTime[i],showPostTime[i]);

             if(h.ContainsKey(exactPostTime[i])&&h[exactPostTime[i]].ToString()!=showPostTime[i])
                return "impossible";

             string[] sub_showTime=showPostTime[i].Split(" ");
             TimeSpan cTime=TimeSpan.Parse(exactPostTime[i]);
             
             if(sub_showTime[1]=="minutes")
             {
                cTime=cTime.Add(TimeSpan.FromMinutes(Double.Parse(sub_showTime[0])));
                cTime =new TimeSpan(cTime.Hours,cTime.Minutes,cTime.Seconds);
             }
             else if(sub_showTime[1]=="hours")
             {
                cTime=cTime.Add(TimeSpan.FromHours(Double.Parse(sub_showTime[0])));
                cTime =new TimeSpan(cTime.Hours,cTime.Minutes,cTime.Seconds);
             }

             if(currentTime<cTime)
                currentTime=cTime;

            i++;
           }
           return currentTime.ToString();
        }
    }
}
