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

            String[] result = new string[n];

    	    for(int i=0;i<n;i++)  {
                String[] timeSplit = exactPostTime[i].Split(":");

                //int hour = int.Parse(timeSplit[0]);
                //int minute = int.Parse(timeSplit[1]);
                //int second = int.Parse(timeSplit[2]);

                DateTime date = new DateTime(2019, 6, 24, int.Parse(timeSplit[0]), int.Parse(timeSplit[1]), int.Parse(timeSplit[2]), 24);

                if(showPostTime[i].Contains("seconds"))  {
                    result[i] = exactPostTime[i];
                }

                else if(showPostTime[i].Contains("minutes"))  {
                    int mins  = int.Parse(showPostTime[i].Split(" ")[0]);
                    date = date.AddMinutes(mins);
                    result[i] = date.ToString("HH:mm:ss");
                }

                else  {
                    int h = int.Parse(showPostTime[i].Split(" " )[0]);
                    date = date.AddHours(h);
                    result[i] = date.ToString("HH:mm:ss");
                }
            }

            Array.Sort(result);

            return result[n-1];

            throw new NotImplementedException();
        }
    }
}
