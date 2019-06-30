using System;

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
            var currentTimeLowerBounds = new DateTime[exactPostTime.Length];
            var currentTimeUpperBounds = new DateTime[exactPostTime.Length];

            for (int i = 0; i < exactPostTime.Length; i++)
            {
                for (int j = i + 1; j < exactPostTime.Length; j++)
                {
                    if (exactPostTime[i] == exactPostTime[j])
                    {
                        if (showPostTime[i] != showPostTime[j])
                        {
                            return "impossible";
                        }
                    }
                }

                DateTime.TryParse(exactPostTime[i], out DateTime exactTime);
                var currentTimeLowerBound = exactTime;
                var currentTimeUpperBound = exactTime;
                var displayedPostTimeComponents = showPostTime[i].Split(' ');

                //Determining possible corresponding upper bound and lower vound values of current time.
                switch (displayedPostTimeComponents[1])
                {
                    case "seconds":
                        currentTimeUpperBound = exactTime.AddSeconds(59);
                        break;

                    case "minutes":
                        currentTimeLowerBound = exactTime.AddMinutes(double.Parse(displayedPostTimeComponents[0]));
                        currentTimeUpperBound = currentTimeLowerBound.AddSeconds(59);
                        break;

                    case "hours":
                        currentTimeLowerBound = exactTime.AddHours(double.Parse(displayedPostTimeComponents[0]));
                        currentTimeUpperBound = currentTimeLowerBound.AddMinutes(59);
                        currentTimeUpperBound = currentTimeLowerBound.AddSeconds(59);
                        break;

                    default:
                        break;
                }

                currentTimeLowerBounds[i] = currentTimeLowerBound;
                currentTimeUpperBounds[i] = currentTimeUpperBound;
            }

            //sorting upperbounds in ascending order and lower bounds in descending order
            Array.Sort(currentTimeUpperBounds);
            Array.Sort<DateTime>(currentTimeLowerBounds, new Comparison<DateTime>(
                  (i1, i2) => i2.CompareTo(i1)));

            //comapring lowerbounds to actual upper bound to obtain the 
            //max possible value for the lowerbound that is smaller than upperbound.            
            var actualUpperBound = currentTimeUpperBounds[0];
            foreach (var lowerBound in currentTimeLowerBounds)
            {
                if (DateTime.Compare(actualUpperBound, lowerBound) > 0)
                {
                    return lowerBound.TimeOfDay.ToString();
                }
            }

            return currentTimeLowerBounds[0].TimeOfDay.ToString();
        }
    }
}