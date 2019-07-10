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

        public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime){
            const string IMPOSSIBLE = "impossible";
            int totaltime = exactPostTime.Length;

            for(var i=0; i<totaltime; i++){
                for(var j=0; j<totaltime; j++){
                    if(i==j) continue;
                    if(exactPostTime[i] == exactPostTime[j] && showPostTime[i] != showPostTime[j])
                        return IMPOSSIBLE;
                }
            }

            string[] currentTime = new string[totaltime];

            for(var i=0; i<totaltime; i++){
                int hours = Convert.ToInt32(exactPostTime[i].Split(":")[0]);
                int minutes = Convert.ToInt32(exactPostTime[i].Split(":")[1]);
                int seconds = Convert.ToInt32(exactPostTime[i].Split(":")[2]);

                DateTime timeAndDate = new DateTime(1900, 1, 1, hours, minutes, seconds);

                if(showPostTime[i].Contains("seconds"))
                    currentTime[i] = exactPostTime[i];
                else if(showPostTime[i].Contains("minutes")){
                    int showPostTimeMinutes = Convert.ToInt32(showPostTime[i].Split(" ")[0]);
                    TimeSpan timeSpan = new TimeSpan(0, showPostTimeMinutes, 0);
                    currentTime[i] = timeAndDate.Add(timeSpan).ToString().Split(" ")[1];
                }
                else if(showPostTime[i].Contains("hours")){
                    int showPostTimeHours = Convert.ToInt32(showPostTime[i].Split(" ")[0]);
                    TimeSpan timeSpan = new TimeSpan(showPostTimeHours ,0, 0);
                    currentTime[i] = timeAndDate.Add(timeSpan).ToString().Split(" ")[1];
                }
            }

            Array.Sort(currentTime);
            
            try{
                return currentTime[totaltime-1];
            }
            catch(Exception e){
                Console.WriteLine(e.ToString());
                Console.WriteLine("Input of length : 0");
                return IMPOSSIBLE;
            }
        }
    }
}
