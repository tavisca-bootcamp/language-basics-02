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
            Console.WriteLine($"[{postTimesCsv}], [{showTimesCsv}] => {result}");
        }

		public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {
            int postLength = exactPostTime.Length;

            for (int i = 0; i <postLength; i++)
            {
                for (int j = i + 1; j < postLength; j++)
                {
                    if (exactPostTime[i] == exactPostTime[j])
                        if (showPostTime[i] != showPostTime[j])
                            return "impossible";
                }
            }

            string[] currentTime = new string[postLength]; 

            for (int i = 0; i < postLength; i++)
            {

                DateTime time = Convert.ToDateTime(exactPostTime[i]); //converting string to an equivalent date and time value

                if (showPostTime[i].Contains("seconds"))
                {
                    currentTime[i] = exactPostTime[i];
                }

                else if (showPostTime[i].Contains("minutes"))
                {
                    string minutes = showPostTime[i].Split(' ')[0];
                    DateTime current = time.AddMinutes(Int32.Parse(minutes)); //adding minutes to time
                    string result = current.ToString().Split(' ')[1];

                    currentTime[i] = result;
                }

                else if (showPostTime[i].Contains("hours"))
                {
                    string hours = showPostTime[i].Split(' ')[0];
                    DateTime current = time.AddHours(Int32.Parse(hours)); //adding hours to time
                    string result = current.ToString().Split(' ')[1];

                    currentTime[i] = result;
                }

                //if there is no solution
                else
                    return "impossible"

            }
            Array.Sort(currentTime);
            return currentTime[postLength - 1]; //choosing the largest element
            throw new NotImplementedException();
        }
            
    }
}
