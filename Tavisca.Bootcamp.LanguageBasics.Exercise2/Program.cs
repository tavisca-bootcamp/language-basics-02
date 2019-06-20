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
            //Console.ReadKey(true);
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
            int l=exactPostTime.Length,EPT,SPT1,SPT2;
            int[] CT1=new int[l];
            int[] CT2=new int[l];
            for(int i=0;i<l;i++){
                String[] str=exactPostTime[i].Split(":");
                EPT=(Convert.ToInt32(str[0])*3600)+(Convert.ToInt32(str[1])*60)+Convert.ToInt32(str[2]);
                str=showPostTime[i].Split(" ");SPT1=0;SPT2=0;
                if(str[1]=="seconds"){
                    SPT1=0;
                    SPT2=59;
                }
                else if(str[1]=="minutes"){
                    SPT1=Convert.ToInt32(str[0])*60;
                    SPT2=SPT1+59;
                }
                else{
                    SPT1=Convert.ToInt32(str[0])*60*60;
                    SPT2=3599+SPT1;
                }
                CT1[i]=(EPT+SPT1)%86400;
                CT2[i]=(EPT+SPT2)%86400;
            }
            int min=CT1[0],max=CT2[0];
            for(int i=1;i<l;i++){
                if(CT1[i]>min)
                min=CT1[i];
                if(CT2[i]<max)
                max=CT2[i];
            }
            if(min<=max){
                int HH,MM,SS;
                SS=min%60;
                min=min/60;
                MM=min%60;
                min=min/60;
                HH=min;
                String s;
                if(HH>9)
                s=HH+":";
                else
                s="0"+HH+":";
                if(MM>9)
                s=s+MM+":";
                else
                s=s+"0"+MM+":";
                if(SS>9)
                s=s+SS;
                else
                s=s+"0"+SS;
                return s;
            }
            return "impossible";
            throw new NotImplementedException();
        }
    }
}
