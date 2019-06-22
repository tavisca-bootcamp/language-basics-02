using System;
using System.Collections.Generic;

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
            // Add your code here.
            Dictionary<string, string> hash = new Dictionary<string ,string>();
            for(int i =0;i<exactPostTime.Length;i++)
            {
                if(hash.ContainsKey(exactPostTime[i]))
                {
                    if( hash[exactPostTime[i]]!=showPostTime[i])
                        return "impossible";
                }
                else
                    hash.Add(exactPostTime[i],showPostTime[i]);
            }

            List<string> times = new List<string>();
            for(int i=0;i<exactPostTime.Length;i++)
            {
                string []time = exactPostTime[i].Split(':');
                //Console.WriteLine(time[0]+" "+time[1]+" "+time[2]);
                TimeSpan post_time=new TimeSpan(Convert.ToInt32(time[0]),Convert.ToInt32(time[1]),Convert.ToInt32(time[2]));
                TimeSpan time_diff=time_message(showPostTime[i]);

                TimeSpan current=post_time+time_diff;
                //string total = 
                times.Add(Convert.ToString(current));
            }
            return lexo(times);
        }

        private static TimeSpan time_message(string x)
        {
            string [] check = x.Split(' ');
            if(check[0]=="few")
                return new TimeSpan(0,0,0);
            else if(check[1]=="minutes")
                return new TimeSpan(0,Convert.ToInt32(check[0]),0);
            else
                return new TimeSpan(Convert.ToInt32(check[0]),0,0);
        }

        public static string lexo(List<string> times)
        {
            int max=Int32.MinValue;
            int pos=Int32.MinValue;
            for(int i=0;i<times.Count;i++)
            {
                //Console.WriteLine(times[i]);
                int index=times[i].IndexOf('.');
                if(index>=0)
                    times[i]=times[i].Substring(index+1);
                string [] num=times[i].Split(":");
               // int index=num[0].IndexOf('.');
                
                //Console.WriteLine(num[0]);
                int total=Convert.ToInt32(num[0])*60*60+Convert.ToInt32(num[1])*60+Convert.ToInt32(num[2]);
                if(total>max)
                {
                    max=total;
                    pos=i;
                }
            }
            return times[pos];
        }
        //throw new NotImplementedException();
    }
}
