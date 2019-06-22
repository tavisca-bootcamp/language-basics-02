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

        public static string GetCurrentTime(string[] exactPostTimes, string[] showPostTimes)
        {
            string [] show = new string[3];
            int len = exactPostTimes.Length;
            DateTime [] output=new DateTime[2*len];
            DateTime[] t = new DateTime[len];
            DateTime[] t1 = new DateTime[2];
            int k=0;
            for(int i=0;i<len;i++){
                for(int j=1;j<len;j++){
                    if(exactPostTimes[i]==exactPostTimes[j]){
                        if(showPostTimes[i]!=showPostTimes[j]){
                            return "impossible";
                        }
                    }
                }
            }
            for(int i=0 ; i<len;i++){
                t[i] = DateTime.Parse(exactPostTimes[i]);
                show= showPostTimes[i].Split(' ');
                if(String.Equals(show[1],"seconds")){
                output[2*i]=t[i];
                output[2*i+1]=t[i].Add(new TimeSpan(0,0,59));
                }
            else if(String.Equals(show[1],"minutes")){
                k = Int32.Parse(show[0]);
                output[2*i]=t[i].Add(new TimeSpan(0,k,0));
                output[2*i+1]=t[i].Add(new TimeSpan(0,k,59));
                }
            else{
                    k = Int32.Parse(show[0]);
                    output[2*i]=t[i].Add(new TimeSpan(k,0,0));
                    output[2*i+1]=t[i].Add(new TimeSpan(k,59,59));
                }
            }
            t1[0] = output[0];
            t1[1] = output[1];
            for(int i=1;i<len;i++){
                if(output[2*i].TimeOfDay >t1[1].TimeOfDay || t1[0].TimeOfDay> output[2*i+1].TimeOfDay){
                    return "impossible";
                }
                t1[0]= t1[0].TimeOfDay>output[2*i].TimeOfDay ? t1[0] : output[2*i];
                t1[1]= t1[1].TimeOfDay<output[2*i+1].TimeOfDay ? t1[1] : output[2*i+1];
            }
            return t1[0].TimeOfDay.ToString();
        }
    }
}