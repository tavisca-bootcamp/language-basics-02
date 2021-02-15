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
            List<TimeSpan> listOfTime = new List<TimeSpan>();
            TimeSpan[] time = new TimeSpan[exactPostTime.Length];
            TimeSpan currentTime = TimeSpan.Parse("23:59:59");

            for (int i = 0; i < exactPostTime.Length; i++)
            {
                time[i] = TimeSpan.Parse(exactPostTime[i]);
            }

            for (int i = 0; i < showPostTime.Length; i++)
            {
                if (showPostTime[i] == "few seconds ago")
                {
                    TimeSpan time1 = time[i].Add(new TimeSpan(00, 00, 00));
                    TimeSpan time2 = time[i].Add(new TimeSpan(00, 00, 59));

                    listOfTime.Add(new TimeSpan(time2.Hours, time2.Minutes, time2.Seconds));
                    listOfTime.Add(new TimeSpan(time2.Hours, time2.Minutes, time2.Seconds));

                }
                if (showPostTime[i].Split(" ")[1] == "minutes")
                {
                    int X = int.Parse(showPostTime[i].Split(" ")[0]);

                    TimeSpan time1 = time[i].Add(new TimeSpan(00, X, 00));
                    TimeSpan time2 = time[i].Add(new TimeSpan(00, X, 59));

                    listOfTime.Add(new TimeSpan(time1.Hours, time1.Minutes, time1.Seconds));
                    listOfTime.Add(new TimeSpan(time2.Hours, time2.Minutes, time2.Seconds));

                }
                if (showPostTime[i].Split(" ")[1] == "hours")
                {
                    int X = int.Parse(showPostTime[i].Split(" ")[0]);

                    TimeSpan time1 = time[i].Add(new TimeSpan(X, 00, 00));
                    TimeSpan time2 = time[i].Add(new TimeSpan(X, 59, 59));

                    listOfTime.Add(new TimeSpan(time1.Hours, time1.Minutes, time1.Seconds));
                    listOfTime.Add(new TimeSpan(time2.Hours, time2.Minutes, time2.Seconds));
                }
            }
            for (int index = 0; index < listOfTime.Count; index += 2)
            {
                TimeSpan arivalTime = listOfTime[index];
                TimeSpan endTime = listOfTime[index + 1];
                bool isValidArrival = true;
                bool isValidEnd = true;
                for (int i = 0; i < listOfTime.Count; i += 2)
                {
                    if (isValidArrival == true && arivalTime.CompareTo(listOfTime[i]) >= 0
                        && arivalTime.CompareTo(listOfTime[i + 1]) <= 0)
                    {

                    }
                    else
                    {
                        isValidArrival = false;
                    }
                    if (isValidEnd == true && endTime.CompareTo(listOfTime[i]) >= 0
                        && endTime.CompareTo(listOfTime[i + 1]) <= 0)
                    {

                    }
                    else
                    {

                        isValidEnd = false;
                    }
                }
                if (isValidArrival == true && arivalTime.CompareTo(currentTime) == -1)
                {
                    currentTime = arivalTime;
                }
                if (isValidEnd == true && endTime.CompareTo(currentTime) == -1)
                {
                    currentTime = endTime;
                }
            }
            string result = "";
            if (currentTime == new TimeSpan(23, 59, 59))
            {
                result = "impossible";
            }
            else
            {
                result = currentTime.ToString();
            }


            return result;
        }
    }
}
