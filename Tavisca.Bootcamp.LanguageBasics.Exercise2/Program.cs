using System;
using System.Linq;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class ForumPostEasy
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
            string contradictoryTime = "impossible";
            for (int index = 0; index < exactPostTime.Length; index++)
            {
                for (int position = index + 1; position < exactPostTime.Length; position++)
                {
                    if (exactPostTime[index] == exactPostTime[position])
                        if (showPostTime[index] != showPostTime[position])
                            return contradictoryTime;
                }
            }

            
            string[] currentTime = new string[exactPostTime.Length];

            
            for (int index = 0; index < exactPostTime.Length; index++)
            {

                
                string[] split = exactPostTime[index].Split(":");
                DateTime timeOfDay = new DateTime(2000, 1, 01, Convert.ToInt32(split[0]), Convert.ToInt32(split[1]), Convert.ToInt32(split[2]));

                                
                //for 00:00:00 to 00:00:59
                if (showPostTime[index].Contains("seconds"))
                {
                    currentTime[index] = exactPostTime[index];
                }

                //for 00:01:00 to 00:59:59
                else if (showPostTime[index].Contains("minutes"))
                {

                    string min = showPostTime[index].Split(" ")[0];                
                    TimeSpan time = new TimeSpan(0, Convert.ToInt32(min),0);
                    currentTime[index] = timeOfDay.Add(time).ToString().Split(" ")[1];  

                }

                //for 01:00:00 and before
                else if (showPostTime[index].Contains("hours"))
                {

                    string hours = showPostTime[index].Split(" ")[0];
                    TimeSpan timeSpan = new TimeSpan(Convert.ToInt32(hours), 0, 0);
                    currentTime[index] = timeOfDay.Add(timeSpan).ToString().Split(" ")[1];

                }

                //Exception handling for empty strings
                try
                {
                    if (showPostTime[index].Length == 0)
                        throw new NullReferenceException();
                }
                catch (NullReferenceException ex)
                {
                    Console.WriteLine("Empty post time value " + ex);
                    return contradictoryTime;
                }
            }


            Array.Sort(currentTime);
            return currentTime[(exactPostTime.Length - 1)];
            
        }
        }
    }
