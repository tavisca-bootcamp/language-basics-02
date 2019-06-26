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
            int n = exactPostTime.Length;
 
            string[] arr = new string[n];  //array to contain total current times.
 
            for (int i = 0; i < n; i++){
                 //converting post time to a DateTime object
                DateTime time = DateTime.ParseExact(exactPostTime[i], "HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
 
                if (showPostTime[i].Contains("seconds"))  //current time for cases where string is "a few seconds ago"
                {
                    arr[i] = exactPostTime[i];
                }
 
                else if (showPostTime[i].Contains("minutes"))
                {
                    string minutes = showPostTime[i].Split(' ')[0];
                    DateTime current = time.AddMinutes(Int32.Parse(minutes)); //adding minutes to get probable current time
                    string result = current.ToString("HH:mm:ss");
 
                    arr[i] = result;
                }
 
                else if (showPostTime[i].Contains("hours"))
                {
                    string hours = showPostTime[i].Split(' ')[0];
                    DateTime current = time.AddHours(Int32.Parse(hours));// adding hours to get probable current time
                    string result = current.ToString("HH:mm:ss");
 
                    arr[i] = result;
                }
 
 
            }
 
            for (int i = 0; i <n; i++) //checking for cases where same post time gives different human readable strings.
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (exactPostTime[i] == exactPostTime[j])
                        if (showPostTime[i] != showPostTime[j])
                            return "impossible";
                }
            }
 
            Array.Sort(arr);
            return arr[postNumber - 1]; //printing the time which comes lexicographically first
 
        }
    }
}
