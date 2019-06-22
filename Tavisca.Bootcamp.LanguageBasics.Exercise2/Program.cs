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
            int n = exactPostTime.Length;
            int[,] a = new int[n, 6];
            int h = 0, m = 0, s = 0, min = 0, hour = 0;
            for (int i = 0; i < n; i++)
            {
                h = Int32.Parse(exactPostTime[i].Substring(0, 2));
                m = Int32.Parse(exactPostTime[i].Substring(3, 2));
                s = Int32.Parse(exactPostTime[i].Substring(6, 2));
                string k = showPostTime[i];
                if (k[0] == 'f')
                {
                    a[i, 0] = h;
                    a[i, 1] = m;
                    a[i, 2] = s;
                    if (s == 0)
                        s = 59;
                    else
                    {
                        s = s - 1;
                        if (m < 59)
                            m = m + 1;
                        else
                        {
                            m = 0;
                            h = (h + 1) % 24;
                        }



                    }
                    a[i, 3] = h;
                    a[i, 4] = m;
                    a[i, 5] = s;
                }
                else if (k[2] == 'm' || k[3] == 'm')
                {
                    if (k[1] == ' ')
                        min = Int32.Parse(k.Substring(0, 1));
                    else
                        min = Int32.Parse(k.Substring(0, 2));
                    m = m + min;
                    if (m <= 59)
                    {
                        a[i, 0] = h;
                        a[i, 1] = m;
                        a[i, 2] = s;

                    }
                    else
                    {
                        m = m % 60;
                        h = (h + 1) % 24;
                        a[i, 0] = h;
                        a[i, 1] = m;
                        a[i, 2] = s;
                    }
                    if (s == 0)
                        s = 59;
                    else
                    {
                        s = s - 1;
                        if (m < 59)
                            m = m + 1;
                        else
                        {
                            m = 0;
                            h = (h + 1) % 24;
                        }



                    }
                    a[i, 3] = h;
                    a[i, 4] = m;
                    a[i, 5] = s;



                }
                else if (k[2] == 'h' || k[3] == 'h')
                {
                    if (k[1] == ' ')
                        hour = Int32.Parse(k.Substring(0, 1));
                    else
                        hour = Int32.Parse(k.Substring(0, 2));

                    h = (h + hour) % 24;
                    a[i, 0] = h;
                    a[i, 1] = m;
                    a[i, 2] = s;

                    if (s == 0)
                        s = 59;
                    else
                    {
                        s = s - 1;
                        if (m < 59)
                            m = m + 1;
                        else
                        {
                            m = 0;
                            h = (h + 1) % 24;
                        }



                    }
                    if (m == 0)
                        m = 59;
                    else
                    {
                        m = m - 1;
                        h = (h + 1) % 24;
                    }
                    a[i, 3] = h;
                    a[i, 4] = m;
                    a[i, 5] = s;
                }
            }


            int[] result = new int[2];
            int[] temp = new int[2];
            result[0] = a[0, 0] * 10000 + a[0, 1] * 100 + a[0, 2];
            result[1] = a[0, 3] * 10000 + a[0, 4] * 100 + a[0, 5];
            for (int i = 1; i < n; i++)
            {
                temp[0] = a[i, 0] * 10000 + a[i, 1] * 100 + a[i, 2];
                temp[1] = a[i, 3] * 10000 + a[i, 4] * 100 + a[i, 5];
                if (temp[0] >= result[0] && temp[0] < result[1])
                {
                    result[0] = temp[0];
                    result[1] = Math.Min(temp[1], result[1]);
                }
                else if (result[0] >= temp[0] && result[0] < temp[1])
                {
                    result[1] = Math.Min(temp[1], result[1]);
                }
                else
                    return "impossible";
            }
            string res = null;
            if (result[0] > result[1])
                return "00:00:00";
            else
            {
                string x = result[0].ToString();
                switch (x.Length)
                {
                    case 0: return "00:00:00";
                    case 1: return String.Concat("00:00:0", x);
                    case 2: return String.Concat("00:00:", x);
                    case 3: return String.Concat("00:0", x.Substring(0, 1), ":", x.Substring(1, 2));
                    case 4: return String.Concat("00:", x.Substring(0, 2), ":", x.Substring(2, 2));
                    case 5: return String.Concat("0", x.Substring(0, 1), ":", x.Substring(1, 2), ":", x.Substring(3, 2));
                }

                res = String.Concat(x.Substring(0, 2), ":", x.Substring(2, 2), ":", x.Substring(4, 2));

            }
            return res;

            //throw new NotImplementedException();
        }
    }
}
