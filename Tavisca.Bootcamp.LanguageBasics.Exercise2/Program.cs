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
            string ans;
            DateTime[] dateValues = new DateTime[exactPostTime.Length];
            int hour=0, minute=0, second=0;
            bool flag = true;
            DateTime dateTime;
            for (int i = 0; i < exactPostTime.Length; i++)
            {
                if (DateTime.TryParse(exactPostTime[i], out dateValues[i]))
                {
                    string[] words = showPostTime[i].Split(' ');
                    if (flag)
                    {
                        //will be executed only once.
                        hour = dateValues[i].Hour;
                        minute = dateValues[i].Minute;
                        second = dateValues[i].Second;
                        flag = false;
                    }
                    else
                    {
                        //we will check wheather repeated time is given.
                        if (dateValues[i-1].ToString("HH:mm:ss").Equals(dateValues[i].ToString("HH:mm:ss")) && (!(showPostTime[i-1].Split(' ')[0].Equals(words[0])) || !(showPostTime[i-1].Split(' ')[1].Equals(words[1]))))
                        {
                            return "impossible";
                        }
                    }
                    if (words[1].Equals("hours"))
                    {
                        hour = dateValues[i].AddHours(int.Parse(words[0])).Hour;
                    }
                    else if (words[1].Equals("minutes"))
                    {
                        dateTime = dateValues[i].AddMinutes(int.Parse(words[0]));
                        hour = dateTime.Hour;
                        minute = dateTime.Minute;
                        second = dateValues[i].Second;
                    }
                }
                else
                {
                    Console.WriteLine("Unable to convert '{0}' to a date.", exactPostTime[i]);
                }
            }
            string h1 = ""+hour;
            if (hour == 0) h1 = "00";
            ans = h1 + ":" + minute;
            ans += ":" + second;
            return ans;
        }
    }
}
