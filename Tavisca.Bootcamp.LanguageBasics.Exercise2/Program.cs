using System;
using System.Collections;

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
            //list for exact post time
            ArrayList exactPostTimeList = new ArrayList();

            //list for time when post is made
            ArrayList showPostTimeList = new ArrayList();

            //list for current time
            ArrayList currentTimeList = new ArrayList();
            try
            {
                //Iterating  and adding to post and show time to list
                for (int i = 0; i < exactPostTime.Length; i++)
                {
                    string[] exactPostTimeSplit = exactPostTime[i].Split(':');
                    DateTime d = new DateTime(2001, 01, 01, int.Parse(exactPostTimeSplit[0]), int.Parse(exactPostTimeSplit[1]), int.Parse(exactPostTimeSplit[2]));

                    //checking if post time is same then shown time must also be same
                    if ((exactPostTimeList.Contains(d) && showPostTimeList.Contains(showPostTime[i])) || (!exactPostTimeList.Contains(d)) && !showPostTimeList.Contains(showPostTime[i]))
                    {
                        showPostTimeList.Add(showPostTime[i]);
                        exactPostTimeList.Add(d);
                    }
                    else
                    {
                        return "impossible";
                    }
                }

                //Iterating and calculating current time
                for (int i = 0; i < showPostTime.Length; i++)
                {
                    string[] postTime = showPostTime[i].Split(' ');
                    DateTime dt = (DateTime)exactPostTimeList[i];
                    switch (postTime[1])
                    {
                        //adding hours
                        case "hours":
                            dt = dt.AddHours(double.Parse(postTime[0]));
                            break;

                        //adding minutes
                        case "minutes":
                            dt = dt.AddMinutes(double.Parse(postTime[0]));
                            break;

                        //adding seconds
                        case "seconds":
                            if (postTime[0].Equals("few"))
                            {
                                dt = dt.AddSeconds(0);
                            }
                            else
                            {
                                dt = dt.AddSeconds(double.Parse(postTime[0]));
                            }
                            break;
                    }
                    // converting date time to string
                    currentTimeList.Add(dt.ToLongTimeString());
                }
                //sorting current time list
                currentTimeList.Sort();
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.GetType().Name);
            }
            return currentTimeList[currentTimeList.Count-1].ToString();
        }
    }
}
