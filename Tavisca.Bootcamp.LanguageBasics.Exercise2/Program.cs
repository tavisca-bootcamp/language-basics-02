using System;
using System.Globalization;
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
            var timing=new Dictionary<string,string>();
            var minute=0;
            var timeUpdated=0;
            for(int i=0;i<exactPostTime.Length;i++){
                if(timing.ContainsKey(exactPostTime[i])){
                    if (timing[exactPostTime[i]]!=showPostTime[i]){
                        return "impossible";
                    }
                }else{
                    timing.Add(exactPostTime[i],showPostTime[i]);
                }
            }

            DateTime currentTime=DateTime.ParseExact(exactPostTime[0],"HH:mm:ss",CultureInfo.InvariantCulture);
            DateTime updateTime=DateTime.ParseExact(exactPostTime[0],"HH:mm:ss",CultureInfo.InvariantCulture);

            foreach(var key in timing.Keys){
                if(timing[key].Split(" ")[1]=="seconds"){
                    if(DateTime.Compare(DateTime.ParseExact(key,"HH:mm:ss",CultureInfo.InvariantCulture),currentTime)>0){
                        currentTime=DateTime.ParseExact(key,"HH:mm:ss",CultureInfo.InvariantCulture);
                        
                    }
                    timeUpdated+=1;
                
                }else if(timing[key].Split(" ")[1]=="minutes"){
                    minute=Convert.ToInt16(timing[key].Split(" ")[0]);
                    updateTime=DateTime.ParseExact(key,"HH:mm:ss",CultureInfo.InvariantCulture).AddMinutes(minute);
                    
                    if(DateTime.Compare(updateTime,currentTime)>0){
                        currentTime=updateTime;
                        
                    }
                    timeUpdated+=1;
                }
            }
           
            if (timeUpdated==0){
                int hours=Convert.ToInt16(showPostTime[0].Split(" ")[0]);
                return Convert.ToString(DateTime.ParseExact(exactPostTime[0],"HH:mm:ss",CultureInfo.InvariantCulture).AddHours(hours).TimeOfDay);
            }

            return Convert.ToString(currentTime.TimeOfDay);
            throw new NotImplementedException();
        }
    }
}
