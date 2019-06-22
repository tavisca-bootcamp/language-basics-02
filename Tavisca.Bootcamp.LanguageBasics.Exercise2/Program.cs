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
            //Console.ReadKey(true);
        }

        private static void Test(string[] postTimes, string[] showTimes, string expected)
        {
            string res = GetCurrentTime(postTimes, showTimes);
            var result = res.Equals(expected) ? "PASS" : "FAIL";
            var postTimesCsv = string.Join(", ", postTimes);
            var showTimesCsv = string.Join(", ", showTimes);
            Console.WriteLine($"[{postTimesCsv}], [{showTimesCsv}] => {result}");
        }

        public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {
            int elen = exactPostTime.Length;
            DateTime[] mintm = new DateTime[elen];
            DateTime[] maxtm = new DateTime[elen];
            DateTime currt = Convert.ToDateTime(exactPostTime[0]); ;

            if (isValid(exactPostTime, showPostTime) == 1)
            {
                for (int i = 0; i < elen; i++)
                {
                    DateTime eptm = Convert.ToDateTime(exactPostTime[i]);
                    String[] sptm = showPostTime[i].Split();
                    switch (sptm[1])
                    {
                        case "seconds":
                            mintm[i] = eptm;
                            maxtm[i] = eptm.AddSeconds(59);
                            break;
                        case "minutes":
                            mintm[i] = eptm.AddMinutes(Convert.ToInt32(sptm[0]));
                            maxtm[i] = mintm[i].AddSeconds(59);
                            break;
                        case "hours":
                            mintm[i] = eptm.AddHours(Convert.ToInt32(sptms[0]));
                            maxtm[i] = mintm[i].AddMinutes(59);
                            maxtm[i] = maxtm[i].AddSeconds(59);
                            break;
                    }
                }
                currt = mintm[0];
                for (int i = 0; i < elen; i++)
                {
                    if (mintm[i].TimeOfDay > currt.TimeOfDay)
                        currt = mintm[i];
                }

                return (currt.ToString("HH':'mm':'ss"));
            }
            else
            {
                return "impossible";
            }
        }

        public static int isValid(string[] exactPostTime, string[] showPostTime)
        {
            int len = exactPostTime.Length;
            for (int i = 0; i < len; i++)
            {
                for (int j = i; j < len; j++)
                {
                    if (exactPostTime[i].Equals(exactPostTime[j]))
                        if (!showPostTime[i].Equals(showPostTime[j]))
                            return 0;

                }
            }
            return 1;
        }
    }
}