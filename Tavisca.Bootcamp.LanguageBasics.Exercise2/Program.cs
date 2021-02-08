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

        public static string[] ShowTime(string status)
        {
            Int32 count = 2;
            if (status.Equals("few seconds ago"))
            {
                return new[] { "00:00:00", "00:00:59" };
            }
            if (status.EndsWith("minutes ago"))
            {

                string[] temp = status.Split(new[] { ' ' }, count);
                string t = "00:" + temp[0] + ":";
                return new[] { t + "00", t + "59" };
            }
            if (status.EndsWith("hours ago"))
            {
                string[] temp = status.Split(new[] { ' ' }, count);
                string t = temp[0] + ":";
                return new[] { t + "00:00", t + "59:59" };

            }
            return new[] { "", "" };

        }
        public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {
            // Add your code here.
            if (!Validate(exactPostTime, showPostTime)) 
            return "impossible";
            int k = 0;
            string[] result = new string[exactPostTime.Length * 2];
            string min=string.Empty, max=string.Empty;
            for (int i = 0; i < exactPostTime.Length; i++)
            {
                Int32 count = 3;
                char[] seprate = new[] { ':' };
                string[] exactPostTimeList = exactPostTime[i].Split(seprate, count);
                string[] _showPostTime = ShowTime(showPostTime[i]);

                string curr_min= string.Empty, curr_max=string.Empty;
                for (int j = 0; j < 2; j++)
                {
                    string[] showPostTimeList = _showPostTime[j].Split(seprate, count);
                    int sec = Int32.Parse(exactPostTimeList[2]) + Int32.Parse(showPostTimeList[2]);
                    int carry = 0;
                    string h = String.Empty, m = String.Empty, s = string.Empty;
                    if (sec >= 60)
                    {
                        carry = sec / 60;
                        sec = sec % 60;

                    }
                    int minute = Int32.Parse(exactPostTimeList[1]) + Int32.Parse(showPostTimeList[1]) + carry;
                    carry = 0;
                    if (minute >= 60)
                    {
                        carry = minute / 60;
                        minute = minute % 60;
                    }
                    int hour = Int32.Parse(exactPostTimeList[0]) + Int32.Parse(showPostTimeList[0]) + carry;
                    carry = 0;
                    if (hour >= 24)
                    {
                        carry = hour / 24;
                        hour = hour % 24;
                    }
                    h += hour; m += minute;s += sec;
                    if (h.Length == 1) h = "0" + h;
                    if (m.Length == 1) m = "0" + m;
                    if (h.Length == 1) s = "0" + s;
                    if (j==0) curr_min = carry+";"+h + ":" + minute + ":" + sec;
                    if (j == 1) curr_max = carry + ";" + h + ":" + minute + ":" + sec;
                }
                if (i == 0) { min = curr_min; max = curr_max; }
                else
                {
                    
                    if(string.Compare(min,curr_min) <0)
                    {
                        if(string.Compare(max,curr_min)>0)
                        {
                            min = curr_min;
                        }

                    }
                    if (string.Compare(min, curr_max) < 0)
                    {
                       
                        if (string.Compare(max, curr_max) > 0)
                        {
                            max = curr_max;
                        }

                    }

                }

               

            }

            string[] TimeRange = min.Split(new[] { ';' });
            return TimeRange[1];
        }
    }
}
