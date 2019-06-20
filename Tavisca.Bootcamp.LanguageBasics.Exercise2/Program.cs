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
            string answer = "00:00:00";
            string[] interval = new string[exactPostTime.Length];
            for (int i = 0; i < exactPostTime.Length; i++)
            {
                interval[i] = GetInterval(exactPostTime[i], showPostTime[i]);
            }

            if (exactPostTime.Length == 1)
                return interval[0];
            else
            {
                for (int i = 0; i < exactPostTime.Length; i++)
                {
                    for (int j = i + 1; j < exactPostTime.Length; j++)
                    {
                        if (exactPostTime[i] == exactPostTime[j])
                        {
                            if (showPostTime[i] != showPostTime[j])
                                return "impossible";
                        }
                    }
                    if(Convert.ToDateTime(answer) < Convert.ToDateTime(interval[i]))
                    answer = interval[i];
                }
            }
            return answer;
        }

        public static string GetInterval(string a, string b)
        {
            string[] temp = BreakStringbySpace(b);
            if (b.Contains("seconds"))
            {
                return a;
            }
            else if (b.Contains("minutes"))
            {
                return AddTime(a, 'm', Int32.Parse(temp[0]));
            }
            else
            {
                return AddTime(a, 'h', Int32.Parse(temp[0]));
            }
        }
        public static string[] BreakStringbyInterval(string a)
        {
            return a.Split(',');
        }
        public static string[] BreakStringbySpace(string a)
        {
            return a.Split(' ');
        }

        public static string AddTime(string a, char t, int x = -1)
        {
            DateTime d1 = Convert.ToDateTime(a);
            if (t == 's')
                return (d1.AddSeconds(59)).ToString("HH:mm:ss");
            else if (t == 'm')
            {
                return (d1.AddMinutes(x)).ToString("HH:mm:ss");
            }
            else if (t == 'h')
            {
                return (d1.AddHours(x)).ToString("HH:mm:ss");
            }
            else if (t == 'M')
            {
                d1 = d1.AddSeconds(59);
                return (d1.AddMinutes(x)).ToString("HH:mm:ss");
            }
            else
            {
                d1 = d1.AddSeconds(59);
                d1 = d1.AddMinutes(59);
                return (d1.AddHours(x)).ToString("HH:mm:ss");
            }
        }
    }
}
