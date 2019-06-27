using System;
using System.Linq;

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

            try
            {
                Test(new[] { "" }, new[] {"23 mins ago" },"");
                Test(new[] { "23:23:23", "23:23:23" }, new[] { "59 times ago", "59 minutes ago" }, "00:22:23");
            }
            catch(ArgumentException ae)
            {
                Console.WriteLine(ae.GetType().FullName);
                Console.WriteLine(ae.Message);
            }

            Console.ReadKey(true);
        }

        private static void Test(string[] postTimes, string[] showTimes, string expected)
        {
            var result = GetCurrentTime(postTimes, showTimes).Equals(expected) ? "PASS" : "FAIL";
            var postTimesCsv = string.Join(", ", postTimes);
            var showTimesCsv = string.Join(", ", showTimes);
            Console.WriteLine($"[{postTimesCsv}], [{showTimesCsv}] => {result}");
        }

        public static string GetCurrentTime(string[] exactPostTimes, string[] showPostTimes)
        {
            // Author : Aditi Rupade.

            int unitValue;
            TimeSpan currentTime = new TimeSpan(0),  // to store current times for each iteration
                 posttime  = new TimeSpan(0),     // to store parsed post times
                 latervalue = new TimeSpan(0);    // to store time after adding the message times

            // for same post times but showing different messages
            if (exactPostTimes.Distinct().Count() != showPostTimes.Distinct().Count())
                return "impossible";

            // iterating for each value in exactPostTimes array
            for (int i = 0; i < exactPostTimes.Length; i++)
            {
                //checking for null or empty value in first argument
                if (String.IsNullOrEmpty(exactPostTimes[i]))
                    throw new ArgumentException("argument cannot be null or empty", nameof(exactPostTimes));

                //checking for null or empty value in second argument
                if (String.IsNullOrEmpty(showPostTimes[i]))
                    throw new ArgumentException("argument cannot be null or empty", nameof(showPostTimes));

                //parsing and splitting 
                posttime = TimeSpan.Parse(exactPostTimes[i]);
                string[] splitShowPostTimes = showPostTimes[i].Split(' ');

                string unit = splitShowPostTimes[1];
                Int32.TryParse(splitShowPostTimes[0], out unitValue);


                //Case 1: Checking for seconds
                if (unit =="sec")
                {
                    if ( posttime > currentTime)
                        currentTime = posttime;

                }

                //Case 2: Checking for minutes
                else if (unit.Contains("min"))
                {
                    latervalue = TimeSpan.FromMinutes(unitValue);

                    currentTime = GetLowestCurrentTime(latervalue, posttime, currentTime);
                }

                //Case 3: checking for hours
                else if (unit.Contains("hour"))
                {
                    latervalue = TimeSpan.FromHours(unitValue);

                    currentTime = GetLowestCurrentTime(latervalue, posttime, currentTime);
                }

                //If the unit is not in hours or seconds or minutes
                //else
                //    throw new ArgumentException("argument message must be of the form X seconds/minutes/hours ago", nameof(showPostTimes));

            }
            return currentTime.ToString();
        }
        
        public static TimeSpan GetLowestCurrentTime(TimeSpan latervalue, TimeSpan posttime, TimeSpan currentTime)
        {
            TimeSpan laterValue = posttime + latervalue;
            //if current time is next day then subtracting 1 day then adding lowest value
            if (laterValue > TimeSpan.Parse("1.00:00:00"))
            {
                posttime = laterValue - TimeSpan.FromDays(1);
                if (posttime > currentTime)
                    currentTime = posttime;
            }
            //if current time is same day then adding lowest value and comparing
            else
                 if (laterValue > currentTime)
                currentTime = laterValue;

            return currentTime;
        }

    }
}
