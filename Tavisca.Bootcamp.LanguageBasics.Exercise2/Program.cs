using System;

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
            int l = exactPostTime.Length;
            string[][] rangeOfTime = new string[l][];
            for (int i = 0; i < l; i++)
            {
                string showPT = showPostTime[i];
                int start = 0, stop = 0;
                if (showPT.Contains("seconds"))
                {
                    start = 0;
                    stop = 60;
                }
                else if (showPT.Contains("minutes"))
                {
                    int min = Convert.ToInt32(showPT.Substring(0, showPT.IndexOf('m') - 1));
                    start = min * 60;
                    stop = start + 60;
                }
                else if (showPT.Contains("hours"))
                {
                    int hr = Convert.ToInt32(showPT.Substring(0, showPT.IndexOf('h') - 1));
                    start = hr * 60 * 60;
                    stop = start + (60 * 60);
                }
                string[] rot = new string[stop - start];
                DateTime dt = DateTime.Parse(exactPostTime[i]);
                int k = 0;
                for (int j = start; j < stop; j++)
                {
                    rot[k++] = dt.AddSeconds(j).ToString("HH:mm:ss");
                }
                rangeOfTime[i] = rot;
            }
            int cnt = 0;
            for (int i = 0; i < rangeOfTime[0].Length; i++)
            {
                cnt = 0;
                for (int j = 0; j < l; j++)
                {
                    for (int k = 0; k < rangeOfTime[j].Length; k++)
                    {
                        if (rangeOfTime[0][i].Equals(rangeOfTime[j][k]))
                        {
                            cnt++;
                            break;
                        }
                    }
                    if (cnt != j + 1)
                    {
                        Console.WriteLine(cnt);
                        break;
                    }
                }
                if (cnt == l)
                {
                    return rangeOfTime[0][i];
                }
            }
            return "impossible";
        }
    }
}