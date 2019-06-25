using System;
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
            // Add your code here.
            string[] currentTime=new string[exactPostTime.Length];
            string FinalCurrentTime="";
            int indexTime,indexIncrementTime;
            
            //For impossible cases
            for(indexTime=0;indexTime<exactPostTime.Length;indexTime++)
                for(indexIncrementTime=indexTime+1;indexIncrementTime<exactPostTime.Length;indexIncrementTime++)
                    if(string.Compare(exactPostTime[indexTime],exactPostTime[indexIncrementTime])==0)
                        if(string.Compare(showPostTime[indexTime],showPostTime[indexIncrementTime])!=0)
                            return "impossible";
           
            for(indexTime=0;indexTime<exactPostTime.Length;indexTime++)
            {
                if(showPostTime[indexTime].Contains("seconds"))
                {
                    currentTime[indexTime]=exactPostTime[indexTime];
                }
                else if(showPostTime[indexTime].Contains("minutes"))
                {
                    DateTime date;
                    bool ifSuccess1=DateTime.TryParse(exactPostTime[indexTime], out date);
                    int incr1;
                    bool ifSuccess2=int.TryParse(showPostTime[indexTime].Substring(0,showPostTime[indexTime].IndexOf(" ")), out incr1);
                    DateTime cTime=date.AddMinutes(incr1);
                    currentTime[indexTime]=cTime.ToString("HH:mm:ss",CultureInfo.InvariantCulture);
                }
                else if(showPostTime[indexTime].Contains("hours"))
                {
                    DateTime date;
                    bool ifSuccess1=DateTime.TryParse(exactPostTime[indexTime], out date);
                    int incr2;
                    bool ifSuccess2=int.TryParse(showPostTime[indexTime].Substring(0,showPostTime[indexTime].IndexOf(" ")), out incr2);
                    DateTime cTime=date.AddHours(incr2);
                    currentTime[indexTime]=cTime.ToString("HH:mm:ss",CultureInfo.InvariantCulture);
                }
            }
            //For multiple solutions: CurrentTime that comes first lexicographically.
            if(currentTime.Length>1)
                    for(indexTime=0;indexTime<currentTime.Length-1;indexTime++)
                    {
                        if(string.Compare(currentTime[indexTime],currentTime[indexTime+1])!=0)
                        {
                            if(string.Compare(currentTime[indexTime],currentTime[indexTime+1])>0)
                                FinalCurrentTime=currentTime[indexTime];
                        }
                        else
                            FinalCurrentTime=currentTime[indexTime];
                    }
            else
                FinalCurrentTime=currentTime[0];

            return FinalCurrentTime;
            
        }
    }
}
/*
Last login: Fri Jun 21 21:50:34 on ttys000
Anikets-MacBook-Pro:~ aniket$ cd /Users/aniket/language-basics-02/Tavisca.Bootcamp.LanguageBasics.Exercise2
Anikets-MacBook-Pro:Tavisca.Bootcamp.LanguageBasics.Exercise2 aniket$ dotnet run
[12:12:12], [few seconds ago] => PASS
[23:23:23, 23:23:23], [59 minutes ago, 59 minutes ago] => PASS
[00:10:10, 00:10:10], [59 minutes ago, 1 hours ago] => PASS
[11:59:13, 11:13:23, 12:25:15], [few seconds ago, 46 minutes ago, 23 hours ago] => PASS

[1]+  Stopped                 dotnet run
Anikets-MacBook-Pro:Tavisca.Bootcamp.LanguageBasics.Exercise2 aniket$ 
*/