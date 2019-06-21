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

        public static string process(string ExactPostTime, string ShowPostTime){
                    int index = ExactPostTime.LastIndexOf(':');
                   string temp = ExactPostTime.Substring(index+1);
                   //get seconds, hours and mins
                   int sec = Convert.ToInt32(temp);
                   int min = Convert.ToInt32(ExactPostTime.Substring(ExactPostTime.IndexOf(':')+1,2));
                   int hrs = Convert.ToInt32(ExactPostTime.Substring(0, 2));
                   string currTime = "";
                   if(ShowPostTime.Equals("few seconds ago")){
                   sec = sec + 59;
                   min += sec / 60;
                   hrs += min / 60;
                   sec = sec % 60;
                   min = min % 60;
                   hrs = hrs % 24;
                   }
                   else if(ShowPostTime.Contains(" minutes ago")){
                       int ind = ShowPostTime.IndexOf(' '); // 59 mi
                       int X = Convert.ToInt32(ShowPostTime.Substring(0, ind));
                       min += X ;
                       hrs += min / 60;
                       min = min % 60;
                       hrs = hrs % 24;
                    }
                    else if(ShowPostTime.Contains(" hours ago")){
                        int ind = ShowPostTime.IndexOf(' ');
                        int X = Convert.ToInt32(ShowPostTime.Substring(0, ind));
                        hrs = hrs + 23;
                        hrs = hrs % 24;
                    }
                    //handled all three cases above
        string strHrs = "";
        if (hrs == 0) strHrs = hrs.ToString() + hrs.ToString();
        else strHrs = hrs.ToString();
         currTime = strHrs+":"+min.ToString()+":"+sec.ToString();
        if(ShowPostTime.Equals("few seconds ago")){
         if(ExactPostTime.CompareTo(currTime) < 0) return ExactPostTime;
         else return currTime;
        }
          return currTime;
        }

        public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {
            // Add your code here.
            string currminTime = "00:00:00";
            //should contain same number of elements
            if(exactPostTime.Length != showPostTime.Length) return "impossible";
            if(exactPostTime.Length == 1){
                return process(exactPostTime[0], showPostTime[0]);
            }
            
            else{
            for(int i = 0; i < exactPostTime.Length - 1; i++){
                //cannot have two different human readable messages for same time
                if(exactPostTime[i].Equals(exactPostTime[i+1]) && !(showPostTime[i].Equals(showPostTime[i+1])))
                    return "impossible";
            }
            
            string temp = "";
            for(int i = 0; i < exactPostTime.Length; i++){
                temp = process(exactPostTime[i],showPostTime[i]);
                if(currminTime.CompareTo(temp) < 0) currminTime = temp;
            }
            }

            return currminTime;
            //throw new NotImplementedException();
        }
    }
}
