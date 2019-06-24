using System;
using System.Text.RegularExpressions;
using System.Linq;
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
            int i=0;
            int len=exactPostTime.Length;
            String[] arr = new String[len];
            int k=0;
            for (int z = 0; z < exactPostTime.Length; z++)
            {
                for (int j = z + 1; j < exactPostTime.Length; j++)
                {
                    if (exactPostTime[z] == exactPostTime[j])
                    {
                        if (showPostTime[z] != showPostTime[j])
                        {
                            return "impossible";
                        }
                    }
                }
            }
           foreach(string s in exactPostTime)
           {
                int hr = Int32.Parse(s.Substring(0,2));
                int min =Int32.Parse(s.Substring(3,2));
                int sec = Int32.Parse(s.Substring(6,2));
                DateTime d = new DateTime(2019, 1, 1, hr, min, sec);
               if(showPostTime[i].Contains("seconds"))
               {
                    arr[i]=s;
               }
               else if(showPostTime[i].Contains("minutes"))
                {
                   int temp = Int32.Parse(showPostTime[i].Substring(0,2));
                   d=d.AddMinutes(temp);
                   arr[i]=d.ToLongTimeString();
                   
                }
                else if(showPostTime[i].Contains("hours"))
                {
                   int t = Int32.Parse(showPostTime[i].Substring(0,2));
                   d=d.AddHours(t);
                   arr[i]=d.ToLongTimeString();
                }
               i++;
               k++;
           }
           Array.Sort(arr);
            String res = arr[len-1].Split(" ")[0];
            //Console.WriteLine(res);
           return res;
            throw new NotImplementedException();
        }
    }
}
