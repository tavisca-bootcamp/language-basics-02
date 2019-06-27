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
           int length=exactPostTime.Length;
           int i=0;
           Hashtable hashTable=new Hashtable();//hashtable to check for consistent showPostTime for two same exactPostTime
           TimeSpan currentTime=new TimeSpan(00,00,00);
            while(i<length)
            {
               if(!hashTable.ContainsKey(exactPostTime[i]))//add exactPostTime and ShowPostTime Pair into hashtable
                  hashTable.Add(exactPostTime[i],showPostTime[i]);

               //check if hashtable already contains exactPostTime and corresponding showPostTime is same as any other ShowPostTime
               if(hashTable.ContainsKey(exactPostTime[i])&&hashTable[exactPostTime[i]].ToString()!=showPostTime[i])
                  return "impossible";

               string[] splittedShowTime=showPostTime[i].Split(" ");//splittedShowTime[0] conatins the time as string and splittedShowTime[1] contains "minutes" or "hours" or "second"
               TimeSpan calcualtedTime=TimeSpan.Parse(exactPostTime[i]);
               
               if(splittedShowTime[1]=="minutes")
               {
                  calcualtedTime=calcualtedTime.Add(TimeSpan.FromMinutes(Double.Parse(splittedShowTime[0])));//add minutes of ShowPostTime to calculating Time
                  calcualtedTime =new TimeSpan(calcualtedTime.Hours,calcualtedTime.Minutes,calcualtedTime.Seconds);
               }
               else if(splittedShowTime[1]=="hours")
               {
                  calcualtedTime=calcualtedTime.Add(TimeSpan.FromHours(Double.Parse(splittedShowTime[0])));//add hours of ShowPostTime to calculating Time
                  calcualtedTime =new TimeSpan(calcualtedTime.Hours,calcualtedTime.Minutes,calcualtedTime.Seconds);
               }

               if(currentTime<calcualtedTime)// for first lexicographic Current Time. 
                  currentTime=calcualtedTime;

               i++;
           }
           return currentTime.ToString();
        }
    }
}
