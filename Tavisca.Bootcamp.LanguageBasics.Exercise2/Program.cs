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
            int numberOfPosts = exactPostTime.Length;
            string[] currentTimesArray = new string[numberOfPosts];

            for (var index = 0 ; index < numberOfPosts ; index++ ){
                if( index < numberOfPosts-1 && exactPostTime[index] == exactPostTime[index+1] && showPostTime[index] != showPostTime[index+1]){
                    return "impossible";
                }
                TimeSpan startTime = TimeSpan.Parse(exactPostTime[index]);
                TimeSpan endTime;
                if (showPostTime[index].Contains("seconds")){
                    endTime = new TimeSpan(startTime.Hours, startTime.Minutes, startTime.Seconds);
                    currentTimesArray[index] = endTime.ToString();
                }
                else if(showPostTime[index].Contains("minutes")){
                    var minutesNum = showPostTime[index].Split(' ')[0];
                    var doubleMinute = double.Parse(minutesNum);
                    var timeToAdd = TimeSpan.FromMinutes(doubleMinute);
                    startTime = startTime.Add(timeToAdd);
                    endTime = new TimeSpan(startTime.Hours,startTime.Minutes,startTime.Seconds);
                    currentTimesArray[index] = endTime.ToString();
                }
                else if(showPostTime[index].Contains("hours")){
                    var hours_num = showPostTime[index].Split(' ')[0];
                    var double_hours = double.Parse(hours_num);
                    var timeToAdd = TimeSpan.FromHours(double_hours);
                    startTime = startTime.Add(timeToAdd);
                    endTime = new TimeSpan(startTime.Hours,startTime.Minutes,startTime.Seconds);
                    currentTimesArray[index] = startTime.ToString();
                }
                else{
                    return "impossible";
                }

            }
            Array.Sort(currentTimesArray);
            return currentTimesArray[currentTimesArray.Length-1];
        }
    }
}
