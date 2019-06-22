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
                    {
                        if (showPostTime[i] != showPostTime[j])
                        {
                            return "impossible";
                        }
                    }
                }
            }
            String[] ans = new String[exactPostTime.Length];

            for (int i = 0; i < exactPostTime.Length; i++)
            {
                String[] temp = exactPostTime[i].Split(":");
                DateTime d = new DateTime(2019, 1, 1, Int32.Parse(temp[0]), Int32.Parse(temp[1]), Int32.Parse(temp[2]));
                var hr = temp[0];
                var min = temp[1];
                var sec = temp[2];
                if (showPostTime[i].Contains("seconds"))
                {
                    ans[i] = exactPostTime[i];
                }
                else if (showPostTime[i].Contains("minutes"))
                {
                    String minutes = showPostTime[i].Split(" ")[0];
                    int m = Int32.Parse(minutes);
                    d = d.AddMinutes(m);
                    ans[i] = d.ToLongTimeString();

                }
                else if (showPostTime[i].Contains("hours"))
                {
                    String hour = showPostTime[i].Split(" ")[0];
                    int h = Int32.Parse(hour);
                    d = d.AddHours(h);
                    ans[i] = d.ToLongTimeString();
                }

            }
            Array.Sort(ans);
            return ans[exactPostTime.Length - 1];
            throw new NotImplementedException();
        }
    }
}
