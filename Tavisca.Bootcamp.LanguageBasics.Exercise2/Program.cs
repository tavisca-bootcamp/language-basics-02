using System;
using System.Collections.Generic;

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
           // Console.ReadKey(true);
           //Test(new[] { "11:59:13", "11:13:23", "12:25:15" }, new[] { "23 hours ago", "46 minutes ago", "few seconds ago"}, "11:59:23");
        }

        private static void Test(string[] postTimes, string[] showTimes, string expected)
        {
            var postTimesCsv = string.Join(", ", postTimes);
            var showTimesCsv = string.Join(", ", showTimes);
            List<string> pt = new List<string>();
            List<string> st = new List<string>();
            Dictionary<string, string> hs = new Dictionary<string, string>();
            string s="possible";
            for (int i=0; i<postTimes.Length; i++){
                if (!hs.ContainsKey(postTimes[i])){
                    pt.Add(postTimes[i]);
                    st.Add(showTimes[i]);
                    hs.Add(postTimes[i], showTimes[i]);
                }
                else if (!hs.GetValueOrDefault(postTimes[i]).Equals(showTimes[i])){
                    s="impossible";
                    break;
                }
            }

            if (s.Equals("impossible")){
                Console.WriteLine($"[{postTimesCsv}], [{showTimesCsv}] => FAIL"); 
                return;   
            }

            mergeSort(pt, st, 0, pt.Count-1);
          /* for (int w=0; w<pt.Count; w++){
                Console.WriteLine(pt[w]+"    "+st[w]);
           }*/
            


            var result = GetCurrentTime(pt, st, ref s).Equals(expected) ? "PASS" : "FAIL";
            
            Console.WriteLine($"[{postTimesCsv}], [{showTimesCsv}] => {result}");
        }

        public static string GetCurrentTime(List<string> pt, List<string> st, ref string s)
        {
            //Console.WriteLine("1");
            int x=0;
            int[] c = new int[3];
            int[] d = new int[3];
            foreach (string sp in pt[0].Split(":")){
                c[x] = Convert.ToInt32(sp);
                d[x] = Convert.ToInt32(sp);
                x++;
            }
            int f = c[0];
            if (st[0].Substring(0, 1).Equals("f"))
                seconds(d);
            else{
                int index = st[0].IndexOf(' ');
                int v = Convert.ToInt32(st[0].Substring(0, index));
                if (st[0].Substring(index+1, 1).Equals("h")){
                    hours (c, v);
                    d[0]=c[0];
                    d[1]=c[1];
                    d[2]=c[2];
                    minute(d, 59);
                }
                else {
                    minute(c, v);
                    d[1]=c[1];
                    d[2]=c[2];
                }
                seconds(d);
            }
            //Console.WriteLine("2 "+s+", "+c[0]+", "+c[1]+", "+c[2]+", count="+pt.Count);
            for (int j=1; j<pt.Count; j++){
                
                x=0;
                int[] a = new int[3];
                int[] b = new int[3];
                foreach (string sp in pt[j].Split(":")){
                    a[x] = Convert.ToInt32(sp);
                    b[x] = Convert.ToInt32(sp);
                    x++;
                }

                if (a[0]<f){
                    a[0]+=24;
                    b[0]+=24;
                }

                if (st[j].Substring(0, 1).Equals("f"))
                    seconds(b);
                else{
                    int index = st[j].IndexOf(' ');
                    int v = Convert.ToInt32(st[j].Substring(0, index));
                    if (st[j].Substring(index+1, 1).Equals("h")){
                        hours(a, v);
                        b[0]=a[0];
                        b[1]=a[1];
                        b[2]=a[2];
                        minute(b, 59);
                    }
                    else {
                        minute(a, v);
                        b[1]=a[1];
                        b[2]=a[2];
                    }
                    seconds(b);
                }

                intersection(c, d, a, b, ref s);

                if (s.Equals("impossible"))
                    return s;

            }
            c[0]%=24;
            s=Convert.ToString(c[0])+":"+Convert.ToString(c[1])+":"+Convert.ToString(c[2]);
            //Console.WriteLine("s="+s);

            return s;


        }

        public static void intersection(int[] a, int[] b, int[] c, int[] d, ref string s){
            if (a[0]<c[0]){
                a[0]=c[0];
                a[1]=c[1];
                a[2]=c[2];
            }
            else if (a[0]==c[0]){
                if (a[1]<c[1]) {
                    a[1]=c[1];
                    a[2]=c[2];
                }
                else if (a[2]<c[2]) 
                    a[2]=c[2];
            }

            if (b[0]>d[0]){
                b[0]=d[0];
                b[1]=d[1];
                b[2]=d[2];
            }
            else if (b[0]==d[0]){
                if (b[1]>d[1]) {
                    b[1]=d[1];
                    b[2]=d[2];
                }
                else if (b[2]>d[2]) 
                    b[2]=d[2];
            }

            if (a[0]>b[0]){
                s="impossible";
                return;
            }
            if (a[0]==b[0]){
                if (a[1]>b[1]){
                    s="impossible";
                    return;
                }
                else if (a[1]==b[1]){
                    if (a[2]>b[2]){
                        s="impossible";
                        return;
                    }
                }
            }
            

        }
        

        public static void hours(int[] b, int h){
            b[0]+=h;
        }

        public static void minute(int[] b, int m){
            b[1]+=m;
            if (b[1]>=60){
                b[1]%=60;
                hours(b, 1);
            }
        }

        public static void seconds(int[] b){
            b[2]+=59;
            if (b[2]>=60){
                b[2]%=60;
                minute(b, 1);
            }
        }

        public static void mergeSort(List<string> pt, List<string> st, int l, int r){
            if (l<r){
                int m=(l+r)/2;
                mergeSort(pt, st, l, m);
                mergeSort(pt, st, m+1, r);
                merge(pt, st, l, m, r);
            }            
        }

        public static void merge(List<string> pt, List<string> st, int l, int m, int r){
            int n1=m-l+1;
            int n2=r-m;
            string[] pa = new string[n1];
            string[] sa = new string[n1];
            string[] pb = new string[n2];
            string[] sb = new string[n2];
            for (int i=l; i<=m; i++){
                pa[i-l]=pt[i];
                sa[i-l]=st[i];
            }
            for (int i=m+1; i<=r; i++){
                pb[i-m-1]=pt[i];
                sb[i-m-1]=st[i];
            }

            int x=0, y=0, z=0;
            while (x<n1  && y<n2){
                if (!showLess(sa[x], sb[y])){
                    pt[z]=pa[x];
                    st[z]=sa[x];
                    x++;
                }
                else{
                    pt[z]=pb[y];
                    st[z]=sb[y];
                    y++;
                }
                z++;
            }

            while (x<n1){
                 pt[z]=pa[x];
                 st[z]=sa[x];
                 x++;
                 z++;
            }
            while (y<n2){
                pt[z]=pb[y];
                st[z]=sb[y];
                y++;
                z++;
            }

        }

        public static bool showLess (string a, string b){
            if (a.Equals(b) || a.Substring(0, 1).Equals("f"))
                return true;
            if (b.Substring(0, 1).Equals("f"))
                return false;
            
            int i=a.IndexOf(' ');
            int j=b.IndexOf(' ');

            if (a.Substring(i+1).Equals(b.Substring(j+1))){
                int x = Convert.ToInt32(a.Substring(0, i));
                int y = Convert.ToInt32(b.Substring(0, j));
                if (x>=y)
                    return true;
                return false;
            }
            else if (a.Substring(i+1, 1).Equals("m"))
                return true;
            
            return false;
        }
    }
}
