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
            Console.ReadKey(true);
        }

        private static void Test(string[] postTimes, string[] showTimes, string expected)
        {
            var resultTimeSpan = GetCurrentTime(postTimes, showTimes).Equals(expected) ? "PASS" : "FAIL";
            var postTimesCsv = string.Join(", ", postTimes);
            var showTimesCsv = string.Join(", ", showTimes);
            Console.WriteLine($"[{postTimesCsv}], [{showTimesCsv}] => {resultTimeSpan}");
        }

        public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {

            //Check for contradicting time and human readable messages
            Dictionary<string,string> timeDict = new Dictionary<string,string>();
            for(int i=0;i<exactPostTime.Length;i++)
            {
                if(timeDict.ContainsKey(exactPostTime[i]))
                {
                    if(timeDict[exactPostTime[i]] != showPostTime[i])
                        return "impossible";
                }
                else
                    timeDict.Add(exactPostTime[i],showPostTime[i]);
            }
            
            
            List<string> timesList = new List<string>();
            TimeSpan resultTimeSpan,timeSpan1,timeSpan2;
            string resultTimeSpanTime;
            for(int i=0;i<exactPostTime.Length;i++)
            {
                string [] time = exactPostTime[i].Split(':');
                int hour = int.Parse(time[0]);
                int min = int.Parse(time[1]);
                int sec = int.Parse(time[2]); 
                timeSpan2 = new TimeSpan(hour,min,sec);

                timeSpan1 = GetTimeFromMessage(showPostTime[i]);
                resultTimeSpan = timeSpan2+timeSpan1;

                resultTimeSpanTime = resultTimeSpan.ToString();

               
                timesList.Add(resultTimeSpanTime);

            }

            return LexicographicallySmall(timesList);
            
        }

        private static string LexicographicallySmall(List<string> timesList)
        {
            // for(int i=0;i<timesList.Count;i++)
            // {
            //     for(int j=0;j<timesList.Count-1-i;j++)
            //     {
            //         if((timesList[j].Length>timesList[j+1].Length) && string.Compare(timesList[j],timesList[j+1])>1)
            //         {
            //             string temp = timesList[j];
            //             timesList[j] = timesList[j+1];
            //             timesList[j+1] = temp;
            //         }
            //     }
            // }
            String maxTime = timesList[0];

            for(int i=1;i<timesList.Count;i++)
            {   
                if(string.Compare(maxTime,timesList[i]) == -1 && !timesList[i].Contains("1."))
                {
                    maxTime = timesList[i];
                }

            }

             if(maxTime.Contains("1."))
                    maxTime = maxTime.Split("1.")[1];
            return maxTime;
        }

        //Convert human readable string to TimeSpan object
        private static TimeSpan GetTimeFromMessage(string timeInString)
        {
            String [] parseMessage = timeInString.Split(' ');
            if(parseMessage[1] == "seconds")
            {
               return new TimeSpan(0,0,0);
            }

            else if(parseMessage[1] == "minutes")
            {
                return new TimeSpan(0,int.Parse(parseMessage[0]),0);
            }

            else
            {
                return new TimeSpan(int.Parse(parseMessage[0]),0,0);
            }
        }
    }
}
