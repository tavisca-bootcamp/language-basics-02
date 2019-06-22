using System;
using System.Text.RegularExpressions;

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

        static string findIntersection(int[,] intervals, int N)
        {
            int l = intervals[0, 0];
            int r = intervals[0, 1];
            for (int i = 1; i < N; i++)
            {
                if (intervals[i, 0] > r ||
                    intervals[i, 1] < l)
                {

                    return "impossible";
                }
                else
                {
                    l = Math.Max(l, intervals[i, 0]);
                    r = Math.Min(r, intervals[i, 1]);
                }
            }
            // Console.WriteLine(l + " " + r);
            if (l < r)
                return l.ToString();
            else
                return r.ToString();
        }

        public static int convertInt(TimeSpan y)
        {
            string x = y.ToString();
            if (x[1] == '.')
            {
                String z = x.Substring(2);
                x = z;
            }
            string[] numbers = Regex.Split(x, @"\D+");
            int k = int.Parse(numbers[0]) * 10000 + int.Parse(numbers[1]) * 100 + int.Parse(numbers[2]);
            return k;
        }
        public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {
            // Add your code here.
            int i, l;
            TimeSpan ts, ts1, ts2;
            l = showPostTime.Length;
            int[,] cs = new int[l, 2];
            for (i = 0; i < l; i++)
            {
                ts = TimeSpan.Parse(exactPostTime[i]);

                if (showPostTime[i][0] == 'f')
                {
                    ts1 = ts.Add(new TimeSpan(0, 0, 59));
                    cs[i, 0] = convertInt(ts);
                    cs[i, 1] = convertInt(ts1);
                }
                else if (showPostTime[i].IndexOf('m') != -1)
                {
                    string[] number = Regex.Split(showPostTime[i], @"\D+");
                    int min = int.Parse(number[0]);
                    ts1 = ts.Add(new TimeSpan(0, min, 0));
                    ts2 = ts.Add(new TimeSpan(0, min, 59));
                    cs[i, 0] = convertInt(ts1);
                    cs[i, 1] = convertInt(ts2);
                }
                else
                {
                    string[] number = Regex.Split(showPostTime[i], @"\D+");
                    int hr = int.Parse(number[0]);
                    ts1 = ts.Add(new TimeSpan(hr, 0, 0));
                    ts2 = ts.Add(new TimeSpan(hr, 59, 59));
                    cs[i, 0] = convertInt(ts1);
                    cs[i, 1] = convertInt(ts2);
                }
            }
            string s = findIntersection(cs, l);
            if (s.Equals("impossible"))
                return s;
            else
            {
                if (s.Length < 6)
                {
                    int k, j = 6 - s.Length;
                    char x = '0';
                    for (k = 0; k < i; k++)
                        s = x + s;
                }
                string d;
                string y2, y1, y3;
                y1 = s.Substring(0, 2);
                y2 = s.Substring(2, 2);
                y3 = s.Substring(4, 2);
                d = y1 + ':' + y2 + ':' + y3;
                return d;
            }

            throw new NotImplementedException();
        }
    }
}
