using System;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {

          public static Boolean possible(int[] bb,int[] cc,int l)
        {
            for(int i=0;i<l-1;i++)
            {
                for(int y=i+1;y<l;y++)
                {
                  if(bb[i]<bb[y])
                  {
                      int t=bb[i];
                      bb[i]=bb[y];
                      bb[y]=t;
                      t=cc[i];
                      cc[i]=cc[y];
                      cc[y]=t;
                      
                  }
                }
            }
              for(int i=0;i<l-1;i++)
              {
                  if(bb[i]==bb[i+1] && cc[i]!=cc[i+1])
                  return false;
              }
              return true;
        }
      
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

        public static string GetCurrentTime(string[] a, string[] b)
        {
            
            int sec,seca=0; 
            int l=a.Length;
            int[] m=new int[l];
            int[] n=new int[l];
            string[] k=new string[l];
            int[] bb=new int[l];
            int[] cc=new int[l];
             int[] dd=new int[l];
            for(int i=0;i<l;i++) // 1 time
            {
             string[] c=a[i].Split(':');
             string[] d=b[i].Split(' ');
             sec=Int32.Parse(c[0])*60*60+Int32.Parse(c[1])*60+Int32.Parse(c[2]);
             if(d[0].Equals("few"))
             seca=0;
             if(d[1].StartsWith('m'))
             seca=Int32.Parse(d[0])*60;
             if(d[1].StartsWith('h'))
             seca=Int32.Parse(d[0])*60*60;
             bb[i]=sec;
             cc[i]=seca;
             int secf=sec+seca;
             if(secf>=86400)
             secf=secf-86400;
             int hr=secf/3600;
             int secl1=secf%3600;
             int min=secl1/60;
             int secl2=secl1%60;
             if(hr==0)
              k[i]="00:"+min+":"+secl2;
             else
              k[i]=hr+":"+min+":"+secl2;
              dd[i]=hr*60*60+min*60+secl2;
            }
             if(!possible(bb,cc,l))
            {
            return "impossible";
            }
            
            for(int y=0;y<l-1;y++)
            {
           for(int u=y+1;u<l;u++)
           {
               if(dd[y]<dd[u])
               {
                   int j=dd[y];
                   dd[y]=dd[u];
                   dd[u]=j;
                   string t=k[y];
                   k[y]=k[u];
                   k[u]=t;
               }
           }
            }
             return k[0];
       
        }
    }
}
