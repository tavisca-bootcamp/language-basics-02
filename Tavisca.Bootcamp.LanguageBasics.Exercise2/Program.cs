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
            // Author : Aditi Rupade.

            TimeSpan currentTime = new TimeSpan(0),  // to store current times for each iteration
                 posttime  = new TimeSpan(0),     // to store parsed post times
                 latervalue = new TimeSpan(0);    // to store time after adding the message times

            // for same post times but showing different messages
            if (exactPostTimes.Distinct().Count() != showPostTimes.Distinct().Count())
                return "impossible";

            // iterating for each value in exactPostTimes array
            for (int i = 0; i < exactPostTimes.Length; i++)
            {
                //parsing and splitting 
                posttime = TimeSpan.Parse(exactPostTimes[i]);
                string[] val = showPostTimes[i].Split(' ');


                //Case 1: Checking for seconds
                if (val[1] =="seconds")
                {
                    if ( posttime > currentTime)
                        currentTime = posttime;

                }
                else
                {
                    int value = int.Parse(val[0]);

                    //Case 2: Checking for minutes
                    if (val[1].Contains("min"))
                    {
                        latervalue = TimeSpan.FromMinutes(value);

                        currentTime = TimeSpan.Parse(GetCurrentValue(latervalue, posttime, currentTime));
                    }

                    //Case 3: checking for hours
                    if (val[i].Contains("hour"))
                    {
                        latervalue = TimeSpan.FromHours(value);

                        currentTime = TimeSpan.Parse(GetCurrentValue(latervalue, posttime, currentTime));
                    }
                }
            }
            return currentTime.ToString();
        }
        
        public static string GetCurrentValue(TimeSpan latervalue, TimeSpan posttime, TimeSpan currentTime)
        {
            //if current time is next day
            if (posttime + latervalue > TimeSpan.Parse("1.00:00:00"))
            {
                posttime = posttime + latervalue - TimeSpan.FromDays(1);
                if (posttime > currentTime)
                    currentTime = posttime;
            }
            //if current time is same day
            else
                 if (posttime + latervalue > currentTime)
                currentTime = posttime + latervalue;

            return currentTime.ToString();
        }
    }
}
