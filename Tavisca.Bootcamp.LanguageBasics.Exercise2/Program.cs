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
            String ans = "";
            Boolean check = isImpossible(exactPostTime, showPostTime);
            if (check == true)
            {
                ans = "impossible";
            }
            else
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
                        max[i] = maximum1(postArray);
                    }
                    else if (showArray[1].Equals("minutes"))
                    {
                        min[i] = minimum1(postArray, showArray, "minutes");
                        String[] minSplit = min[i].Split(":");
                        max[i] = maximum1(minSplit);
                    }
                    else if (showArray[1].Equals("hours"))
                    {
                        min[i] = minimum1(postArray, showArray, "hours");
                        String[] minSplit = min[i].Split(":");
                        max[i] = maximum1(minSplit);
                    }
                }
                Array.Sort(min);
                ans = (min[min.Length - 1]);
            }
            return ans;
        }
        public static Boolean isImpossible(String[] postTimes, String[] showTimes)
        {
            for (int i = 0; i < postTimes.Length; i++)
            {
                for (int j = i + 1; j < postTimes.Length; j++)
                {
                    if (postTimes[i] == postTimes[j])
                    {
                        if (showTimes[i] == showTimes[j])
                        {
                            continue;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public static String minimum1(String[] postArray, String[] showArray, String time)
        {
            String result = "";
            String minHour1 = "";
            int minSec = Int32.Parse(postArray[2]);
            int minMinute = Int32.Parse(postArray[1]);
            int minHour = Int32.Parse(postArray[0]);
            if (time == "minutes")
            {
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
                result = (minHour1) + ":" + Convert.ToString(minMinute) + ":" + Convert.ToString(minSec);
            }
            else if (time == "hours")
            {
                minHour = minHour + Int32.Parse(showArray[0]);
                if (minHour >= 24)
                {
                    minHour = minHour - 24;
                }
                result = Convert.ToString(minHour) + ":" + Convert.ToString(minMinute) + ":" + Convert.ToString(minSec);
            }
            return result;
        }
        public static String maximum1(String[] postArray)
        {
            String result = "";
            String maxHour1 = "";
            int maxSec = Int32.Parse(postArray[2]);
            int maxMinute = Int32.Parse(postArray[1]);
            int maxHour = Int32.Parse(postArray[0]);
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
            result = Convert.ToString(maxHour) + ":" + Convert.ToString(maxMinute) + ":" + Convert.ToString(maxSec);
            return result;
        }
    }
}
