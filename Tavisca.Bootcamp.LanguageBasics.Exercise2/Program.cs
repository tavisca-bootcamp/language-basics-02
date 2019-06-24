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
            int finalh=0,finalm=0,finals=0;
            //string s1,s2,s3;
            int l1=exactPostTime.Length;
            for(int i=0;i<l1;i++)
            {
                for(int j=i+1;j<l1;j++)
                {
                    if(exactPostTime[i]==exactPostTime[j])
                    {
                        if(!string.Equals(showPostTime[i],showPostTime[j]))
                        {
                            return "impossible";
                        }
                    }
                }
            }
            string[] curt=new string[l1];
            for(int i=0;i<l1;i++)
            { string s1,s2,s3;
                string[] t=exactPostTime[i].Split(':');
                string[] s=showPostTime[i].Split(' ');
                finalh=int.Parse(t[0]);
                finalm=int.Parse(t[1]);
                finals=int.Parse(t[2]);
                s3=finals.ToString();

                

              if(showPostTime[i].Contains("minutes"))
              {
                  
                  int mins=int.Parse(s[0]);
                  finalm=finalm+mins;
                  if(finalm>59)
                  {finalm=finalm-60;
                   if(finalm==0)
                   {
                       s2="00";
                   }
                   else
                   {
                        s2=finalm.ToString();
                   }
                  
                   finalh++;
                   if(finalh>23)
                   {
                       finalh=finalh-24;
                       if(finalh==0)
                       {
                           s1="00";
                       }
                       else
                       {
                           s1=finalh.ToString();
                       }
                   }
                   else
                   {
                        s1=finalh.ToString();
                   }
                   
                   }
                   else
                   {
                       s2=finalm.ToString();
                       s1=finalh.ToString();
                   }
                


              }
              else if(showPostTime[i].Contains("hours"))
              {  s2=finalm.ToString();
                 int hrs=int.Parse(s[0]);
                 finalh=finalh+hrs;
                 if(finalh>23)
                 { 
                     finalh=finalh-24;
                     if(finalh==0)
                       {
                           s1="00";
                       }
                       else
                       {
                           s1=finalh.ToString();
                       }
                 }
                 else
                 {
                     s1=finalh.ToString();
                 }
                 
              }
              else
              {
                 s1=finalh.ToString();
                 s2=finalm.ToString();
              }
             
              //s3=finals.ToString();
              curt[i]=s1+":"+s2+":"+s3;


            }
            Array.Sort(curt);
            Console.WriteLine(curt[l1-1]);
            return curt[l1-1];
            
            throw new NotImplementedException();
        }
    }
}
