using System;
using System.Linq;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        class Time
        {
            public int H, M, S;
            public Time(int h, int m, int s)
            {
                H = h;
                M = m;
                S = s;
            }
        }
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
            int h, m, s;
            Time[] t = new Time[exactPostTime.Length];  //array of object of time
            int[] ti = new int[exactPostTime.Length];   //temporary array used for add time
            int[] min = new int[exactPostTime.Length];  //contains min value of time
            int[] max = new int[exactPostTime.Length];  //contains max value of time

            //store time in object of class time
            for (int i = 0; i < exactPostTime.Length; i++)
            {
                int[] a = ParseString(exactPostTime[i]);
                h = a[0]; m = a[1]; s = a[2];
                t[i] = new Time(h, m, s);
                ti[i] = h * 10000 + m * 100 + s;
                //Console.WriteLine(h + " :" + m + " " + s + "\n");
                //Console.WriteLine(ti[i]+"\n");
            }

            //process tofind min and max time
            int hh, mm, ss;
            for (int i = 0; i < showPostTime.Length; i++)
            {
                if (showPostTime[i] == "few seconds ago")
                {
                    min[i] = addTime(0, 0, 0, t[i].H, t[i].M, t[i].S);
                    max[i] = addTime(0, 0, 59, t[i].H, t[i].M, t[i].S);
                }
                else if (showPostTime[i][3] == 'm' || showPostTime[i][2] == 'm')
                {
                    int x = 0, j = 0;
                    while (showPostTime[i][j] != ' ')
                    {
                        x = x * 10 + showPostTime[i][j] - '0';
                        j++;
                    }

                    min[i] = addTime(0, x, 0, t[i].H, t[i].M, t[i].S);
                    max[i] = addTime(0, x, 59, t[i].H, t[i].M, t[i].S);
                }
                else if (showPostTime[i][3] == 'h' || showPostTime[i][2] == 'h')
                {
                    int x = 0, j = 0;
                    while (showPostTime[i][j] != ' ')
                    {
                        x = x * 10 + showPostTime[i][j] - '0';
                        j++;
                    }
                    min[i] = addTime(x, 0, 0, t[i].H, t[i].M, t[i].S);
                    max[i] = addTime(x, 59, 59, t[i].H, t[i].M, t[i].S);
                }
            }

            //for impossible case
            int p = min.Max(), q = max.Min();
            if (p == 0 && q == 0)
                return "00:00:00";
            for (int i = 0; i < min.Length; i++)
            {
                if (p > max[i] || q < min[i])
                    return "impossible";
            }

            //finding lexographically smallest time
            int temp = Math.Min(min[0], max[0]);
            for (int i = 1; i < min.Length; i++)
            {
                if (temp >= min[i] && temp <= max[i] || temp <= min[i] && temp >= max[i])
                    continue;
                else
                    temp = Math.Min(min[i], max[i]);
                //Console.WriteLine(temp);
            }

            /*for(int i = 0; i < showPostTime.Length; i++)
            {
                Console.WriteLine(ti[i]+" "+min[i] +"  "+max[i]);
            }*/
            //binding hour minute and second into a single string
            string second = ":" + temp % 100;
            temp /= 100;
            string minute = ":" + temp % 100;
            temp /= 100;
            string hour = "";
            if (temp > 0)
                hour = "" + temp % 100;
            else
                hour += "00";
            //Console.WriteLine(hour + minute + second);
            return hour + minute + second;
            throw new NotImplementedException();
        }
        //function which add two time
        static int addTime(int h, int m, int s, int hh, int mm, int ss)
        {
            //int ans = 0;
            int se = (ss + s) / 60;
            ss = (ss + s) % 60;
            int mi = (mm + m + se) / 60;
            mm = (mm + m + se) % 60;
            hh = (hh + mi + h) % 24;

            return hh * 10000 + mm * 100 + ss;
        }

        //function which parse string to int
        public static int[] ParseString(string s)
        {
            int[] ans = new int[3];
            int x = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == ':')
                {
                    x++;
                    continue;
                }
                ans[x] = ans[x] * 10 + s[i] - '0';
            }
            return ans;
        }
    }
}
