using System;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    class Program
    {
        static void Main(string[] args)
        {
            Test(new[] { "12:12:12" }, new[] { "few seconds ago" }, "12:12:12");
            Test(new[] { "23:23:23", "23:23:23" }, new[] { "59 minutes ago", "59 minutes ago" }, "00:22:23");
            Test(new[] { "00:10:10", "00:10:10" }, new[] { "59 minutes ago", "1 hours ago" }, "impossible");
            //11:59:13 lexicographically smaller than 11:59:23
            Test(new[] { "11:59:13", "11:13:23", "12:25:15" }, new[] { "few seconds ago", "46 minutes ago", "23 hours ago" }, "11:59:13");
            Console.ReadKey(true);
        }

        private static void Test(string[] postTimes, string[] showTimes, string expected)
        {
            var result = GetCurrentTime(postTimes, showTimes).Equals(expected) ? "PASS" : "FAIL";
            var postTimesCsv = string.Join(", ", postTimes);
            var showTimesCsv = string.Join(", ", showTimes);
            Console.WriteLine($"[{postTimesCsv}], [{showTimesCsv}] => {result}");
        }

        private enum TimeUnit { HOURS, MINUTES, SECONDS };

        //Generates strings for possible current time for a given current time and precision unit.
        private static string[] GenerateTimeStrings(DateTime currentTime, TimeUnit unit)
        {
            string[] currentTimeStrings = null;
            switch (unit)
            {
                case TimeUnit.MINUTES:
                    const short mSolutionCount = 60 * 60;
                    currentTimeStrings = new string[mSolutionCount];
                    currentTimeStrings[0] = currentTime.ToString("HH:mm:ss");
                    for (short i = 1; i < mSolutionCount; i++)
                        currentTimeStrings[i] = currentTime.AddSeconds(1).ToString("HH:mm:ss");
                    break;
                case TimeUnit.SECONDS:
                    const short sSolutionCount = 60;
                    currentTimeStrings = new string[sSolutionCount];
                    currentTimeStrings[0] = currentTime.ToString("HH:mm:ss");
                    for (short i = 1; i < sSolutionCount; i++)
                        currentTimeStrings[i] = currentTime.AddSeconds(1).ToString("HH:mm:ss");
                    break;
            }

            return currentTimeStrings;
        }

        private static short ExtractLeadingNumber(string message)
        {
            return short.Parse(message.Substring(0, message.IndexOf(' ')));
        }

        //Returns true if two DateTime are equal for a given precision unit.
        private static bool AreTimesEquivalent(DateTime timeA, DateTime timeB, TimeUnit smallestUnit)
        {
            switch (smallestUnit)
            {
                case TimeUnit.HOURS:
                    if (timeA.Hour == timeB.Hour)
                        return true;
                    break;
                case TimeUnit.MINUTES:
                    if (timeA.Hour == timeB.Hour && timeA.Minute == timeB.Minute)
                        return true;
                    break;
                case TimeUnit.SECONDS:
                    if (timeA.Hour == timeB.Hour && timeA.Minute == timeB.Minute)
                        return true;
                    break;
            }

            return false;
        }

        public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {
            DateTime currentTime = new DateTime();
            string[] currentTimeStrings = null;
            TimeUnit smallestUnit = TimeUnit.HOURS;

            for (int i = 0; i < exactPostTime.Length; i++)
            {
                //Parse exactPostTime
                DateTime postTime = DateTime.Parse(exactPostTime[i]);
                string message = showPostTime[i];

                //Generate possible solution for current time corresponding to first message.
                if (i == 0)
                {
                    if (message.Contains("seconds"))
                    {
                        currentTime = postTime;
                        currentTimeStrings = GenerateTimeStrings(currentTime, TimeUnit.SECONDS);
                        smallestUnit = TimeUnit.SECONDS;
                    }
                    else if (message.Contains("minutes"))
                    {
                        short minutes = ExtractLeadingNumber(message);
                        currentTime = postTime.AddMinutes(minutes);
                        currentTimeStrings = GenerateTimeStrings(currentTime, TimeUnit.SECONDS);
                        smallestUnit = TimeUnit.MINUTES;
                    }
                    else if (message.Contains("hours"))
                    {
                        short hours = ExtractLeadingNumber(message);
                        currentTime = postTime.AddHours(hours);
                        currentTimeStrings = GenerateTimeStrings(currentTime, TimeUnit.MINUTES);
                        smallestUnit = TimeUnit.HOURS;
                    }
                }

                //Check if the following messages are consistent with the first one.
                else
                {
                    short number = ExtractLeadingNumber(message);
                    DateTime newCurrentTime;
                    if (message.Contains("hours"))
                    {
                        newCurrentTime = postTime.AddHours(number);
                        if (!AreTimesEquivalent(currentTime, newCurrentTime, smallestUnit))
                            return "impossible";
                    }
                    else if (message.Contains("minutes"))
                    {
                        newCurrentTime = postTime.AddMinutes(number);
                        if (!AreTimesEquivalent(currentTime, newCurrentTime, smallestUnit))
                            return "impossible";
                    }

                }
            }

            //To find lexicographically smallest time string.
            Array.Sort(currentTimeStrings);
            return currentTimeStrings[0];
        }
    }
}
