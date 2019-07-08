using System;
using System.Collections.Generic;
using System.Text;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    class Time
    {
        public static string CurrentTime(List<string> timeOfPosting, List<string> timeOfShowing, string result)
        {
            
            int location = 0;
            int[] startOfFirst = new int[3];
            int[] endOfFirst = new int[3];
            foreach (string sp in timeOfPosting[0].Split(":"))
            {
                startOfFirst[location] = Convert.ToInt32(sp);
                endOfFirst[location] = startOfFirst[location];
                location++;
            }
            int f = startOfFirst[0];

            // if timeOfShowing is "few seconds ago"
            if (timeOfShowing[0][0]=='f')
                TimeAdd.Seconds(endOfFirst);

            else
            {
                //index of first space in timeOfShowing[0]
                int index = timeOfShowing[0].IndexOf(' ');
                int v = Convert.ToInt32(timeOfShowing[0].Substring(0, index));

                // if timeOfShowing is "y hours ago" where y is any number
                if (timeOfShowing[0][index + 1]=='h')
                {
                    TimeAdd.Hours(startOfFirst, v);
                    endOfFirst[0] = startOfFirst[0];
                    endOfFirst[1] = startOfFirst[1];
                    endOfFirst[2] = startOfFirst[2];
                    TimeAdd.Minute(endOfFirst, 59);
                }

                // if timeOfShowing is "y minutes ago" where y is any number
                else
                {
                    TimeAdd.Minute(startOfFirst, v);
                    endOfFirst[1] = startOfFirst[1];
                    endOfFirst[2] = startOfFirst[2];
                }
                TimeAdd.Seconds(endOfFirst);
            }
            
            for (int j = 1; j < timeOfPosting.Count; j++)
            {

                location = 0;
                int[] startOfSecond = new int[3];
                int[] endOfSecond = new int[3];
                foreach (string sp in timeOfPosting[j].Split(":"))
                {
                    startOfSecond[location] = Convert.ToInt32(sp);
                    endOfSecond[location] = Convert.ToInt32(sp);
                    location++;
                }

                if (startOfSecond[0] < f)
                {
                    startOfSecond[0] += 24;
                    endOfSecond[0] += 24;
                }

                if (timeOfShowing[j].Substring(0, 1).Equals("f"))
                    TimeAdd.Seconds(endOfSecond);
                else
                {
                    int index = timeOfShowing[j].IndexOf(' ');
                    int v = Convert.ToInt32(timeOfShowing[j].Substring(0, index));
                    if (timeOfShowing[j].Substring(index + 1, 1).Equals("h"))
                    {
                        TimeAdd.Hours(startOfSecond, v);
                        endOfSecond[0] = startOfSecond[0];
                        endOfSecond[1] = startOfSecond[1];
                        endOfSecond[2] = startOfSecond[2];
                        TimeAdd.Minute(endOfSecond, 59);
                    }
                    else
                    {
                        TimeAdd.Minute(startOfSecond, v);
                        endOfSecond[1] = startOfSecond[1];
                        endOfSecond[2] = startOfSecond[2];
                    }
                    TimeAdd.Seconds(endOfSecond);
                }

                intersection(startOfFirst, endOfFirst, startOfSecond, endOfSecond, ref result);

                if (result.Equals("impossible"))
                    return result;

            }
            startOfFirst[0] %= 24;

            string hour = Convert.ToString(startOfFirst[0]);
            if (hour.Length == 1)
                hour = "0" + hour;

            string min = Convert.ToString(startOfFirst[1]);
            if (min.Length == 1)
                min = "0" + min;

            string sec = Convert.ToString(startOfFirst[2]);
            if (sec.Length == 1)
                sec = "0" + sec;

            result = hour + ":" + min + ":" + sec;

            return result;


        }

        public static void intersection(int[] startOfFirst, int[] endOfFirst, int[] startOfSecond, int[] endOfSecond, ref string result)
        {
            if (startOfFirst[0] < startOfSecond[0])
            {
                startOfFirst[0] = startOfSecond[0];
                startOfFirst[1] = startOfSecond[1];
                startOfFirst[2] = startOfSecond[2];
            }
            else if (startOfFirst[0] == startOfSecond[0])
            {
                if (startOfFirst[1] < startOfSecond[1])
                {
                    startOfFirst[1] = startOfSecond[1];
                    startOfFirst[2] = startOfSecond[2];
                }
                else if (startOfFirst[2] < startOfSecond[2])
                    startOfFirst[2] = startOfSecond[2];
            }

            if (endOfFirst[0] > endOfSecond[0])
            {
                endOfFirst[0] = endOfSecond[0];
                endOfFirst[1] = endOfSecond[1];
                endOfFirst[2] = endOfSecond[2];
            }
            else if (endOfFirst[0] == endOfSecond[0])
            {
                if (endOfFirst[1] > endOfSecond[1])
                {
                    endOfFirst[1] = endOfSecond[1];
                    endOfFirst[2] = endOfSecond[2];
                }
                else if (endOfFirst[2] > endOfSecond[2])
                    endOfFirst[2] = endOfSecond[2];
            }

            if (startOfFirst[0] > endOfFirst[0])
            {
                result = "impossible";
                return;
            }
            if (startOfFirst[0] == endOfFirst[0])
            {
                if (startOfFirst[1] > endOfFirst[1])
                {
                    result = "impossible";
                    return;
                }
                else if (startOfFirst[1] == endOfFirst[1])
                {
                    if (startOfFirst[2] > endOfFirst[2])
                    {
                        result = "impossible";
                        return;
                    }
                }
            }


        }


        

    }
}
