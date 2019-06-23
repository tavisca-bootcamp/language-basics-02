using System;
using System.Collections;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    class Program
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


        private static short ExtractLeadingNumber(string message)
        {
            return short.Parse(message.Substring(0, message.IndexOf(' ')));
        }

        public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {
            DateTime[] timeInstances = new DateTime[exactPostTime.Length * 2];
            short itemCount = 0;

            for (short i = 0; i < exactPostTime.Length; i++)
            {
                DateTime postTime = DateTime.Parse(exactPostTime[i]);
                string message = showPostTime[i];

                if (i != 0)
                {
                    if (exactPostTime[i] == exactPostTime[i - 1])
                        if (message != showPostTime[i - 1])
                            return "impossible";
                }

                if (message.Contains("seconds"))
                {
                    timeInstances[itemCount++] = postTime.AddSeconds(0);
                    timeInstances[itemCount++] = postTime.AddSeconds(59);
                }
                else if (message.Contains("minutes"))
                {
                    short minutes = ExtractLeadingNumber(message);
                    timeInstances[itemCount++] = postTime.AddMinutes(minutes);
                    timeInstances[itemCount++] = postTime.AddMinutes(minutes).AddSeconds(59);
                }
                else if (message.Contains("hours"))
                {
                    short hours = ExtractLeadingNumber(message);
                    timeInstances[itemCount++] = postTime.AddHours(hours);
                    timeInstances[itemCount++] = postTime.AddMinutes(59).AddSeconds(59);
                }
            }

            string[] timeStrings = new string[itemCount];
            for (short i = 0; i < itemCount; i++)
                timeStrings[i] = timeInstances[i].ToString("HH:mm:ss");
            Array.Sort(timeStrings);
            return timeStrings[itemCount/2 - 1];
        }
    }
}
