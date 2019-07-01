using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions; 
namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    class ForumPostEasy{
        public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime){
            //checking if the pairs of exactposttime and showposttime are valid....i.e, if duplicates are present then their values must be equal
            if(IsValid(exactPostTime,showPostTime)==true){
                //list for storing currenttime
                var times = new List<TimeSpan>();
                for(int i=0;i<exactPostTime.Length;i++){
                    var exactPost = exactPostTime[i];
			        var showPost = showPostTime[i];
                    //finding current time and storing it into the list
                    var exactTime=GetExactTime(exactPost,showPost);
                    times.Add(exactTime);
                }
                times.Sort();
                //returning the last value of the list....i.e, maximum value
                return times[times.Count-1].ToString();
            }
            //if invalid
            return "impossible";
        }
       private static TimeSpan GetExactTime(string exactPost,string showPost){
           //correctionInfo is a 2 tuple storing the part of time to be changed and the value by which it is to be changed
            var correctionInfo = TimeCorrector.GetCorrectionInfo(showPost);
            var postTime = TimeSpan.Parse(exactPost);
            switch(correctionInfo.Item1){
                case Time.Hour:
                    postTime = postTime.Add(TimeSpan.FromHours(correctionInfo.Item2));
                    return new TimeSpan(postTime.Hours,postTime.Minutes,postTime.Seconds);
                case Time.Minute:
                    postTime = postTime.Add(TimeSpan.FromMinutes(correctionInfo.Item2));
                    return new TimeSpan(postTime.Hours,postTime.Minutes,postTime.Seconds);
                case Time.Second:
                    postTime = postTime.Add(TimeSpan.FromSeconds(correctionInfo.Item2));
                    return new TimeSpan(postTime.Hours,postTime.Minutes,postTime.Seconds);
                default:
                    return postTime;
            }
        }
        private static bool IsValid(string[] exactPostTime, string[] showPostTime)
        {
            var duplicates = exactPostTime
                    .Select((t, i) => new { Index = i, Text = t })
                    .GroupBy(g => g.Text)
                    .Where(g => g.Count() > 1);
            foreach (var duplicate in duplicates)
            {
                var showTimes = new List<string>();
                foreach (var post in duplicate)
                    showTimes.Add(showPostTime[post.Index]);
                if (showTimes != null && showTimes.Count > 0)
                {
                    var firstShowTime = showTimes[0];
                    var mismatch = showTimes.Where(s => s.Equals(firstShowTime, StringComparison.InvariantCultureIgnoreCase) == false);
                    if (mismatch != null && mismatch.Count() > 0)
                        return false;
                }
            }
            return true;
        }
    }
}