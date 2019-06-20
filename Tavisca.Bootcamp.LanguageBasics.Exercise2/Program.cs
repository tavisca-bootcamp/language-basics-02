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

        public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {
            // Add your code here.
            DateTime PostTime, CurrentTime, result=DateTime.Parse("00:00:00");
            String ShowTime;

            for(int i=0; i<exactPostTime.Length-1; i++)
            {
                for(int j=i+1; j<exactPostTime.Length; j++)
                {
                    if(exactPostTime[i]==exactPostTime[j])
                    {
                        if(showPostTime[i]!=showPostTime[j])
                        {
                            return "impossible";
                        }
                    }
                }
            }

            for(int i=0; i<exactPostTime.Length; i++)
            {
                PostTime = DateTime.Parse(exactPostTime[i]);
                CurrentTime = PostTime;
                ShowTime = showPostTime[i];
                string[] split_ShowTime = ShowTime.Split(' ');

                if(split_ShowTime[1].Equals("seconds"))
                {
                    CurrentTime = PostTime;
                }
                else if(split_ShowTime[1].Equals("minutes"))
                {
                    CurrentTime = PostTime.AddMinutes(double.Parse(split_ShowTime[0]));
                }
                else if(split_ShowTime[1].Equals("hours"))
                {
                    CurrentTime = PostTime.AddHours(double.Parse(split_ShowTime[0]));
                }

                if(DateTime.Compare(CurrentTime.Date,PostTime.Date) == 1)
                {
                   CurrentTime = CurrentTime.Add(new TimeSpan(-24, 00, 00));
                }

                if(i==0)
                {
                    result = CurrentTime;
                }

                if(TimeSpan.Compare(CurrentTime.TimeOfDay,result.TimeOfDay) > -1)
                {
                    result = CurrentTime;
                }
            }
            return result.ToLongTimeString();
            throw new NotImplementedException();
        }
    }
}
