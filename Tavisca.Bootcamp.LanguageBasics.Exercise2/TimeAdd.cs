using System;
using System.Collections.Generic;
using System.Text;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    class TimeAdd
    {
        public static void Hours(int[] time, int hour)
        {
            time[0] += hour;
        }

        public static void Minute(int[] time, int minute)
        {
            time[1] += minute;
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
