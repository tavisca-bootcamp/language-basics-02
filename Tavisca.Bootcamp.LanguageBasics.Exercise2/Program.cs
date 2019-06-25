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
            var lengthOfTimeArray=exactPostTime.Length; 
            int exactPostTimeInSec,showPostTimeMin,showPostTimeMax;
            // To store min current time for every exact post time
            var currentTimeMin=new int[lengthOfTimeArray];
            // To store max current time for every exact post time
            var currentTimeMax=new int[lengthOfTimeArray];

            //Storing values in arrays declared above for current time calculation
            for(int i=0;i<lengthOfTimeArray;i++){
                //Evaluting exact post time in sec. Also checking if input of exact post time is in right format
                string[] str=exactPostTime[i].Split(":");
                if((str.Length==3)&&(str[0]!=null)&&(str[1]!=null)&&(str[2]!=null))
                exactPostTimeInSec=(Convert.ToInt32(str[0])*3600)+(Convert.ToInt32(str[1])*60)+Convert.ToInt32(str[2]);
                else
                return "impossible";
                //Verifying the show post time is in right format and storing it in an array
                str=showPostTime[i].Split(" ");
                if((str.Length!=3))
                return "impossible";
                if((str[0]==null)||(str[1]==null)||(str[2]==null))
                return "impossible";
                //calculation of min and max show post time in seconds
                if(str[1]=="seconds"){
                    showPostTimeMin=0;
                    showPostTimeMax=59;
                }
                else if(str[1]=="minutes"){
                    showPostTimeMin=Convert.ToInt32(str[0])*60;
                    showPostTimeMax=showPostTimeMin+59;
                }
                else{
                    showPostTimeMin=Convert.ToInt32(str[0])*60*60;
                    showPostTimeMax=3599+showPostTimeMin;
                }
                //calculation of max and min current time in seconds
                currentTimeMin[i]=(exactPostTimeInSec+showPostTimeMin)%86400;
                currentTimeMax[i]=(exactPostTimeInSec+showPostTimeMax)%86400;
            }

            //calculating max and min value for current time 
            int minOfCurrentTime=currentTimeMin[0],maxOfCurrentTime=currentTimeMax[0];
            for(int i=1;i<lengthOfTimeArray;i++){
                if(currentTimeMin[i]>minOfCurrentTime)
                minOfCurrentTime=currentTimeMin[i];
                if(currentTimeMax[i]<maxOfCurrentTime)
                maxOfCurrentTime=currentTimeMax[i];
            }
            //if min current time is less than max current time
            if(minOfCurrentTime<=maxOfCurrentTime){
                //calculation of current time in hours, mins and seconds
                int currentTimeHH,currentTimeMM,currentTimeSS;
                //minOfCurrentTime in sec
                currentTimeSS=minOfCurrentTime%60;
                //minOfCurrentTime in mins
                minOfCurrentTime=minOfCurrentTime/60;
                currentTimeMM=minOfCurrentTime%60;
                //minOfCurrentTime in hours
                minOfCurrentTime=minOfCurrentTime/60;
                currentTimeHH=minOfCurrentTime;
                //Converting time into string format
                string currentTime;
                //adding HH part
                if(currentTimeHH>9)
                currentTime=currentTimeHH+":";
                else
                currentTime="0"+currentTimeHH+":";
                //adding MM part
                if(currentTimeMM>9)
                currentTime=currentTime+currentTimeMM+":";
                else
                currentTime=currentTime+"0"+currentTimeMM+":";
                //adding SS part
                if(currentTimeSS>9)
                currentTime=currentTime+currentTimeSS;
                else
                currentTime=currentTime+"0"+currentTimeSS;
                return currentTime;
            }
            //if min current time is greater than max current time
            return "impossible";
        }
    }
}