using System;
using System.Linq;
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

         /************************************************************
          Idea is to solve this problem is to find innermost subset of the 
			time and return the first valid  time in that subset 
			 # 86400 means 24 hour
			 # 3599 means 3600s - 1s
			 # 59 means 60 s -1s


			**********************************************************************/
         // early detection of no solution
			for(int i=0;i< exactPostTime.Length;i++){

				for(int j=i+1;j<exactPostTime.Length;j++){
					if(exactPostTime[i].Equals(exactPostTime[j]))
					if(!showPostTime[i].Equals(showPostTime[j]))
					return "impossible";

				}
			}

			int totalsec,low,high;
			var sol_array_1=new TimeSpan[exactPostTime.Length];
			var sol_array_2=new TimeSpan[exactPostTime.Length];
			for(int i=0;i<exactPostTime.Length;i++){
				totalsec=Convert.ToInt32(TimeSpan.Parse(exactPostTime[i]).TotalSeconds);
				low=0;high=0;
				if(showPostTime[i].Contains("seconds")){
					low=0;
					high=59;
				}
				else if(showPostTime[i].Contains("minutes")){
					low=Int32.Parse(showPostTime[i].Split(" ")[0])*60;
					high=low+59;
				}
				else{
					low=Int32.Parse(showPostTime[i].Split(" ")[0])*60*60;
					high=3599+low;
				}
				sol_array_1[i]= new TimeSpan(0,0,(totalsec+low)%86400);
				sol_array_2[i]=new TimeSpan(0,0,(totalsec+high)%86400);
			}
			
			TimeSpan minSpan=sol_array_1.Max();
			TimeSpan maxSpan=sol_array_2.Min();
			/* Result lies between Max TimeSpan of sol_array_1 and min TimeSpan of sol_array_2  i.e innermost subset*/
			if(minSpan<=maxSpan){
				return minSpan.ToString();
			}
			return "impossible";
			
		}
		
	}
}
