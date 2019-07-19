using System;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public class ForumPostEasy{
        public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime){
            string[] currentTime = new string[showPostTime.Length]; 

            for (int i = 0; i < currentTime.Length; i++)
            {

                DateTime time = Convert.ToDateTime(exactPostTime[i]); 

                if (showPostTime[i].Contains("seconds"))
                {
                    currentTime[i] = exactPostTime[i];
                }

                else if (showPostTime[i].Contains("minutes"))
                {
                    string minutes = showPostTime[i].Split(' ')[0];
                    DateTime CurrentTime = time.AddMinutes(Int32.Parse(minutes));
                    string result = CurrentTime.ToString().Split(' ')[1];

                    currentTime[i] = result;
                }

                else if (showPostTime[i].Contains("hours"))
                {
                    string hours = showPostTime[i].Split(' ')[0];
                    DateTime CurrentTime = time.AddHours(Int32.Parse(hours)); 
                    string result = CurrentTime.ToString().Split(' ')[1];

                    currentTime[i] = result;
                }
                else
                    return "impossible";

            }
            Array.Sort(currentTime);
            return currentTime[currentTime.Length - 1]; 
        }
    }
}