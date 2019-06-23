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
            int flag = 0;
            String ans = "";
            for (int i = 0; i < exactPostTime.Length; i++)
            {
                flag = 0;
                for (int j = i + 1; j < exactPostTime.Length; j++)
                {
                    if (exactPostTime[i] == exactPostTime[j])
                    {
                        if (showPostTime[i] == showPostTime[j])
                        {
                            continue;
                        }
                        else
                        {
                            flag = 1;
                            break;
                        }

                    }
                }
                if (flag == 1)
                {
                    ans = "impossible";
                    return ans;
                }
            }
            if (flag == 0)
            {

                String[] min = new String[exactPostTime.Length];
                String[] max = new String[exactPostTime.Length];
                for (int i = 0; i < exactPostTime.Length; i++)
                {

                    String[] postArray = exactPostTime[i].Split(":");
                    String[] showArray = showPostTime[i].Split(" ");
                    if (showArray[1].Equals("seconds"))
                    {
                        // System.out.println("*");
                        min[i] = exactPostTime[i];
                        int sec = Int32.Parse(postArray[2]);
                        int minute = Int32.Parse(postArray[1]);
                        int hour = Int32.Parse(postArray[0]);
                        sec = sec + 59;
                        if (sec >= 60)
                        {
                            //System.out.println(sec);
                            sec = sec - 60;
                            minute = minute + 1;
                        }
                        if (minute >= 60)
                        {
                            minute = minute - 60;
                            hour = hour + 1;
                        }
                        if (hour >= 24)
                        {
                            hour = hour - 24;
                        }
                        max[i] = Convert.ToString(hour) + ":" + Convert.ToString(minute) + ":" + Convert.ToString(sec);
                    }
                    else if (showArray[1].Equals("minutes"))
                    {
                        String maxHour1 = "";
                        String minHour1 = "";
                        //System.out.println("**");
                        int minSec = Int32.Parse(postArray[2]);
                        int minMinute = Int32.Parse(postArray[1]);
                        int minHour = Int32.Parse(postArray[0]);
                        minMinute = minMinute + Int32.Parse(showArray[0]);
                        if (minMinute >= 60)
                        {
                            minMinute = minMinute - 60;
                            minHour = minHour + 1;
                        }
                        if (minHour >= 24)
                        {
                            minHour = minHour - 24;
                        }
                        if (minHour < 10)
                        {
                            minHour1 = "0" + Convert.ToString(minHour);
                        }
                        else
                        {
                            minHour1 = Convert.ToString(minHour);
                        }
                        min[i] = (minHour1) + ":" + Convert.ToString(minMinute) + ":" + Convert.ToString(minSec);
                        int maxSec = minSec;
                        int maxMinute = minMinute;
                        int maxHour = minHour;
                        maxSec = maxSec + 59;
                        if (maxSec >= 60)
                        {
                            maxSec = maxSec - 60;
                            maxMinute = maxMinute + 1;
                        }
                        if (maxMinute >= 60)
                        {
                            maxMinute = maxMinute - 60;
                            maxHour = maxHour + 1;
                        }
                        if (maxHour >= 24)
                        {
                            maxHour = maxHour - 24;
                        }
                        if (maxHour < 10)
                        {
                            maxHour1 = "0" + Convert.ToString(maxHour);
                        }
                        else
                        {
                            maxHour1 = Convert.ToString(maxHour);

                        }
                        max[i] = (maxHour1) + ":" + Convert.ToString(maxMinute) + ":" + Convert.ToString(maxSec);
                    }
                    else if (showArray[1].Equals("hours"))
                    {
                        //System.out.println("***");
                        int minSec = Int32.Parse(postArray[2]);
                        int minMinute = Int32.Parse(postArray[1]);
                        int minHour = Int32.Parse(postArray[0]);
                        minHour = minHour + Int32.Parse(showArray[0]);
                        if (minHour >= 24)
                        {
                            minHour = minHour - 24;
                        }
                        min[i] = Convert.ToString(minHour) + ":" + Convert.ToString(minMinute) + ":" + Convert.ToString(minSec);


                        int maxSec = minSec;
                        int maxMinute = minMinute;
                        int maxHour = minHour;
                        maxSec = maxSec + 59;
                        if (maxSec >= 60)
                        {
                            maxSec = maxSec - 60;
                            maxMinute = maxMinute + 1;
                        }
                        if (maxMinute >= 60)
                        {
                            maxMinute = maxMinute - 60;
                            maxHour = maxHour + 1;
                        }
                        if (maxHour >= 24)
                        {
                            maxHour = maxHour - 24;
                        }
                        max[i] = Convert.ToString(maxHour) + ":" + Convert.ToString(maxMinute) + ":" + Convert.ToString(maxSec);
                    }
                    //System.out.println(min[i]+" "+max[i]);
                }
                Array.Sort(min);
                ans = (min[min.Length - 1]);
            }
            return ans;
            throw new NotImplementedException();
        }
    }
}
