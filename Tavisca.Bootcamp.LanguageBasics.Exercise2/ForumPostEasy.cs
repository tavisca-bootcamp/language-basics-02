using System;
using System.Collections.Generic;
using System.Linq;
namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    class ForumPostEasy
    {
        public static string CurrentTime(string[] exactPostTime,string[] showPostTime)
        {
            if(IsValid(exactPostTime,showPostTime)==false)
                return "impossible";
            
            var length = exactPostTime.Length;
            var times = new List<TimeSpan>();

            for(var i=0;i<length;i++){
                var exactPost = exactPostTime[i];
                var showPost = showPostTime[i];
                times.Add(GetExactTime(exactPost,showPost));
            }

            times.Sort();
            return times[times.Count-1].ToString();
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

        private static TimeSpan GetExactTime(string exactPost,string showPost)
        {
            Tuple<Time, int> correctionInfo = TimeCorrector.GetCorrectionInfo(showPost);
            var postTime = TimeSpan.Parse(exactPost);
            switch(correctionInfo.Item1)
            {
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
                    return new TimeSpan(postTime.Hours,postTime.Minutes,postTime.Seconds);
            }
        }

    }
    
}