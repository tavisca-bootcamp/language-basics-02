using System;
using System.Collections.Generic;
using System.Text;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    class TimeAdd
    {
        public static void Hours(int[] time, int h)
        {
            time[0] += h;
        }

        public static void Minute(int[] time, int m)
        {
            time[1] += m;
            if (time[1] >= 60)
            {
                time[1] %= 60;
                Hours(time, 1);
            }
        }

        public static void Seconds(int[] time)
        {
            time[2] += 59;
            if (time[2] >= 60)
            {
                time[2] %= 60;
                Minute(time, 1);
            }
        }
    }
}
