using System;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class ForumPostEasy
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


        /*
            Custom Data type to store the time in string as well as integer format, and to perform
            reusable operations on the given data such as convertMinMaxTime()
        */
        public class MyTime
        {
            public string exactTime;
            public int hour;
            public int minutes;
            public int seconds;
            public int minhour;
            public int minminutes;
            public int minseconds;
            public int maxhour;
            public int maxminutes;
            public int maxseconds;
            public string showTime;
            public string minRangeCurrentTime;
            public string maxRangeCurrentTime;
            public MyTime(string exactTime, string showTime)
            {
                this.exactTime = exactTime;
                this.showTime = showTime;
                this.convert();
            }
            void convert()
            {
                string[] exTime = exactTime.Split(":");
                hour = Int32.Parse(exTime[0]);
                minutes = Int32.Parse(exTime[1]);
                seconds = Int32.Parse(exTime[2]);
            }
            public void convertMinMaxTime()
            {
                string[] maxTime = maxRangeCurrentTime.Split(":");
                maxhour = Int32.Parse(maxTime[0]);
                maxminutes = Int32.Parse(maxTime[1]);
                maxseconds = Int32.Parse(maxTime[2]);
                string[] minTime = minRangeCurrentTime.Split(":");
                minhour = Int32.Parse(minTime[0]);
                minminutes = Int32.Parse(minTime[1]);
                minseconds = Int32.Parse(minTime[2]);
            }
        }

        public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {
            /*
                Idea :-
                1.) Calculate estimated current time based on the exactPostTime and showPostTime for each i in string[].
                2.) Calculate the range of each possible time using showPostTime range and the estimated current time
                    obtained in step 1.
                3.) Traverse each range and find the innermost possible subset of the time range.
                4.) Two cases arise :
                    a.) If innermost time subset is not possible the simply return impossible as there are no overlappings.
                    b.) If innermost time subset is possible then return the least valid time in the subset.

            */

            MyTime[] myTime = new MyTime[exactPostTime.Length];
            for (int i = 0; i < myTime.Length; i++)
            {
                myTime[i] = new MyTime(exactPostTime[i], showPostTime[i]);

                string secondsago = "0";
                string minutesago = "0";
                string hoursago = "0";
                int rangeSeconds = 59;
                int rangeMinutes = 0;

                if (showPostTime[i].Contains("minute"))
                {
                    minutesago = myTime[i].showTime.Split(" ")[0];
                }
                else if (myTime[i].showTime.Contains("hour"))
                {
                    hoursago = myTime[i].showTime.Split(" ")[0];                  
                    rangeMinutes = 59;
                }
                int ss = myTime[i].seconds + Int32.Parse(secondsago);
                int mm = myTime[i].minutes + Int32.Parse(minutesago) + ss / 60;
                int hh = myTime[i].hour + Int32.Parse(hoursago) + mm / 60;
                ss = ss % 60;
                mm = mm % 60;
                hh = hh % 24;
                int ss1 = ss + rangeSeconds;
                int mm1 = mm + rangeMinutes + ss1 / 60;
                int hh1 = hh + mm1 / 60;
                ss1 = ss1 % 60;
                mm1 = mm1 % 60;
                hh1 = hh1 % 24;
                myTime[i].minRangeCurrentTime = $"{hh:D2}:{mm:D2}:{ss:D2}";
                myTime[i].maxRangeCurrentTime = $"{hh1:D2}:{mm1:D2}:{ss1:D2}";
            }



            MyTime[] myRange = findMinMaxTime(myTime);
            MyTime minRange = myRange[0];
            MyTime maxRange = myRange[1];
            
            /*
                Check whether there are any overlapping or not.
                If not - return "impossible"
                else - return least valid time.
            */
            if (maxRange.hour - minRange.hour > 0)
            {
                return $"{minRange.hour:D2}:{minRange.minutes :D2}:{minRange.seconds:D2}";
            }
            else if (maxRange.hour - minRange.hour < 0)
            {
                return "impossible";
            }
            else
            {
                if (maxRange.minutes - minRange.minutes > 0)
                {
                    return $"{minRange.hour:D2}:{minRange.minutes :D2}:{minRange.seconds:D2}";
                }
                else if (maxRange.minutes - minRange.minutes < 0)
                {
                    return "impossible";
                }
                else
                {
                    if (maxRange.seconds - minRange.seconds >= 0)
                    {
                        return $"{minRange.hour:D2}:{minRange.minutes :D2}:{minRange.seconds:D2}";
                    }
                }
            }
            return "impossible";
        }

        /*
            Utility function to find the innermost subset time range [minValidTime, maxValidTime]. 
        */
        static MyTime[] findMinMaxTime(MyTime[] myTime)
        {
            int maxHour = -999;
            int maxMinutes = -999;
            int maxSeconds = -999;
            int minHour = 999;
            int minMinutes = 999;
            int minSeconds = 999;
            for(int k=0; k<3; k++){
                for(int i=0; i<myTime.Length; i++){
                    myTime[i].convertMinMaxTime();
                    if(k == 0){
                        maxHour = Math.Max(maxHour, myTime[i].minhour);
                        minHour = Math.Min(minHour, myTime[i].maxhour);
                    }
                    if(k == 1){
                        if(maxHour == myTime[i].minhour){
                            maxMinutes = Math.Max(maxMinutes, myTime[i].minminutes);
                        }
                        if(minHour == myTime[i].maxhour){
                            minMinutes = Math.Min(minMinutes, myTime[i].maxminutes);
                        }
                    }if(k == 2){
                        if(maxHour == myTime[i].minhour && maxMinutes == myTime[i].minminutes){
                            maxSeconds = Math.Max(maxSeconds, myTime[i].minseconds);
                        }
                        if(minHour == myTime[i].maxhour && minMinutes == myTime[i].maxminutes){
                            minSeconds = Math.Min(minSeconds, myTime[i].maxseconds);
                        }
                    }
                }
            }
            MyTime maxRange = new MyTime($"{minHour:D2}:{minMinutes:D2}:{minSeconds:D2}","");
            MyTime minRange = new MyTime($"{maxHour:D2}:{maxMinutes:D2}:{maxSeconds:D2}","");
            return new MyTime[2]{minRange,maxRange};
        }
    }
}
