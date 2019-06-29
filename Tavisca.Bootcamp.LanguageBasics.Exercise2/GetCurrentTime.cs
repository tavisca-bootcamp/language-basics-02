using System;
using System.Text.RegularExpressions;
using System.Linq;
namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    class ForumPostEasy
    {
        public string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {
            int i=0;
            int lenghtOfExactPostTime=exactPostTime.Length;
            String[] currentTime = new String[lenghtOfExactPostTime];
            int k=0;
            for (int z = 0; z < exactPostTime.Length; z++)
            {
                for (int j = z + 1; j < exactPostTime.Length; j++)
                {
                    if (exactPostTime[z] == exactPostTime[j])
                    {
                        if (showPostTime[z] != showPostTime[j])
                        {
                            return "impossible";
                        }
                    }
                }
            }
           foreach(string s in exactPostTime)
           {
                int hours  = Int32.Parse(s.Substring(0,2));
                int minutes =Int32.Parse(s.Substring(3,2));
                int seconds = Int32.Parse(s.Substring(6,2));
                DateTime d = new DateTime(2019, 1, 1, hours, minutes, seconds);
               if(showPostTime[i].Contains("seconds"))
               {
                    currentTime[i]=s;
               }
               else if(showPostTime[i].Contains("minutes"))
                {
                   int minutesToAdd = Int32.Parse(showPostTime[i].Substring(0,2));
                   d=d.AddMinutes(minutesToAdd);
                   currentTime[i]=d.ToLongTimeString();
                   
                }
                else if(showPostTime[i].Contains("hours"))
                {
                   int hoursToAdd = Int32.Parse(showPostTime[i].Substring(0,2));
                   d=d.AddHours(hoursToAdd);
                   currentTime[i]=d.ToLongTimeString();
                }
               i++;
               k++;
           }
            Array.Sort(currentTime);
            String finalCurrentTime = currentTime[lenghtOfExactPostTime-1].Split(" ")[0];
            return finalCurrentTime;
            throw new NotImplementedException();
        }
    }
}