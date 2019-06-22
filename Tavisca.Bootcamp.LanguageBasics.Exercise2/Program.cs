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
            // Add your code here.
           /* Return impossible if same exactPostTime have different showPostTime */
            for (int i = 0; i < exactPostTime.Length; i++)
            {
                for (int j = i + 1; j < exactPostTime.Length; j++) 
                {
                    if ((exactPostTime[i] == exactPostTime[j])&& (showPostTime[i] != showPostTime[j]))
                            return "impossible";
                }
            } 
             
            /* Checking if all post time are equal */ 
            int f=0;
            for (int j = 0; j < exactPostTime.Length-1; j++) 
            {
                 if (exactPostTime[j] == exactPostTime[j+1] && showPostTime[j] == showPostTime[j+1] )
                     continue;
                 else f=1;
            }
            /* Returning current time if all post time are equal */
            if(f==0) 
            {
                   currentTime(exactPostTime[0],showPostTime,0);
            }
  
            /* Getting the current for all different post time*/
            string[] cur_time = new string[exactPostTime.Length];

            for (int j = 0; j < exactPostTime.Length; j++) 
            {
                      cur_time[j]=currentTime(exactPostTime[j],showPostTime,j);
                   
            }
              Array.Sort(cur_time);
              
            return cur_time[(exactPostTime.Length-1)];

            throw new NotImplementedException();
            
        }
         
         /* Computing current time */
      public static string currentTime(string time, string[] showPostTime,int j)
      {
                 TimeSpan res_time =  TimeSpan.Parse(time);
                 string[] s =   showPostTime[j].Split(null);
                  
                    if(s[0]!="few")
                    {   
                        if(s[1]=="hours")
                         {
                              TimeSpan res_time2 = new TimeSpan(Convert.ToInt16(s[0]), 0,0); 
                              res_time = res_time + res_time2;
                              string[] sx =res_time.ToString().Split(".");
                             
                              if(sx.Length==1)  return (sx[0]);
                              else  return sx[1];
                               
                         }
                        else if(s[1]=="minutes")
                        {
                              TimeSpan res_time2 = new TimeSpan(0,Convert.ToInt16(s[0]),0); 
                              res_time = res_time + res_time2;
                              string[] sx =res_time.ToString().Split(".");
                              
                              if(sx.Length==1)  return (sx[0]);
                              else  return sx[1];
                        }
                         
                    } 
                   
                 return time;
                    
         }

       
    }
}
