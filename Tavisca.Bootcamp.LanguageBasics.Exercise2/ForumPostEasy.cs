using System;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise2
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
            // Add your code here.
            int length = exactPostTime.Length;

            for (int i = 0; i < length; i++)
            {
                for (int j = i + 1; j < length; j++)
                {
                    if (exactPostTime[i] == exactPostTime[j])
                        if (showPostTime[i] != showPostTime[j])
                            return "impossible";
                }
            }

            string[] currentTime = new string[length]; //new array to store current-time

            for (int i = 0; i < length; i++)
            {
                DateTime time = Convert.ToDateTime(exactPostTime[i]); //converting string to an equivalent date and time value

                if (showPostTime[i].Contains("seconds"))   //if showPostTime contains "seconds"
                {
                    currentTime[i] = exactPostTime[i];
                }
                
                else if (showPostTime[i].Contains("minutes"))  //if showPostTime contains "minutes"
                {
                    string minutes = showPostTime[i].Split(' ')[0];
                    DateTime newTime = time.AddMinutes(Int32.Parse(minutes)); //adding minutes to time
                    string result = newTime.ToString().Split(' ')[1];
                    currentTime[i] = result;
                }

                else if (showPostTime[i].Contains("hours"))  //if showPostTime contains "hours"
                {
                    string hours = showPostTime[i].Split(' ')[0];
                    DateTime newTime = time.AddHours(Int32.Parse(hours)); //adding hours to time
                    string result = newTime.ToString().Split(' ')[1];
                    currentTime[i] = result;
                }
              
                else                // //if there is no solution
                    return "impossible";
            }

            Array.Sort(currentTime);
            return currentTime[length - 1]; //choosing the largest element
        }
    }
}
