using System;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    class ForumPostEasy
    {
        private static DateTime min, max;

        public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {
            DateTime postTime, currentTime;
            string showTime;
            bool isImpossible;
            min = DateTime.Parse("00:00:00");
            max = DateTime.Parse("23:59:59");

            isImpossible = CheckForImpossible(exactPostTime, showPostTime);

            if (isImpossible)
            {
                return "impossible";
            }
            else
            {
                for (int i = 0; i < exactPostTime.Length; i++)
                {
                    postTime = DateTime.Parse(exactPostTime[i]);
                    currentTime = postTime;
                    showTime = showPostTime[i];

                    if (showTime.Contains("seconds"))
                    {
                        ForSecondsAndMinutes(currentTime);
                    }
                    else if (showTime.Contains("minutes"))
                    {
                        currentTime = postTime.AddMinutes(double.Parse(showTime.Split(' ')[0]));
                        ForSecondsAndMinutes(currentTime);
                    }
                    else if (showTime.Contains("hours"))
                    {
                        currentTime = postTime.AddHours(double.Parse(showTime.Split(' ')[0]));
                        ForHours(currentTime);
                    }
                }
            }
            return min.ToLongTimeString();
        }

        private static bool CheckForImpossible(string[] exactPostTime, string[] showPostTime)
        {
            for (int i = 0; i < exactPostTime.Length - 1; i++)
            {
                for (int j = i + 1; j < exactPostTime.Length; j++)
                {
                    if (exactPostTime[i] == exactPostTime[j])
                    {
                        if (showPostTime[i] != showPostTime[j])
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private static void ForSecondsAndMinutes(DateTime currentTime)
        {
            if (DateTime.Compare(DateTime.Parse(currentTime.ToLongTimeString()), DateTime.Parse(min.ToLongTimeString())) == 1)
            {
                min = currentTime;
            }
            if (DateTime.Compare(DateTime.Parse(currentTime.AddSeconds(59).ToLongTimeString()), DateTime.Parse(max.ToLongTimeString())) == -1)
            {
                max = currentTime.AddSeconds(59);
            }
        }

        private static void ForHours(DateTime currentTime)
        {
            if (DateTime.Compare(DateTime.Parse(currentTime.ToLongTimeString()), DateTime.Parse(min.ToLongTimeString())) == 1)
            {
                min = currentTime;
            }
            if (DateTime.Compare(DateTime.Parse(currentTime.AddMinutes(59).ToLongTimeString()), DateTime.Parse(max.ToLongTimeString())) == -1)
            {
                max = currentTime.AddMinutes(59);
            }
        }
    }
}
