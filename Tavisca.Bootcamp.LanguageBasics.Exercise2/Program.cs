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
            for (int i = 0; i < exactPostTime.Length; i++)
            {
                for (int j = i + 1; j < exactPostTime.Length; j++)
                {
                    if (exactPostTime[i] == exactPostTime[j] && showPostTime[i] != showPostTime[j])
                    {
                            return "impossible";
                    }
                }
            }
            string[] CurrentArray = new string[exactPostTime.Length];
            for (int i = 0; i < exactPostTime.Length; i++)
            {
                DateTime time = Convert.ToDateTime(exactPostTime[i]);               
                if (showPostTime[i].Contains("seconds"))
                {
                    CurrentArray[i] = exactPostTime[i];
                }
                else if (showPostTime[i].Contains("minutes"))
                {
                    string Minutes = showPostTime[i].Split(' ')[0];
                    DateTime Current = time.AddMinutes(Int32.Parse(Minutes)); 
                    string Result =  Current.ToString().Split(' ')[1];
                    CurrentArray[i] = Result;
                }
                else if (showPostTime[i].Contains("hours"))
                {
                    string Hours = showPostTime[i].Split(' ')[0];
                    DateTime Current = time.AddHours(Int32.Parse(Hours)); 
                    string Result = Current.ToString().Split(' ')[1];
                    CurrentArray[i] = Result;
                }
                else
                {
                    return "impossible";
                }
            }
            Array.Sort(CurrentArray);//Ascending order
            return CurrentArray[exactPostTime.Length - 1];
        }
    }
}
