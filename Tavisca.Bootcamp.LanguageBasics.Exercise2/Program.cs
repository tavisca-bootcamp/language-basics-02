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
            // Add your code here.
            int totalPosts = exactPostTime.Length;
            for (int i = 0; i < totalPosts; i++)
            {
                for (int j = i + 1; j < totalPosts; j++)
                {
                    if (exactPostTime[i] == exactPostTime[j])
                    {
                        if (showPostTime[i] != showPostTime[j])
                        {
                            Console.WriteLine("Impossible!");
                            return "impossible";
                        }
                    }
                }
            }

            string[] resultantCurrentTime = new string[totalPosts];
            for (int i = 0; i < totalPosts; i++)
            {
                string[] splittedTimeIntoHoursMinSec = exactPostTime[i].Split(":");
                DateTime tempDateTimeObject = new DateTime(2019, 6, 21, Int32.Parse(splittedTimeIntoHoursMinSec[0]), Int32.Parse(splittedTimeIntoHoursMinSec[1]), Int32.Parse(splittedTimeIntoHoursMinSec[2]));

                if (showPostTime[i].Contains("seconds"))
                {
                    resultantCurrentTime[i] = exactPostTime[i];
                }
                else if (showPostTime[i].Contains("minutes"))
                {
                    string spanAfterPostInMinutes = showPostTime[i].Split(" ")[0];
                    TimeSpan timeSpanObject = new TimeSpan(0, 0, Int32.Parse(spanAfterPostInMinutes), 0);
                    resultantCurrentTime[i] = tempDateTimeObject.Add(timeSpanObject).ToString().Split(" ")[1];
                }
                else
                {
                    string spanAfterPostInHours = showPostTime[i].Split(" ")[0];
                    TimeSpan timeSpanObject = new TimeSpan(0, Int32.Parse(spanAfterPostInHours), 0, 0);
                    resultantCurrentTime[i] = tempDateTimeObject.Add(timeSpanObject).ToString().Split(" ")[1];
                }
            }

            Array.Sort(resultantCurrentTime);
            Console.WriteLine(resultantCurrentTime[totalPosts - 1]);
            /*for (int i = 0; i < resultantCurrentTime.Length; i++)
                Console.WriteLine("-{0}-", resultantCurrentTime[i]);*/
            string[] temp = resultantCurrentTime[totalPosts - 1].Split(":");
            string ans;
            ans = Convert.ToString(Convert.ToInt32(temp[0]) % 24) + ":" + temp[1] + ":" + temp[2];
            
            return ans;
            throw new NotImplementedException();
        }
    }
}
