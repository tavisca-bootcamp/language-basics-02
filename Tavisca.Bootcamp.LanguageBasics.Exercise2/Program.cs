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
            int n = exactPostTime.Length;

            for(int i=0;i<n;i++)  {
                for(int j=i+1;j<n;j++)  {
                    if(exactPostTime[i] == exactPostTime[j])  {
                        if(showPostTime[i] != showPostTime[j])
                            return "impossible";
                    }
                }
            }

            String currentTime = null;

    	    for(int i=0;i<n;i++)  {
                String tempCurrTime = null;
                String[] timeSplit = exactPostTime[i].Split(":");

                DateTime date = new DateTime(2019, 6, 24, int.Parse(timeSplit[0]), int.Parse(timeSplit[1]), int.Parse(timeSplit[2]), 24);

                if(showPostTime[i].Contains("seconds"))  {
                    tempCurrTime = date.ToString("HH:mm:ss");
                }

                else if(showPostTime[i].Contains("minutes"))  {
                    int mins  = int.Parse(showPostTime[i].Split(" ")[0]);
                    date = date.AddMinutes(mins);
                    tempCurrTime = date.ToString("HH:mm:ss");
                }

                else  {
                    int h = int.Parse(showPostTime[i].Split(" " )[0]);
                    date = date.AddHours(h);
                    tempCurrTime = date.ToString("HH:mm:ss");
                }
                
                if(currentTime != null)  {
                    if(currentTime.CompareTo(tempCurrTime) < 0)  {
                        currentTime = tempCurrTime;
                    }
                }
                else  {
                    currentTime = tempCurrTime;
                }
            }

            return currentTime;

            throw new NotImplementedException();
        }
    }
}
