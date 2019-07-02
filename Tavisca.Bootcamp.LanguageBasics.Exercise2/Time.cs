using System;
using System.Collections.Generic;
using System.Text;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    class Time
    {
        public static string GetCurrentTime(List<string> timeOfPosting, List<string> timeOfShowing, ref string s)
        {
            //Console.WriteLine("1");
            int x = 0;
            int[] startOfFirst = new int[3];
            int[] endOfFirst = new int[3];
            foreach (string sp in timeOfPosting[0].Split(":"))
            {
                startOfFirst[x] = Convert.ToInt32(sp);
                endOfFirst[x] = Convert.ToInt32(sp);
                x++;
            }
            int f = startOfFirst[0];
            if (timeOfShowing[0].Substring(0, 1).Equals("f"))
                TimeAdd.Seconds(endOfFirst);
            else
            {
                int index = timeOfShowing[0].IndexOf(' ');
                int v = Convert.ToInt32(timeOfShowing[0].Substring(0, index));
                if (timeOfShowing[0].Substring(index + 1, 1).Equals("h"))
                {
                    TimeAdd.Hours(startOfFirst, v);
                    endOfFirst[0] = startOfFirst[0];
                    endOfFirst[1] = startOfFirst[1];
                    endOfFirst[2] = startOfFirst[2];
                    TimeAdd.Minute(endOfFirst, 59);
                }
                else
                {
                    TimeAdd.Minute(startOfFirst, v);
                    endOfFirst[1] = startOfFirst[1];
                    endOfFirst[2] = startOfFirst[2];
                }
                TimeAdd.Seconds(endOfFirst);
            }
            //Console.WriteLine("2 "+s+", "+c[0]+", "+c[1]+", "+c[2]+", count="+pt.Count);
            for (int j = 1; j < timeOfPosting.Count; j++)
            {

                x = 0;
                int[] startOfSecond = new int[3];
                int[] endOfSecond = new int[3];
                foreach (string sp in timeOfPosting[j].Split(":"))
                {
                    startOfSecond[x] = Convert.ToInt32(sp);
                    endOfSecond[x] = Convert.ToInt32(sp);
                    x++;
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

                intersection(startOfFirst, endOfFirst, startOfSecond, endOfSecond, ref s);

                if (s.Equals("impossible"))
                    return s;

            }
            startOfFirst[0] %= 24;

            string h = Convert.ToString(startOfFirst[0]);
            if (h.Length == 1)
                h = "0" + h;

            string m = Convert.ToString(startOfFirst[1]);
            if (m.Length == 1)
                m = "0" + m;

            string sec = Convert.ToString(startOfFirst[2]);
            if (sec.Length == 1)
                sec = "0" + sec;

            s = h + ":" + m + ":" + sec;

            return s;


        }

        public static void intersection(int[] startOfFirst, int[] endOfFirst, int[] startOfSecond, int[] endOfSecond, ref string s)
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
                s = "impossible";
                return;
            }
            if (startOfFirst[0] == endOfFirst[0])
            {
                if (startOfFirst[1] > endOfFirst[1])
                {
                    s = "impossible";
                    return;
                }
                else if (startOfFirst[1] == endOfFirst[1])
                {
                    if (startOfFirst[2] > endOfFirst[2])
                    {
                        s = "impossible";
                        return;
                    }
                }
            }


        }


        

    }
}
