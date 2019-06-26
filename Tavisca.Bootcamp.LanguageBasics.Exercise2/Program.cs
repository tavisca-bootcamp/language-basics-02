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
           string result= "";
            //condition if only one exactPostTime is given
            if(exactPostTime.Length == 1)
            {
                result = exactPostTime[0];
                return result;
            }
            //
            for(int i = 0; i < exactPostTime.Length; i++)
            {
                for(int j=0; j<exactPostTime.Length; j++)
                {
                    if((exactPostTime[i] == exactPostTime[j]) && (showPostTime[i] != showPostTime[j]))
                    {
                        return "impossible";
                    }

                }
                var exactHour = exactPostTime[i].Split(":")[0];
                var HH = int.Parse(exactHour);
                var exactMinute = exactPostTime[i].Split(":")[1];         
                var MM = int.Parse(exactMinute);
                var exactSecond = exactPostTime[i].Split(":")[2];
                var SS = int.Parse(exactSecond);
                // if showPostTime contain hour then the total number of hour ago is added to HH which shows current hour 
                if(showPostTime[i].Contains("hours"))
                {
                    var shownHour = showPostTime[i].Split(" ")[0];
                    HH += int.Parse(shownHour);
                    if(HH > 24)
                    {
                        HH -= 24;
                    }
                }
                // if showPostTime contain minutes then the total number of minutes ago is added to MM which shows current minute 
                if(showPostTime[i].Contains("minutes"))
                {
                    var shownMinute = showPostTime[i].Split(" ")[0];
                    MM += int.Parse(shownMinute);
                    if(MM > 59)// if current minute is greater tha 59 then hour is increased buy one 
                    {
                        MM -= 60;
                        HH++;
                    }
                    if(HH == 24)//value of HH cannot be 24
                        HH = 0;

                    HH.ToString();
                    MM.ToString();
                    SS.ToString();

                    if(HH.Equals(0))
                        result = "00" + ":" + MM + ":" + SS;
                    else if(MM.Equals(0))
                        result = HH + ":" + "00" + ":" + SS;
                    else if(SS.Equals(0))
                        result = HH + ":" + MM + ":" + "00";
                    else
                        result = HH + ":" + MM + ":" + SS;
                }
            }
            return result;
        }
    }
}
