using System;
using System.Collections.Generic;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise2
{
    class ForumPostEasy
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
                if (showPostTime[i].Contains("few"))
                {
                    TimeSpan time = new TimeSpan(timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
                    currentTime.Add(time);
                }
                else if (showPostTime[i].Contains("minutes"))
                {
                    string minute = showPostTime[i].Split(' ')[0];
                    timeSpan = timeSpan.Add(TimeSpan.FromMinutes(double.Parse(minute)));
                    TimeSpan time = new TimeSpan(timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
                    currentTime.Add(time);
                }
                else
                {
                    string hours = showPostTime[i].Split(' ')[0];
                    timeSpan = timeSpan.Add(TimeSpan.FromHours(double.Parse(hours)));
                    TimeSpan time = new TimeSpan(timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
                    currentTime.Add(time);
                }

            }
            currentTime.Sort();
            return currentTime[currentTime.Count - 1].ToString();
        }
    }
}
