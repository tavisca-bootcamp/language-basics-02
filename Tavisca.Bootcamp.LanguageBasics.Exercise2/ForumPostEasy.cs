using System;
using System.Collections.Generic;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise2
{
    public static class ForumPostEasy
    {
        public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {
            var currentTime = new List<TimeSpan>();
            for (int i = 0; i < exactPostTime.Length; i++)
            {
                for (int j = i + 1; j < exactPostTime.Length; j++)
                {
                    if (exactPostTime[i] == exactPostTime[j])
                    {
                        if (showPostTime[i] != showPostTime[j])
                            return "impossible";
                    }
                }
                TimeSpan timeSpan = TimeSpan.Parse(exactPostTime[i]);
                TimeSpan time = GetTimeSpan(timeSpan, showPostTime[i]);
                currentTime.Add(time);
            }
            currentTime.Sort();
            return currentTime[currentTime.Count - 1].ToString();
        }
        public static TimeSpan GetTimeSpan(TimeSpan timeSpan,string showPostTime)
        {
            string time = showPostTime.Split(' ')[0];
            if (showPostTime.Contains("minutes"))
            {
                timeSpan = timeSpan.Add(TimeSpan.FromMinutes(double.Parse(time)));
            }
            if (showPostTime.Contains("hours"))
            {
                timeSpan = timeSpan.Add(TimeSpan.FromHours(double.Parse(time)));
            }
            return new TimeSpan(timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        }
    }
}
