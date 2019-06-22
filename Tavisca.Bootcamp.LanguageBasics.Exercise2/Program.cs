using System;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    class Program
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
            /* 
             try to check if two exactPostTime time is same but showPostTime is different
            */
            for (int i = 0; i < exactPostTime.Length-1; i++)
                 if (exactPostTime[i] == exactPostTime[i+1] && showPostTime[i] != showPostTime[i+1])
                            return "impossible";

            /*
            Function2 Add all of exactPostTime and showPostTime  by index and return maximum
             */
            string lastcheck = Function2(exactPostTime, showPostTime);
            return lastcheck; 

            throw new Exception();
        }

        /*
        Function2  Function
        */
        public static string Function2(string[] exactPostTime, string[] showPostTime)
        { 
            string[] finalCheck = new string[exactPostTime.Length];

            for (int j = 0; j < exactPostTime.Length; j++) 
            {
                 TimeSpan PostTime =  TimeSpan.Parse(exactPostTime[j]);
                 string[] showTime =   showPostTime[j].Split(null);
                  
                
                        if(showTime[1]=="hours")
                         {
                              TimeSpan newTime = new TimeSpan(Convert.ToInt16(showTime[0]), 0,0); 
                              PostTime = PostTime+newTime;
                              string[] PostTime_toString =PostTime.ToString().Split(".");
                              if(PostTime_toString.Length==1) 
                                  finalCheck[j]=(PostTime_toString[0]);
                              else
                               finalCheck[j]=(PostTime_toString[1]); 
                         }

                        else if(showTime[1]=="minutes")
                        {
                              TimeSpan newTime = new TimeSpan(0,Convert.ToInt16(showTime[0]),0); 
                              PostTime = PostTime+newTime;
                              string[] PostTime_toString =PostTime.ToString().Split(".");
                              if(PostTime_toString.Length==1) 
                                  finalCheck[j]=(PostTime_toString[0]);
                              else
                                finalCheck[j]=(PostTime_toString[1]);  
                        }
                    else
                        finalCheck[j]=(exactPostTime[j]);
            }
            Array.Sort(finalCheck);//Sort Array to return Maximum Value
            return finalCheck[(exactPostTime.Length-1)];
             
        }
    }
}
