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
            String[] currentTime=new String[exactPostTime.Length];
            String CurrentTime="";
            int i,j;
            
            //For impossible cases
            for(i=0;i<exactPostTime.Length;i++)
                for(j=i+1;j<exactPostTime.Length;j++)
                    if(string.Compare(exactPostTime[i],exactPostTime[j])==0)
                        if(string.Compare(showPostTime[i],showPostTime[j])!=0)
                            return "impossible";
           
            int  incr=0;
            for(i=0;i<exactPostTime.Length;i++)
            {
                if(showPostTime[i].Contains("seconds"))
                {
                    currentTime[i]=exactPostTime[i];
                }
                else if(showPostTime[i].Contains("minutes"))
                {
                    DateTime date=DateTime.Parse(exactPostTime[i]);
                    incr=int.Parse(showPostTime[i].Substring(0,showPostTime[i].IndexOf(" ")));
                    DateTime cTime=date.AddMinutes(incr);
                    currentTime[i]=cTime.ToString("HH:mm:ss",CultureInfo.InvariantCulture);
                }
                else if(showPostTime[i].Contains("hours"))
                {
                    DateTime date=DateTime.Parse(exactPostTime[i]);
                    incr=int.Parse(showPostTime[i].Substring(0,showPostTime[i].IndexOf(" ")));
                    DateTime cTime=date.AddHours(incr);
                    currentTime[i]=cTime.ToString("HH:mm:ss",CultureInfo.InvariantCulture);
                }
            }
            //For multiple solutions: CurrentTime that comes first lexicographically.
            if(currentTime.Length>1)
                    for(i=0;i<currentTime.Length-1;i++)
                    {
                        if(string.Compare(currentTime[i],currentTime[i+1])!=0)
                        {
                            if(string.Compare(currentTime[i],currentTime[i+1])>0)
                                CurrentTime=currentTime[i];
                        }
                        else
                            CurrentTime=currentTime[i];
                    }
            else
                CurrentTime=currentTime[0];

            return CurrentTime;
            throw new NotImplementedException();
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