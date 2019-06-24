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

            for (int i = 0; i < exactPostTime.Length - 1; i++)
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

            DateTime x, ct, min = DateTime.Parse("00:00:00"), max = DateTime.Parse("23:59:59");
            string ShowTime;

            

            for (int i = 0; i < exactPostTime.Length; i++)
            {
                x = DateTime.Parse(exactPostTime[i]);
                ShowTime = showPostTime[i];
                string[] st = ShowTime.Split(' ');

                if (st[1].Equals("seconds"))
                {
                    ct = x;
                    if (DateTime.Compare(DateTime.Parse(ct.ToLongTimeString()), DateTime.Parse(min.ToLongTimeString())) == 1)
                    {
                        min = ct;
                    }
                    if (DateTime.Compare(DateTime.Parse(ct.AddSeconds(59).ToLongTimeString()), DateTime.Parse(max.ToLongTimeString())) == -1)
                    {
                        max = ct.AddSeconds(59);
                    }
                }
                else if (st[1].Equals("minutes"))
                {
                    ct = x.AddMinutes(double.Parse(st[0]));
                    if (DateTime.Compare(DateTime.Parse(ct.ToLongTimeString()), DateTime.Parse(min.ToLongTimeString())) == 1)
                    {
                        min = ct;
                    }
                    if (DateTime.Compare(DateTime.Parse(ct.AddSeconds(59).ToLongTimeString()), DateTime.Parse(max.ToLongTimeString())) == -1)
                    {
                        max = ct.AddSeconds(59);
                    }
                }
                else if (st[1].Equals("hours"))
                {
                    ct = x.AddHours(double.Parse(st[0]));
                    if (DateTime.Compare(DateTime.Parse(ct.ToLongTimeString()), DateTime.Parse(min.ToLongTimeString())) == 1)
                    {
                        min = ct;
                    }
                    if (DateTime.Compare(DateTime.Parse(ct.AddMinutes(59).ToLongTimeString()), DateTime.Parse(max.ToLongTimeString())) == -1)
                    {
                        max = ct.AddMinutes(59);
                    }
                }
            }

            return min.ToLongTimeString();

        }
    }
}
