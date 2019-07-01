using System;
using System.Text.RegularExpressions;
using System.Globalization;

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
            //Console.ReadKey(true);
        }

        private static void Test(string[] postTimes, string[] showTimes, string expected)
        {
            var result = GetCurrentTime(postTimes, showTimes).Equals(expected) ? "PASS" : "FAIL";
            var postTimesCsv = string.Join(", ", postTimes);
            var showTimesCsv = string.Join(", ", showTimes);
            Console.WriteLine($"[{postTimesCsv}], [{showTimesCsv}] => {result}");
        }


        /* Main Idea of the solution : 
        1) calculate the lower bound time and upper bound time for each posttime and showtime
        2) we will calculate this time in seconds.
        3) now we have a list of mintimes and maxtimes ranges...
        4) we need to find a overlap between the time ranges. 
        5) a overlap occurs if we have the (maximun(mintimes array elements) < minimum(maxtime array elements)) as TRUE
        6) if true, we return the lower bound of the range as current time
        7) else we return "impossible"
        */

        const int TotalSecondsInADay = 86400;
        const int MaximumSeconds = 59;
        const int MinimumSeconds = 0;
        const int SecondsInAMinute = 60;
        const int SecondsInAnHour = 3600;

        // User defined class for storing and processing time data !
        public class TimeClass
        {
            public string ExactTime;
            public string ShowTime;
            public int hh;
            public int mm;
            public int ss;
            public int TotalSeconds;
            public int MaxTime;
            public int MinTime;

            
            public TimeClass(string exactTime, string showTime)
            {
                // constructor to initialize objects with exactPostTime and showTimes
                this.ExactTime = exactTime;
                this.ShowTime = showTime;
            }
            
            
            public void Process()
            {
                // Function to process and calculate mintime ad maxtime ranges for a time element
                Regex pattern = new Regex(@"(?<num0>.*):(?<num1>.*):(?<num2>.*)");
                Match match = pattern.Match(this.ExactTime);
                this.hh = Int32.Parse(match.Groups["num0"].Value);
                this.mm = Int32.Parse(match.Groups["num1"].Value);
                this.ss = Int32.Parse(match.Groups["num2"].Value);

                /* 
                string[] timeStringParse = this.ExactTime.Split(':');
                this.hh = Int32.Parse(timeStringParse[0]);
                this.mm = Int32.Parse(timeStringParse[1]);
                this.ss = Int32.Parse(timeStringParse[2]);
                */

                TotalSeconds= ss + (SecondsInAMinute * mm) + (SecondsInAnHour * hh);

                if(this.ShowTime.Contains("seconds")){
                    MinTime = TotalSeconds + MinimumSeconds;
                    MaxTime = MinTime + MaximumSeconds;
                }else if(this.ShowTime.Contains("minutes")){
                    string[] stringParser = this.ShowTime.Split(' ');
                    int minutes = Int32.Parse(stringParser[0]);
                    MinTime = TotalSeconds + SecondsInAMinute*minutes + MinimumSeconds;
                    MaxTime = MinTime + MaximumSeconds;
                }else if(this.ShowTime.Contains("hours")){
                    string[] stringParser = this.ShowTime.Split(' ');
                    int hours = Int32.Parse(stringParser[0]);
                    MinTime = TotalSeconds + SecondsInAnHour*hours ;
                    MaxTime =  MinTime + 59*60 + MaximumSeconds;
                }
                // But a day can have only 86400 seconds, so we need to take mod for minTime and maxTime
                MinTime = MinTime % TotalSecondsInADay;
                MaxTime = MaxTime % TotalSecondsInADay;
            }

        }
        public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {
            int minMaxTime,maxMinTime;
            //Taking input and processing that input:
            var TT = new TimeClass[exactPostTime.Length]; // TT stands for TestCase Time !
            for (int i = 0; i < exactPostTime.Length; i++)
            {
                TT[i] = new TimeClass(exactPostTime[i], showPostTime[i]);
                TT[i].Process();   
            }
            // Now lets find the overlapping time cases
            maxMinTime = TT[0].MinTime;
            for (int i = 0; i < exactPostTime.Length; i++)
            {
                if(TT[i].MinTime > maxMinTime)
                    maxMinTime= TT[i].MinTime;
            }
            minMaxTime = TT[0].MaxTime;
            for (int i = 0; i < exactPostTime.Length; i++)
            {
                if(TT[i].MaxTime < minMaxTime)
                    minMaxTime= TT[i].MaxTime;
            }

            // Now we already have the boundary conditions.
            var result = MakeResultString(maxMinTime, minMaxTime);
            return result;
        }

        private static String MakeResultString(int maxMinTime, int minMaxTime)
        {
            int hh,mm,ss; // resultant hour, minutes and seconds
            var result = "impossible"; // result string to be returned

            if(maxMinTime < minMaxTime)
            {   // we have a result to be returned in this case!
                // result to be considered is - maxmintime(seconds)
                // so, now we will convert maxmintime to hh:mm:ss format
                hh = maxMinTime / SecondsInAnHour;
                hh = hh % 24;
                maxMinTime = maxMinTime % SecondsInAnHour;
                mm = maxMinTime / SecondsInAMinute;
                maxMinTime = maxMinTime % SecondsInAMinute;
                ss = maxMinTime;

                // Returning result in format HH:MM:SS        
                DateTime date1 = new DateTime(2019,07,01,hh,mm,ss);
                CultureInfo ci = CultureInfo.InvariantCulture;
                result = date1.ToString("HH:mm:ss",ci);
            }
            return result;

        }
    }
}
