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
            int[] previousStart = null;
            int[] previousEnd = null;

            for (int i = 0; i < exactPostTime.Length; i++) {
                DateTime prevDateStart = DateTime.Now,
                         prevDateEnd = DateTime.Now,
                        currDateStart = DateTime.Now, 
                        currDateEnd = DateTime.Now;
                // Find time intersection of current and previous
                if (previousStart == null && previousEnd == null) {
                    string[] currentStartSplit = exactPostTime[i].Split(':');
                    previousStart = new[] {int.Parse(currentStartSplit[0]), 
                                           int.Parse(currentStartSplit[1]),
                                           int.Parse(currentStartSplit[2])};

                    int[] getTime_ = Program.getTime(showPostTime[i]);
                    
                    if (getTime_[3] == 1) {
                        previousStart[0] = (getTime_[0] + previousStart[0]) % 24;
                        getTime_[0] = previousStart[0];
                        getTime_[1] = previousStart[1];
                        getTime_[2] = previousStart[2];

                        getTime_[2] = (getTime_[2] + 59);
                        if (getTime_[2] >= 60) getTime_[1] += 1;
                        getTime_[2] %= 60;

                        getTime_[1] = (getTime_[1] + 59);
                        if (getTime_[1] >= 60) getTime_[0] += 1;
                        getTime_[1] %= 60;
                        getTime_[0] %= 24;
                        
                        
                    } else if (getTime_[4] == 1) {

                        previousStart[1] = (getTime_[1] + previousStart[1]);
                        if (previousStart[1] >= 60) previousStart[0] = (previousStart[0] + 1) % 24;
                        previousStart[1] %= 60;
                        getTime_[1] = previousStart[1];
                        getTime_[0] = previousStart[0];
                        getTime_[2] = previousStart[2];
                        
                        getTime_[2] = (getTime_[2] + 59);
                        if (getTime_[2] >= 60) getTime_[1] += 1;
                        getTime_[2] %= 60;

                        if (getTime_[1] >= 60) getTime_[0] += 1;
                        getTime_[1] %= 60;
                        getTime_[0] %= 24;
                        
                    } else {
                        getTime_[2] = (getTime_[2] + previousStart[2]);
                        getTime_[1] = previousStart[1];
                        getTime_[0] = previousStart[0];

                        if (getTime_[2] >= 60) getTime_[1] += 1;
                        getTime_[2] %= 60;

                        if (getTime_[1] >= 60) getTime_[0] += 1;
                        getTime_[1] %= 60;
                        getTime_[0] %= 24;
                    }
                    previousEnd = new[] {getTime_[0], getTime_[1], getTime_[2]};
                    continue;
                }
                // else, update the previous to be intersection
                else {
                    string[] currentStartSplit = exactPostTime[i].Split(':');
                    int[] currentStart = new[] {int.Parse(currentStartSplit[0]),
                                                int.Parse(currentStartSplit[1]),
                                                int.Parse(currentStartSplit[2])};

                    int[] getTime_ = Program.getTime(showPostTime[i]);
                    
                    if (getTime_[3] == 1) {
                        currentStart[0] = (getTime_[0] + currentStart[0]) % 24;
                        getTime_[0] = currentStart[0];
                        getTime_[1] = currentStart[1];
                        getTime_[2] = currentStart[2];

                        getTime_[2] = (getTime_[2] + 59);
                        if (getTime_[2] >= 60) getTime_[1] += 1;
                        getTime_[2] %= 60;

                        getTime_[1] = (getTime_[1] + 59);
                        if (getTime_[1] >= 60) getTime_[0] += 1;
                        getTime_[1] %= 60;
                        getTime_[0] %= 24;
                        
                    } else if (getTime_[4] == 1) {

                        currentStart[1] = (getTime_[1] + currentStart[1]);
                        if (currentStart[1] >= 60) currentStart[0] = (currentStart[0] + 1) % 24;
                        currentStart[1] %= 60;
                        getTime_[1] = currentStart[1];
                        getTime_[0] = currentStart[0];
                        getTime_[2] = currentStart[2];
                        
                        getTime_[2] = (getTime_[2] + 59);
                        if (getTime_[2] >= 60) getTime_[1] += 1;
                        getTime_[2] %= 60;

                        if (getTime_[1] >= 60) getTime_[0] += 1;
                        getTime_[1] %= 60;
                        getTime_[0] %= 24;
                    } else {
                        getTime_[2] = (getTime_[2] + currentStart[2]);
                        getTime_[1] = currentStart[1];
                        getTime_[0] = currentStart[0];

                        if (getTime_[2] >= 60) getTime_[1] += 1;
                        getTime_[2] %= 60;

                        if (getTime_[1] >= 60) getTime_[0] += 1;
                        getTime_[1] %= 60;
                        getTime_[0] %= 24;
                    }
                    int[] currentEnd = new[] {getTime_[0], getTime_[1], getTime_[2]};

                    prevDateStart = new DateTime(1, 1, 1, previousStart[0], previousStart[1], previousStart[2]);
                    currDateStart = new DateTime(1, 1, 1, currentStart[0], currentStart[1], currentStart[2]);

                    if (previousStart[0] <= previousEnd[0]) {
                        prevDateEnd = new DateTime(1, 1, 1, previousEnd[0], previousEnd[1], previousEnd[2]);
                    } else {
                        prevDateEnd = new DateTime(1, 1, 2, previousEnd[0], previousEnd[1], previousEnd[2]);
                    }

                    if (currentStart[0] <= currentEnd[0]) {
                        currDateEnd = new DateTime(1, 1, 1, currentEnd[0], currentEnd[1], currentEnd[2]);
                    } else {
                        currDateEnd = new DateTime(1, 1, 2, currentEnd[0], currentEnd[1], currentEnd[2]);
                    }

                    if (DateTime.Compare(currDateStart, prevDateStart) >= 0 &&
                        DateTime.Compare(currDateStart, prevDateEnd) <= 0 &&
                        DateTime.Compare(currDateEnd, prevDateEnd) >=0) {
                            previousStart = new[] {currDateStart.Hour, currDateStart.Minute, currDateStart.Second};
                            previousEnd = new[] {prevDateEnd.Hour, prevDateEnd.Minute, prevDateEnd.Second};
                    } else if (DateTime.Compare(currDateStart, prevDateStart) <= 0 &&
                                DateTime.Compare(prevDateEnd, currDateEnd) <=0 ) {
                            previousStart = new[] {prevDateStart.Hour, prevDateStart.Minute, prevDateStart.Second};
                            previousEnd = new[] {prevDateEnd.Hour, prevDateEnd.Minute, prevDateEnd.Second};
                    } else if (DateTime.Compare(prevDateStart, currDateStart) >= 0 &&
                        DateTime.Compare(prevDateStart, currDateEnd) <= 0 &&
                        DateTime.Compare(prevDateEnd, currDateEnd) >= 0) {
                            previousStart = new[] {prevDateStart.Hour, prevDateStart.Minute, prevDateStart.Second};
                            previousEnd = new[] {currDateEnd.Hour, currDateEnd.Minute, currDateEnd.Second};
                    } else if (DateTime.Compare(prevDateStart, currDateStart) <= 0 &&
                                DateTime.Compare(currDateEnd, prevDateEnd) <= 0) {
                            previousStart = new[] {currDateStart.Hour, currDateStart.Minute, currDateStart.Second};
                            previousEnd = new[] {currDateEnd.Hour, currDateEnd.Minute, currDateEnd.Second};
                    } else {
                        return "impossible";
                    }
                }
            }

            // return lexicographically smallest string
            if (previousEnd[0] < previousStart[0]) {
                return "00:00:00";
            } 
            string hr = $"{previousStart[0]}";
            if (hr.Length == 1) hr = "0" + hr;
            string min = $"{previousStart[1]}";
            if (min.Length == 1) min = "0" + min;
            string sec = $"{previousStart[2]}";
            if (sec.Length == 1) sec = "0" + sec;
            return $"{hr}:{min}:{sec}";
        }

        public static int[] getTime(string post) {
            var time_ = post.Split(' ');

            int duration;
            if (time_[0].Length == 3) {
                duration = 59;
            } else {
                duration = int.Parse(time_[0]);
            }

            // 3rd index: posted hours ago?
            // 4rd index: posted minutes ago?
            // 5rd index: posted seconds ago?
            if (post.IndexOf("seconds") != -1) {
                return new[] {0, 0, duration, 0, 0, 1};
            } else if (post.IndexOf("minutes") != -1) {
                return new[] {0, duration, 0, 0, 1, 0};
            } else {
                return new[] {duration, 0, 0, 1, 0, 0};
            }
        }
    }
}
