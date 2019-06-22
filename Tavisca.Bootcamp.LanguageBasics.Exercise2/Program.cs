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
            // Add your code here.
            
            for (int i = 0; i < exactPostTime.Length; i++)
            {
                for (int j = i + 1; j < exactPostTime.Length; j++)
                {
                    if (exactPostTime[i] == exactPostTime[j])
                        if (showPostTime[i] != showPostTime[j])
                            return "impossible";
                }
            }

            
            string[] s = new string[exactPostTime.Length];

            
            for (int i = 0; i < exactPostTime.Length; i++)
            {

                
                string[] split = exactPostTime[i].Split(":");
                DateTime dateTime = new DateTime(2000, 1, 01, Convert.ToInt32(split[0]), Convert.ToInt32(split[1]), Convert.ToInt32(split[2]));

                                
                if (showPostTime[i].Contains("seconds"))
                {
                    s[i] = exactPostTime[i];
                }

                else if (showPostTime[i].Contains("minutes"))
                {

                    string min = showPostTime[i].Split(" ")[0];                
                    TimeSpan time = new TimeSpan(0, Convert.ToInt32(min), 0);  
                    s[i] = dateTime.Add(time).ToString().Split(" ")[1];  

                }
                else if (showPostTime[i].Contains("hours"))
                {

                    string hours = showPostTime[i].Split(" ")[0];
                    TimeSpan timeSpan = new TimeSpan(Convert.ToInt32(hours), 0, 0);
                    s[i] = dateTime.Add(timeSpan).ToString().Split(" ")[1];

                }
            }

            Array.Sort(s);
            return s[(exactPostTime.Length - 1)];



            throw new NotImplementedException();
        }
    }
}
