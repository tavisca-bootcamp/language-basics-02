using System;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class ForumPostEasy
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
            return Method(exactPostTime, showPostTime);
        }

        public static string Method(string[] exactPostTime, string[] showPostTime)
        {

            int Temp = exactPostTime.Length;

            for (int i = 0; i < exactPostTime.Length - 1; i++)
                if (exactPostTime[i] == exactPostTime[i + 1] && showPostTime[i] != showPostTime[i + 1]) return "impossible";

            string[] ReadingTime = new string[Temp];

            for (int i = 0; i < Temp; i++)
            {
                DateTime TimeConverted = Convert.ToDateTime(exactPostTime[i]);                
                if (showPostTime[i].Contains("seconds")) ReadingTime[i] = exactPostTime[i];

                else if (showPostTime[i].Contains("minutes"))
                {                    
                    string minutes = showPostTime[i].Split(' ')[0];                   
                    DateTime current = TimeConverted.AddMinutes(Int32.Parse(minutes));                    
                    string result = current.ToString().Split(' ')[1];                    
                    ReadingTime[i] = result;
                }

                else if (showPostTime[i].Contains("hours"))
                {                    
                    string hours = showPostTime[i].Split(' ')[0];                   
                    DateTime current = TimeConverted.AddHours(Int32.Parse(hours));                  
                    string result = current.ToString().Split(' ')[1];                    
                    ReadingTime[i] = result;
                }
                else return "impossible";
            }
            Array.Sort(ReadingTime);
            return ReadingTime[Temp - 1];
        }

       
    }

}
