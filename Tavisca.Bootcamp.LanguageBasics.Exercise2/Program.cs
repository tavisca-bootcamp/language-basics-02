using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;

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
            String[] sorted=new String[exactPostTime.Length];
            String res;
            //============================================================================================================================================
            //finding the resultant minimum
            if(exactPostTime.Length==1)
            {
                
                String[] time=exactPostTime[0].Split(new char[]{':'}); //spliting original time
                String[] show=showPostTime[0].Split(" ");          //spliting show time
                int m=0,h=0;                                  
                if((show[1].Contains("seconds"))!=true)     
                {
                    if(show[1].Contains("minutes"))
                        m=Int32.Parse(show[0]);
                    if(show[1].Contains("hours"))
                        h=Int32.Parse(show[0]);
                }   

                int H=Int32.Parse(time[0]), M=Int32.Parse(time[1]), S=Int32.Parse(time[2]); //coverting to int

                //Console.WriteLine($"{time[0]} : {time[1]} : {time[2]} = {show[0]} {show[1]}");
                //System.Console.WriteLine($"{exactPostTime[i]} <-> {exactPostTime[i]}");
                //change of the time for every post
                if((M+m)>60)
                    {H=((H+1)%24);M=((M+m)%60);}
                else if((H+h)>24)
                    H=(H+h)%24;
                else
                    {H+=h;M+=m;}

                res=string.Format("{0:00}:{1:00}:{2:00}", H, M,S);    
                //System.Console.WriteLine($"H{H},M:{M},S{S}  h{h},m:{m} == {res}");
                return res;
            }
            //====================================================================================================================================                
            else
            {   //for impossible condition
                for(int j=0;j<exactPostTime.Length-1;j++)
                    if((exactPostTime[j]==exactPostTime[j+1])&&(showPostTime[j]!=showPostTime[j+1]))
                        {return "impossible";break;}
                //for multiple inputs
                for(int i=0;i<exactPostTime.Length;i++)
                {
                    String[] time=exactPostTime[i].Split(new char[]{':'}); //spliting original time
                    String[] show=showPostTime[i].Split(" ");          //spliting show time
                    int m=0,h=0;                                  
                    if((show[1].Contains("seconds"))!=true)     
                    {
                        if(show[1].Contains("minutes"))
                            m=Int32.Parse(show[0]);
                        if(show[1].Contains("hours"))
                            h=Int32.Parse(show[0]);
                    }   

                    int H=Int32.Parse(time[0]), M=Int32.Parse(time[1]), S=Int32.Parse(time[2]); //coverting to int

                    //Console.WriteLine($"{time[0]} : {time[1]} : {time[2]} = {show[0]} {show[1]}");
                    //System.Console.WriteLine($"{exactPostTime[i]} <-> {exactPostTime[i]}");
                    //change of the time for every post
                    if((M+m)>60)
                        {H=((H+1)%24);M=((M+m)%60);}
                    else if((H+h)>24)
                        H=(H+h)%24;
                    else
                        {H+=h;M+=m;}

                    res=string.Format("{0:00}:{1:00}:{2:00}", H, M,S);    
                    //System.Console.WriteLine($"H{H},M:{M},S{S}  h{h},m{m} == {res}");
                    sorted[i]=res;
                    }
                    
                var abc="";
                for(int i=0;i<sorted.Length-1;i++)
                {
                    if(string.Compare(sorted[i],sorted[i+1])==1)
                        abc=sorted[i];
                    else
                        abc=sorted[i+1];
                }
                //System.Console.WriteLine($"abc ={abc}");
                return abc;
            }
        
        }
    }
}