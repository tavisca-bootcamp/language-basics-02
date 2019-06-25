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
            DateTime previousStart = DateTime.Now;  // initialized to avoid compile-time error
            DateTime previousEnd = DateTime.Now;  // initialized to avoid compile-time error
            System.Console.WriteLine("I am Ironman");

            for (int i = 0; i < exactPostTime.Length; i++) {
                
                // Get the first time interval
                if (i == 0) {
                    GetPossibleTimeInterval(exactPostTime[i], showPostTime[i], out previousStart, out previousEnd);
                }
                
                else {
                    DateTime currentStart;
                    DateTime currentEnd;
                    
                    // Find time interval for current time 
                    GetPossibleTimeInterval(exactPostTime[i], showPostTime[i], out currentStart, out currentEnd);

                    #region  Find the intersection time interval between current and previous time stamps
                        // p -> previous, c -> current, S -> start, E -> end

                        // Timeline: [pS |cS pE| cE]
                        if (currentStart >= previousStart && currentStart <= previousEnd && currentEnd >= previousEnd) {
                                previousStart = currentStart;
                                // previousEnd = previousEnd;  // Statement unnecessary
                        }
                        // Timeline: [cS |pS pE| cE]
                        else if (currentStart <= previousStart && previousEnd <= currentEnd) {
                                // previousStart = previousStart;  // Statement unnecessary
                                // previousEnd = previousEnd;  // Statement unnecessary
                        } 
                        // Timeline: [cS |pS cE| pE]
                        else if (previousStart >= currentStart && previousStart <= currentEnd && previousEnd >= currentEnd) {
                                // previousStart = previousStart;  // Statement unnecessary
                                previousEnd = currentEnd;
                        } 
                        // Timeline: [pS |cS cE| pE]
                        else if (previousStart <= currentStart && currentEnd <= previousEnd) {
                                previousStart = currentStart;
                                previousEnd = currentEnd;
                        }
                        // No intersection possible as Timeline: [pS pE cS cE] or [cS cE pS pE] 
                        else {
                            return "impossible";
                        }
                    #endregion

                }
            }

            // return lexicographically smallest string
            if (previousStart.DayOfWeek != previousEnd.DayOfWeek) {
                return "00:00:00";
            } 
            #region Constructing correct format->  hh:mm:ss
                string hr = $"{previousStart.Hour}";
                if (hr.Length == 1) hr = "0" + hr;
                string min = $"{previousStart.Minute}";
                if (min.Length == 1) min = "0" + min;
                string sec = $"{previousStart.Second}";
                if (sec.Length == 1) sec = "0" + sec;
            #endregion

            return $"{hr}:{min}:{sec}";
        }

        public static void GetPossibleTimeInterval(string exactPostTime, string showPostTime, out DateTime start, out DateTime end) {
            #region Separate hh:mm:ss
                string[] time_t = exactPostTime.Split(':');
                int[] time_t_int = new[] {int.Parse(time_t[0]), int.Parse(time_t[1]), int.Parse(time_t[2])};
            #endregion
            
            #region Convert to DateTime Object
                start = new DateTime(1, 1, 1, time_t_int[0], time_t_int[1], time_t_int[2]);
                end = new DateTime(1, 1, 1, time_t_int[0], time_t_int[1], time_t_int[2]);
            #endregion

            // Posted X time ago. Get the time separated
            int[] getTimeIncrement = Program.getTime(showPostTime);
            
            #region Add the time increment to the time
                if (getTimeIncrement[3] == 1) {
                    // X hours
                    start = start.AddHours(getTimeIncrement[0]);
                } else if (getTimeIncrement[4] == 1) {
                    // X minutes 
                    start = start.AddMinutes(getTimeIncrement[1]);
                } else {
                    // few seconds
                    // start = start;  // statement unnecessary
                }
                end += new TimeSpan(getTimeIncrement[0], getTimeIncrement[1], getTimeIncrement[2]);
            #endregion
            
            // As we are only adding hr:min:sec we don't care of their of their days if their start and end
            // are on the same day.
            #region If both on day 2, then change their day to day 1
                if (start.Day == 2 && end.Day == 2) {
                    start = new DateTime (1, 1, 1, start.Hour, start.Minute, start.Second); // day 1
                    end = new DateTime (1, 1, 1, end.Hour, end.Minute, end.Second); // day 1
                }
            #endregion
        }

        // returns: {hr, min, sec, isPostedHoursAgo, isPostedMinutesAgo, isPostedSecondsAgo}
        public static int[] getTime(string post) {
            var time_ = post.Split(' ');

            if (time_[0] == "few") return new[] {0, 0, 59, 0, 0, 1};  // Posted: few seconds ago

            if (time_[1] == "minutes") return new[] {0, int.Parse(time_[0]), 59, 0, 1, 0};  // X minutes ago

            return new[] {int.Parse(time_[0]), 59, 0, 1, 0, 0};  // X hours ago
        }
    }
}
