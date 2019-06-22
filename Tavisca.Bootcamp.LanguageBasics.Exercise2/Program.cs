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
            int len = exactPostTime.Length;
            string[] arr = new string[len];
            for(int i=0;i<len-1;i++){
                if(exactPostTime[i]==exactPostTime[i+1] && showPostTime[i]!=showPostTime[i+1]){
                    return "impossible";
                }
            }

           
            for(int i=0;i<len;i++){

                 TimeSpan timing = TimeSpan.Parse(exactPostTime[i]);

                if(showPostTime[i].Contains("seconds")){
                    TimeSpan cur = new TimeSpan(timing.Hours,timing.Minutes,timing.Seconds);
                    arr[i]=Convert.ToString(cur);
                     //Console.Write("inside seconds \n");
                }

                else if(showPostTime[i].Contains("minutes")){
                    string mint = showPostTime[i].Split(" ")[0];
                    timing = timing.Add(TimeSpan.FromMinutes(Double.Parse(mint)));
                    TimeSpan cur = new TimeSpan(timing.Hours,timing.Minutes,timing.Seconds);
                    arr[i]=Convert.ToString(cur);
                }

                else if(showPostTime[i].Contains("hours")){
                    string hr = showPostTime[i].Split(" ")[0];
                    timing = timing.Add(TimeSpan.FromHours(Double.Parse(hr)));
                    TimeSpan cur = new TimeSpan(timing.Hours,timing.Minutes,timing.Seconds);
                    arr[i]=Convert.ToString(cur);
                }
                else{
                    return "impossible";
                }

                //Console.Write(arr[i]+" ");

            }

            Array.Sort(arr);
            return Convert.ToString(arr[arr.Length-1]);

            throw new NotImplementedException();
        }
    }
}
