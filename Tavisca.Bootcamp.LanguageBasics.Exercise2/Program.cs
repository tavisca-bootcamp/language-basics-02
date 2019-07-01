using System;
using System.Globalization;
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
            string[] currentTime=new string[exactPostTime.Length];
            DateTime dateTime;
            for(int i = 0; i < exactPostTime.Length; i++)
            {
                // Checking of conflict in Time
                for(int j = i+1; j < exactPostTime.Length; j++)
                {
                    if((exactPostTime[i] == exactPostTime[j]) && (showPostTime[i] != showPostTime[j]))
                    {
                        return "impossible";
                    }
                }
                if( showPostTime[i].Contains("seconds") )
                {
                    currentTime[i] = exactPostTime[i];
                }
                else if( showPostTime[i].Contains("minutes") )
                {
                    bool isTimeForamt = DateTime.TryParse(exactPostTime[i], out dateTime); 
                    string minutes = showPostTime[i].Split(" ")[0]; //taking that x minute
                    //adding the minutes to currentTime ans also covertingto a valid string
                    currentTime[i]=dateTime.AddMinutes(Int32.Parse(minutes)).ToString("HH:mm:ss",CultureInfo.InvariantCulture);
                }
                else if(showPostTime[i].Contains("hours"))
                {
                    bool isTimeForamt = DateTime.TryParse(exactPostTime[i], out dateTime);
                    string hour = showPostTime[i].Split(" ")[0]; //taking that x hour
                    currentTime[i]=dateTime.AddHours(Int32.Parse(hour)).ToString("HH:mm:ss",CultureInfo.InvariantCulture);
                }
                
            }
            Array.Sort(currentTime);
            return currentTime[(exactPostTime.Length -1)]; //returning the subset time
        }
    }
}
