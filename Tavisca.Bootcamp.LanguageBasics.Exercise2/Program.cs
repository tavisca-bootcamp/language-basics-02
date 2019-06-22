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


        /* Main Idea of the solution : 
        1) calculate the lower bound time and upper bound time for each posttime and showtime
        2) we will calculte this time in seconds.
        3) now we have a list of mintimes and maxtimes ranges...
        4) we need to find a overlap between the time ranges. 
        5) a overlap occurs if we have the (maximun(mintimes array elements) < minimum(maxtime array elements)) as TRUE
        6) if true, we return the lower bound of the range as current time
        7) else we return "impossible"
        */

        // User defined class for storing and processing time data !
        public class TimeClass{
            public string exactTime;
            public string showTime;
            public int hh;
            public int mm;
            public int ss;
            public int totalSeconds;
            public int maxTime;
            public int minTime;

            // constructor to initialize objects with exactPostTime and showTimes
            public TimeClass(string exactTime, string showTime){
                this.exactTime = exactTime;
                this.showTime = showTime;
            }
            
            // Function to process and calculate mintime ad maxtime ranges for a time element
            public void Process(){
                string[] temp1 = exactTime.Split(':');
                hh = Int32.Parse(temp1[0]);
                mm = Int32.Parse(temp1[1]);
                ss = Int32.Parse(temp1[2]);

                totalSeconds= ss + (60*mm) + (3600*hh);

                if(showTime.Contains("seconds")){
                    minTime = totalSeconds + 0;
                    maxTime = minTime + 59;
                }else if(showTime.Contains("minutes")){
                    string[] temp = showTime.Split(' ');
                    int tempMin = Int32.Parse(temp[0]);
                    minTime = totalSeconds + 60*tempMin + 0;
                    maxTime = minTime + 59;
                }else if(showTime.Contains("hours")){
                    string[] temp = showTime.Split(' ');
                    int tempHr = Int32.Parse(temp[0]);
                    minTime = totalSeconds + 3660*tempHr ;
                    maxTime =  minTime + 59*60 + 59;
                }
                // But a day can have only 86400 seconds, so we need to take mod for minTime and maxTime
                minTime = minTime % 86400;
                maxTime = maxTime % 86400;
            }

        }
        public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {
            // Add your code here.
            int minmaxTime,maxminTime;
            int hh,mm,ss; // resultant hour, minutes and seconds
            string result = ""; // result string to be returned

            //Taking input and processing that input:
            int len = exactPostTime.Length;
            TimeClass[] TT = new TimeClass[len]; // TT stands for TestCase Time !
            for (int i = 0; i < len; i++)
            {
                TT[i] = new TimeClass(exactPostTime[i], showPostTime[i]);
                TT[i].Process();   
            }
            
            // Now lets find the overlapping time cases
            maxminTime = TT[0].minTime;
            for (int i = 0; i < len; i++)
            {
                if(TT[i].minTime > maxminTime)
                    maxminTime= TT[i].minTime;
            }
            minmaxTime = TT[0].maxTime;
            for (int i = 0; i < len; i++)
            {
                if(TT[i].maxTime < minmaxTime)
                    minmaxTime= TT[i].maxTime;
            }

            
            // Now we already have the boundary conditions.
            if(maxminTime < minmaxTime){
                
                // we have a result to be returned in this case!
                // result to be considered is - maxmintime(seconds)
                // so, now we will convert maxmintime to hh:mm:ss format
                hh = maxminTime / 3600;
                hh = hh % 24;
                maxminTime = maxminTime % 3600;
                mm = maxminTime / 60;
                maxminTime = maxminTime % 60;
                ss = maxminTime;


                // Returning result in format HH:MM:SS, we need to convert single digit(9:7:5) numbers to double digit(09:07:05)
                // adding hours to result
                if(hh>9){
                     result = result + $"{hh}:";
                }else{
                    result = result + $"0{hh}:";
                }
                // adding minutes to result
                if(mm>9){
                     result = result + $"{mm}:";
                }else{
                    result = result + $"0{mm}:";
                }
                // adding seconds to result
                if(ss>9){
                     result = result + $"{ss}";
                }else{
                    result = result + $"0{ss}";
                }
                
                return result;

            }else{
                // if there is no overlapping between the mintime and maxtime time ranges
                // then there is no possible current time for the problem - hence return "impossible"
                return "impossible";
            }

            throw new NotImplementedException();
        }
    }
}
