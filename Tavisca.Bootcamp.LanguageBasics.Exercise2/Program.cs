using System;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class ForumPostEasy
    {
        private static DateTime min, max;

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
            DateTime postTime, currentTime; 
            string showTime;
            bool isImpossible;
            min = DateTime.Parse("00:00:00");
            max = DateTime.Parse("23:59:59");

            isImpossible = CheckForImpossible(exactPostTime, showPostTime);

            if (isImpossible)
            {
                return "impossible";
            }
            else
            {
                for (int i = 0; i < exactPostTime.Length; i++)
                {
                    postTime = DateTime.Parse(exactPostTime[i]);
                    currentTime = postTime;
                    showTime = showPostTime[i];

                    if (showTime.Contains("seconds"))
                    {
                        ForSecondsAndMinutes(currentTime);
                    }
                    else if (showTime.Contains("minutes"))
                    {
                        currentTime = postTime.AddMinutes(double.Parse(showTime.Split(' ')[0]));
                        ForSecondsAndMinutes(currentTime);
                    }
                    else if (showTime.Contains("hours"))
                    {
                        currentTime = postTime.AddHours(double.Parse(showTime.Split(' ')[0]));
                        ForHours(currentTime);
                    }
                }
            }
            return min.ToLongTimeString();
        }

        private static bool CheckForImpossible(string[] exactPostTime, string[] showPostTime)
        {
            for (int i = 0; i < exactPostTime.Length - 1; i++)
            {
                for (int j = i + 1; j < exactPostTime.Length; j++)
                {
                    if (exactPostTime[i] == exactPostTime[j])
                    {
                        if (showPostTime[i] != showPostTime[j])
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private static void ForSecondsAndMinutes(DateTime currentTime)
        {
            if (DateTime.Compare(DateTime.Parse(currentTime.ToLongTimeString()), DateTime.Parse(min.ToLongTimeString())) == 1)
            {
                min = currentTime;
            }
            if (DateTime.Compare(DateTime.Parse(currentTime.AddSeconds(59).ToLongTimeString()), DateTime.Parse(max.ToLongTimeString())) == -1)
            {
                max = currentTime.AddSeconds(59);
            }
        }

        private static void ForHours(DateTime currentTime)
        {
            if (DateTime.Compare(DateTime.Parse(currentTime.ToLongTimeString()), DateTime.Parse(min.ToLongTimeString())) == 1)
            {
                min = currentTime;
            }
            if (DateTime.Compare(DateTime.Parse(currentTime.AddMinutes(59).ToLongTimeString()), DateTime.Parse(max.ToLongTimeString())) == -1)
            {
                max = currentTime.AddMinutes(59);
            }
        }

    }
}
