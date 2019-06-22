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
            //Console.ReadKey(true);
        }

        private static void Test(string[] postTimes, string[] showTimes, string expected)
        {
            var result = GetCurrentTime(postTimes, showTimes).Equals(expected) ? "PASS" : "FAIL";
            var postTimesCsv = string.Join(", ", postTimes);
            var showTimesCsv = string.Join(", ", showTimes);
            Console.WriteLine($"[{postTimesCsv}], [{showTimesCsv}] => {result}");
        }

        public class TimeClass{
            public string exactTime;
            public string showTime;
            public int hh;
            public int mm;
            public int ss;
            public int totalSeconds;
            public int maxTime;
            public int minTime;

            public TimeClass(string exactTime, string showTime){
                this.exactTime = exactTime;
                this.showTime = showTime;
            }

            public void Process(){
                string[] temp1 = exactTime.Split(':');
                hh = Int32.Parse(temp1[0]);
                mm = Int32.Parse(temp1[1]);
                ss = Int32.Parse(temp1[2]);

                totalSeconds= ss + (60*mm) + (60*60*hh);

                if(showTime.Contains("seconds")){
                    minTime = totalSeconds + 0;
                    maxTime = totalSeconds + 59;
                }else if(showTime.Contains("minutes")){
                    string[] temp = showTime.Split(' ');
                    int tempMin = Int32.Parse(temp[0]);
                    minTime = totalSeconds + 60*tempMin + 0;
                    maxTime = totalSeconds + minTime + 59;
                }else if(showTime.Contains("hours")){
                    string[] temp = showTime.Split(' ');
                    int tempHr = Int32.Parse(temp[0]);
                    minTime = totalSeconds + 3660*tempHr + 0 + 0;
                    maxTime = totalSeconds + minTime + 59*60 + 59;
                }
            }

        }
        public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {
            // Add your code here.
            //Taking input and processing that input:
            int len = exactPostTime.Length;
            TimeClass[] TT = new TimeClass[len]; // TT stands for TestCase Time !
            for (int i = 0; i < len; i++)
            {
                TT[i] = new TimeClass(exactPostTime[i], showPostTime[i]);
                TT[i].Process();   
            }
            // Printing input data for checking if input was correct :
            for (int i = 0; i < len; i++)
            {
                Console.WriteLine($"{TT[i].exactTime} {TT[i]. showTime} {TT[i].hh}|{TT[i].mm}|{TT[i].ss}  {TT[i].minTime}|{TT[i].maxTime} Totalsec= {TT[i].totalSeconds}");
            }
            return "12:12:12"; 
            throw new NotImplementedException();
        }
    }
}
