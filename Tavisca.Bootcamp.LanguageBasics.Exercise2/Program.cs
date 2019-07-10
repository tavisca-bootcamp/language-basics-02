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

            if(!isPossible(exactPostTime, showPostTime)) return IMPOSSIBLE;

            string[] currentTime = new string[totaltime];

            for(var i=0; i<totaltime; i++){
                int hours=0, minutes=0, seconds=0;
                getHoursMinutesSeconds(exactPostTime[i], ref hours, ref minutes, ref seconds);

                if(showPostTime[i].Contains("seconds"))
                    currentTime[i] = exactPostTime[i];
                else if(showPostTime[i].Contains("minutes"))
                    currentTime[i] = currentTimeAddMinutes(showPostTime[i], hours, minutes, seconds);
                else if(showPostTime[i].Contains("hours"))
                    currentTime[i] = currentTimeAddHours(showPostTime[i], hours, minutes, seconds);
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

        public static bool isPossible(string[] exactPostTime, string[] showPostTime){
            for(var i=0; i<exactPostTime.Length; i++){
                for(var j=0; j<exactPostTime.Length; j++){
                    if(i==j) continue;
                    if(exactPostTime[i] == exactPostTime[j] && showPostTime[i] != showPostTime[j])
                        return false;
                }
            }
            return true;
        }

        public static void getHoursMinutesSeconds(string exactPostTime, ref int hours, ref int minutes, ref int seconds){
            hours = Convert.ToInt32(exactPostTime.Split(":")[0]);
            minutes = Convert.ToInt32(exactPostTime.Split(":")[1]);
            seconds = Convert.ToInt32(exactPostTime.Split(":")[2]);
        }

        public static string currentTimeAddMinutes(string showPostTime, int hours, int minutes, int seconds){
            DateTime timeAndDate = new DateTime(1900, 1, 1, hours, minutes, seconds);
            int showPostTimeMinutes = Convert.ToInt32(showPostTime.Split(" ")[0]);
            TimeSpan timeSpan = new TimeSpan(0, showPostTimeMinutes, 0);
            return timeAndDate.Add(timeSpan).ToString().Split(" ")[1];
        }

        public static string currentTimeAddHours(string showPostTime, int hours, int minutes, int seconds){
            DateTime timeAndDate = new DateTime(1900, 1, 1, hours, minutes, seconds);
            int showPostTimeHours = Convert.ToInt32(showPostTime.Split(" ")[0]);
            TimeSpan timeSpan = new TimeSpan(showPostTimeHours, 0, 0);
            return timeAndDate.Add(timeSpan).ToString().Split(" ")[1];
        }
    }
}
