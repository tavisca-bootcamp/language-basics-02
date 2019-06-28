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

	    int finalHr=0,finalMin=0,finalSec=0;
           
            int length1=exactPostTime.Length;
            for(int i=0;i<length1;i++)
            {
                for(int j=i+1;j<length1;j++)
                {
			//same time of post 
                    if(exactPostTime[i]==exactPostTime[j])
                    {
				
                        if(!string.Equals(showPostTime[i],showPostTime[j]))
                        {
                            return "impossible";
                        }
                    }
                }
            }
            string[] temp=new string[length1];
            for(int i=0;i<length1;i++)
            { string s1,s2,s3;
                string[] t=exactPostTime[i].Split(':');
                string[] s=showPostTime[i].Split(' ');
                finalHr=int.Parse(t[0]);
                finalMin=int.Parse(t[1]);
                finalSec=int.Parse(t[2]);
                s3=finalSec.ToString();

              if(showPostTime[i].Contains("minutes"))
              {
                  
                  int mins=int.Parse(s[0]);
                  finalMin=finalMin+mins;
                  if(finalMin>59)
                  {finalMin=finalMin-60;
                   if(finalMin==0)
                   {
                       s2="00";
			//explicitly set 0 to 00
                   }
                   else
                   {
                        s2=finalMin.ToString();
                   }
                  
                   finalHr++;
                   if(finalHr>23)
                   {
                       finalHr=finalHr-24;
                       if(finalHr==0)
                       {
                           s1="00";
                       }
                       else
                       {
                           s1=finalHr.ToString();
                       }
                   }
                   else
                   {
                        s1=finalHr.ToString();
                   }
                   
                   }
                   else
                   {
                       s2=finalMin.ToString();
                       s1=finalHr.ToString();
                   }
                
              }
              else if(showPostTime[i].Contains("hours"))
              {  s2=finalMin.ToString();
                 int hrs=int.Parse(s[0]);
                 finalHr=finalHr+hrs;
                 if(finalHr>23)
                 { 
                     finalHr=finalHr-24;
                     if(finalHr==0)
                       {
                           s1="00";
                       }
                       else
                       {
                           s1=finalHr.ToString();
                       }
                 }
                 else
                 {
                     s1=finalHr.ToString();
                 }
                 
              }
              else
              {
                 s1=finalHr.ToString();
                 s2=finalMin.ToString();
              }
             
                temp[i]=s1+":"+s2+":"+s3;


            }
            Array.Sort(temp);
            Console.WriteLine(temp[length1-1]);
            return temp[length1-1];
            

            throw new NotImplementedException();
        }
    }
}
