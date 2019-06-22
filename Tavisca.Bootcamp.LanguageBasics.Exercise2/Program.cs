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
            string res =GetCurrentTime(postTimes, showTimes);
            var result = res.Equals(expected) ? "PASS" : "FAIL";
            var postTimesCsv = string.Join(", ", postTimes);
            var showTimesCsv = string.Join(", ", showTimes);
            Console.WriteLine($"[{postTimesCsv}], [{showTimesCsv}] => {result}");
        }

        public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {
                        int posts= exactPostTime.Length;
            DateTime[] min = new DateTime[posts];
            DateTime[] max = new DateTime[posts];
            DateTime postTime=Convert.ToDateTime(exactPostTime[0]);;

             if(isValid(exactPostTime,showPostTime)==1)
            {
                for(int i=0; i<posts; i++)
                {
                    DateTime ptime= Convert.ToDateTime(exactPostTime[i]);
                    String[] stime = showPostTime[i].Split();
                    switch(stime[1])
                    {
                        case "seconds": min[i] = ptime;
                                        max[i]= ptime.AddSeconds(59);
                                        break;
                        case "minutes": min[i] = ptime.AddMinutes(Convert.ToInt32(stime[0]));
                                        max[i] = min[i].AddSeconds(59);
                                        break;
                        case "hours"  : min[i] = ptime.AddHours(Convert.ToInt32(stime[0]));
                                        max[i] = min[i].AddMinutes(59);
                                        max[i] = max[i].AddSeconds(59);
                                        break; 
                    }
                    
                }
                postTime= min[0];
                for(int i=0;i<posts;i++)
                {
                    //System.Console.Write(min[i]+".....");
                    if(min[i].TimeOfDay > postTime.TimeOfDay)
                        postTime=min[i];
                }

                //System.Console.WriteLine("Ans= "+postTime);
                return (postTime.ToString("HH':'mm':'ss"));
            }
            else
                return "impossible";
        
            
        }

        public static int isValid(string[] exactPostTime,string[] showPostTime)
        {
            int len = exactPostTime.Length;
            for(int i=0;i<len;i++)
            {
                for(int j=i;j<len;j++)
                {
                    if(exactPostTime[i].Equals(exactPostTime[j]))
                        if(! showPostTime[i].Equals(showPostTime[j]))
                            return 0;
                    
                }
            }
            return 1;
        }

    }
}
