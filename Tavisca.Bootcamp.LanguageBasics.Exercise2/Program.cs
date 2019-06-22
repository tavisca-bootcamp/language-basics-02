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
            // Add your code here.
            //throw new NotImplementedException();
            string min = "00:00:00", max = "23:59:59";
            for (int i = 0; i < exactPostTime.Length; i++)
            {
                string back = "";
                string present = "";
                int hours, minutes, seconds;
                hours = Convert.ToInt32(exactPostTime[i].Substring(0, 2));
                minutes = Convert.ToInt32(exactPostTime[i].Substring(3, 2));
                seconds = Convert.ToInt32(exactPostTime[i].Substring(6, 2));
                string[] message = showPostTime[i].Split(" ");
                if (string.Compare(message[1], "seconds") == 0)
                {
                    back = exactPostTime[i];
                    seconds = seconds + 59;
                    if (seconds % 60 != seconds)
                    {
                        seconds = seconds % 60;
                        minutes++;
                        if (minutes % 60 != minutes)
                        {
                            minutes = minutes % 60;
                            hours++;
                            if (hours % 24 != hours)
                            {
                                hours = hours % 24;

                            }
                        }
                    }
                    string h = hours.ToString(), m = minutes.ToString(), s = seconds.ToString();
                    if (hours <= 9)
                        h = "0" + hours.ToString();
                    if (minutes <= 9)
                        m = ":0" + minutes.ToString();
                    if (seconds <= 9)
                        s = ":0" + seconds.ToString();
                    present = h + ":" + m + ":" + s;


                }
                else if (string.Compare(message[1], "minutes") == 0)
                {
                    minutes = minutes + Int32.Parse(message[0]);
                    if (minutes % 60 != minutes)
                    {
                        minutes = minutes % 60;
                        hours++;
                        if (hours % 24 != hours)
                        {
                            hours = hours % 24;

                        }
                    }
                    string h = hours.ToString(), m = minutes.ToString(), s = seconds.ToString();
                    if (hours <= 9)
                        h = "0" + hours.ToString();
                    if (minutes <= 9)
                        m = ":0" + minutes.ToString();
                    if (seconds <= 9)
                        s = ":0" + seconds.ToString();
                    back = h + ":" + m + ":" + s;

                    seconds = seconds + 59;
                    if (seconds % 60 != seconds)
                    {
                        seconds = seconds % 60;
                        minutes++;
                        if (minutes % 60 != minutes)
                        {
                            minutes = minutes % 60;
                            hours++;
                            if (hours % 24 != hours)
                            {
                                hours = hours % 24;

                            }
                        }


                    }
                    h = hours.ToString(); m = minutes.ToString(); s = seconds.ToString();
                    if (hours <= 9)
                        h = "0" + hours.ToString();
                    if (minutes <= 9)
                        m = ":0" + minutes.ToString();
                    if (seconds <= 9)
                        s = ":0" + seconds.ToString();
                    present = h + ":" + m + ":" + s;
                }
                else
                {
                    hours = hours + Int32.Parse(message[0]);
                    hours = hours % 24;
                    string h = hours.ToString(), m = minutes.ToString(), s = seconds.ToString();
                    if (hours <= 9)
                        h = "0" + hours.ToString();
                    if (minutes <= 9)
                        m = ":0" + minutes.ToString();
                    if (seconds <= 9)
                        s = ":0" + seconds.ToString();
                    back = h + ":" + m + ":" + s;
                    seconds = seconds + 59;
                    if (seconds % 60 != seconds)
                    {
                        seconds = seconds % 60;
                        minutes++;
                        if (minutes % 60 != minutes)
                        {
                            minutes = minutes % 60;
                            hours++;
                            if (hours % 24 != hours)
                            {
                                hours = hours % 24;

                            }
                        }


                    }
                    minutes = minutes + 59;
                    if (minutes % 60 != minutes)
                    {
                        minutes = minutes % 60;
                        hours++;
                        if (hours % 24 != hours)
                        {
                            hours = hours % 24;

                        }
                    }
                    h = hours.ToString(); m = minutes.ToString(); s = seconds.ToString();
                    if (hours <= 9)
                        h = "0" + hours.ToString();
                    if (minutes <= 9)
                        m = ":0" + minutes.ToString();
                    if (seconds <= 9)
                        s = ":0" + seconds.ToString();
                    present = h + ":" + m + ":" + s;

                }
                if (string.Compare(min, back) == -1)
                    min = back;
                if (string.Compare(max, present) == 1)
                    max = present;
            }
            if (string.Compare(min, max) == -1)
                return min;
            return "impossible";
        }
    }
}


