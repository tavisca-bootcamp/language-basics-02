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

        public static string AddTime(string postTime, string type, string showTime = ""){
			string[] time = postTime.Split(':');
			string temp = string.Empty;
			int finalSecs = Int32.Parse(time[2]+59)%60;
			int finalMins = Int32.Parse(time[1]);
			int finalHours = Int32.Parse(time[0]);
			
			// Seconds ago condition 
			if(type.Equals("s")){
				
				if(finalSecs < 59){
					finalMins += 1;
					finalMins %= 60;
				}
				if(finalMins == 0){
					finalHours += 1;
					finalHours %= 24;
				}
				//post time is always greater except below case
                //when final hour is 00 (after adding 59 to the second's value) and post time hour is not 00
				if(finalHours == 0 && Int32.Parse(time[0]) != 0){
					time[0] = finalHours.ToString();
					time[1] = finalMins.ToString();
					time[2] = "0";
				}
				for(int t=0; t < time.Length; t++){
					if(time[t].Length < 2){
						time[t] = "0"+time[t];
					}
				}
				
			 	temp = time[0] + ":" + time[1] + ":" + time[2];
				//Console.WriteLine("Seconds "  + temp);
			}
			// Minutes ago condition
			else if(type.Equals("m")){
				int minutes = Int32.Parse(showTime.Split()[0]);
				finalMins += minutes;
				if(finalMins > 59){
					time[0] = ((Int32.Parse(time[0])+1)%24).ToString();
				}
				finalMins %= 60;
				time[1] = finalMins.ToString();
                
                //checking which time is suitable according to other array values
				temp = AddTime(time[0]+":"+time[1]+":"+time[2],"s");
				//Console.WriteLine("Minutes " + temp);
			}
			// Hours ago condition
			else if(type.Equals("h")){
                //value of hours in showtime
				int hours = Int32.Parse(showTime.Split()[0]);
				
                finalHours += hours;
				finalHours %= 24;

                //updating hours in the time variable
				time[0] = finalHours.ToString();
                
                //checking which time is suitable according to other array values
				temp = AddTime(time[0]+":"+time[1]+":"+time[2],"m", "59 minutes ago");
				//Console.WriteLine("Hours " + temp);
			}
			
			return temp;
		}

        public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {
            // Add your code here.
            //throw new NotImplementedException();
			string finalTime = string.Empty;
			string firstOut = string.Empty;
			for(int i = 0; i < exactPostTime.Length; i++){
				string postTime = exactPostTime[i];
				string showTime = showPostTime[i];
				
				if(showTime.Equals("few seconds ago")){
					finalTime = AddTime(postTime, "s");
				}
				else if(showTime.Contains("minutes ago")){
					finalTime = AddTime(postTime, "m", showTime);
				}
				else if(showTime.Contains("hours ago")){
					finalTime = AddTime(postTime, "h", showTime);
				}
				
                // if this is first in the array
				if(firstOut.Equals(string.Empty)){
					firstOut = finalTime;
				}
                // subsequent values in array of current test case
				else if(!firstOut.Equals(finalTime)){
					finalTime = "impossible";
				}
			}
			
			return finalTime;
        }
    }
}
