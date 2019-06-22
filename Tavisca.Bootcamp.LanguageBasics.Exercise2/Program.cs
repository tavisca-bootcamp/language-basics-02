using System;
using System.Linq;
namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
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
            int ans = 0,j=0;
            int[] flag = new int[showPostTime.Length];
            int[] time = new int[showPostTime.Length];
         
            foreach(string x in showPostTime)
            {
           
                ans = 0;
             
                if (x.Contains("minutes"))
                {
                    
                   
                    for (int i = 0; x[i] != ' '; i++)
                    {
                        ans = ans * 10 + x[i] - '0';
                    }
                    flag[j] = 1;
                    time[j] = ans;
                    j++;
                    // Console.WriteLine(ans);

                }
                else if(x.Contains("seconds"))
                {
                    flag[j] = 0;
                    //  Console.WriteLine(ans);
                    time[j] = 59;
                    j++;
                }
                else if(x.Contains("hours"))
                {
                    flag[j] = 2;
                    //Console.WriteLine(ans);
                    for (int i = 0; x[i] != ' '; i++)
                    {
                        ans = ans * 10 + x[i] - '0';
                    }
                     //Console.WriteLine(ans);
                    time[j] = ans;
                    j++;
                }
            }    
            int k = 0;
            int len = 0;
            string[] array = new string[showPostTime.Length];

            foreach(string p in exactPostTime)
            {
                if (exactPostTime.Length == 1)
                    return p;
                DateTime value = DateTime.Parse(p);
                if (k >= j)
                    break;
                    if(flag[k]==0)
                {
                 array[len]= value.AddSeconds(time[k]).ToString("hh:mm:ss");
                 
                    len++;
                }
                    else if(flag[k]==1)
                {
                    //Console.WriteLine(value.ToLongTimeString());
                   array[len]=value.AddMinutes(time[k]).ToLongTimeString();
                    
                    len++;
                }
                    else if(flag[k]==2)
                {
                  array[len]=value.AddHours(time[k]).AddMinutes(59).ToString("hh:mm:ss");
                   
                    len++;
                }
                k++;

            }
            for(int i=0;i<len;i++)
            if(exactPostTime.Length!=exactPostTime.Distinct().Count())
            {
                if (time.Length == time.Distinct().Count())
                    return "impossible";
                else
                {
                    Array.Sort(array, StringComparer.InvariantCulture);
                   // Console.WriteLine(array[0]);
                    return array[0];
                }
           
            }
            else
            {
                Array.Sort(array, StringComparer.InvariantCulture);
                //Console.WriteLine(array[0]);
                return array[0];
            }

            return "impossible" ;
            throw new NotImplementedException();
        }
    }
}
