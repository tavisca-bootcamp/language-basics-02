using System;
using System.Collections.Generic;
using System.Text;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise2
{
    public class ForumPostEasy
    {
        public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {
            int resultHour = -1, resultMinute = -1, resultSecond = -1;
            for (int i = 0; i < exactPostTime.Length; i++)
            {
                for (int j = 0; j < exactPostTime.Length; j++)
                {
                    if (exactPostTime[i].Equals(exactPostTime[j]) && !showPostTime[i].Equals(showPostTime[j]))
                    {
                        return "impossible";
                    }
                }
            }
            for (int i = 0; i < exactPostTime.Length; i++)
            {
                int[] myInts = Array.ConvertAll(exactPostTime[i].Split(':'), int.Parse);
                int givenHour = myInts[0], givenMinute = myInts[1], givenSecond = myInts[2];
                string[] showPostTimeArray = showPostTime[i].Split(separator: ' ');
                int X = -1;
                if (!showPostTimeArray[0].Equals("few"))
                {
                    X = int.Parse(showPostTimeArray[0]);
                }

                string unit = showPostTimeArray[1];
                if (unit.Equals("seconds"))
                {
                    resultHour = givenHour;

                    if (resultSecond == -1)
                    {
                        resultMinute = givenMinute;
                        resultSecond = givenSecond;
                    }
                    else if (givenMinute == resultMinute + 1 || givenMinute == resultMinute - 1)
                    {
                        resultMinute = givenMinute;
                        resultSecond = givenSecond;
                    }
                    //Console.Write(resultHour + ":" + resultMinute + ":" + resultSecond+ "\n");
                }
                else if (unit.Equals("minutes"))
                {
                    if (givenHour == 23 && givenMinute + X >= 60)
                    {
                        resultHour = 00;
                        resultMinute = (givenMinute + X) % 60;
                        if (resultSecond == -1)
                        {
                            resultSecond = givenSecond;
                        }
                    }
                    else if (givenMinute + X >= 60)
                    {
                        resultHour = givenHour + 1;
                        resultMinute = (givenMinute + X) % 60;
                        resultSecond = givenSecond;

                    }
                    else
                    {
                        resultHour = givenHour;
                        resultMinute = givenMinute + X;

                        resultSecond = givenSecond;
                    }
                    //Console.Write(resultHour + ":" + resultMinute + ":" + resultSecond+"\n");
                }
                else
                {
                    resultHour = (givenHour + X) % 24;
                    if (resultMinute == -1)
                    {
                        resultMinute = givenMinute;
                        resultSecond = givenSecond;
                    }
                    //Console.Write(resultHour + ":" + resultMinute + ":" + resultSecond+"\n");
                }
            }
            string[] arr = new string[3];
            arr[0] = resultHour.ToString();
            arr[1] = resultMinute.ToString();
            arr[2] = resultSecond.ToString();
            for (int i = 0; i < arr.Length; i++)
            {
                if (int.Parse(arr[i]) < 10)
                {
                    arr[i] = "0" + arr[i];
                }
            }
            //Console.Write(string.Join(':', arr));
            return string.Join(':', arr);


        }

    }
}
