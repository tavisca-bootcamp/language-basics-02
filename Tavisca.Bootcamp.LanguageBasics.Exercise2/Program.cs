using System;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(new[] {"12:12:12"}, new [] { "few seconds ago" }, "12:12:12");
            Test(new[] { "23:23:23", "23:23:23" }, new[] { "59 minutes ago", "59 minutes ago" }, "00:22:23");
            Test(new[] { "00:10:10", "00:10:10" }, new[] { "59 minutes ago", "1 hours ago" }, "impossible");
            Test(new[] { "11:59:13", "11:13:23", "12:25:15" }, new[] { "few seconds ago", "46 minutes ago", "23 hours ago" }, "11:59:23");
            Console.ReadKey(true);
        }

        private static void Test(string[] postTimes, string[] showTimes, string expected)
        {
            var result = GetCurrentTime(postTimes, showTimes).Equals(expected) ? "PASS" : "FAIL";
            var postTimesCsv = string.Join(", ", postTimes);
            var showTimesCsv = string.Join(", ", showTimes);
            Console.WriteLine($"[{postTimesCsv}], [{showTimesCsv}] => {result}");
        }

/*static void Main() {
/*Console.WriteLine("Hello World");
    string[]exactPostTime = {"12:12:12"};
   string[] showPostTime = {"few seconds ago"};*/
   
   string[]exactPostTime = {"11:59:13","11:13:23","12:25:15"};
   string[] showPostTime = {"few seconds ago","46 minutes ago","23 hours ago"};
   Console.WriteLine(GetCurrentTime(exactPostTime,showPostTime));
  }*/
  public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {
            string CTime = null;
            if (exactPostTime.Length == 1)
            {
                CTime = getIndex(exactPostTime[0], showPostTime[0]);
                return CTime;
            }
            int Matches = 0;
            for (int i = 0; i < showPostTime.Length; i++)
            {
                for (int j = i + 1; j < showPostTime.Length; j++)
                {
                    if (exactPostTime[i] == exactPostTime[j])
                    {
                        Matches++;
                        if (showPostTime[i] != showPostTime[j])
                        {
                            return "impossible";
                        }
                    }
                }
            }
            if ((Matches * 2) == exactPostTime.Length)
            {
                CTime = getIndex(exactPostTime[0], showPostTime[0]);
                return CTime;
            }
            string[] LowerBounds = findLowerBounds(exactPostTime, showPostTime);
            string[] UpperBounds = findUpperBounds(exactPostTime, showPostTime);
            List<string> TempTimes = new List<string>();
            for (int i = 0; i < LowerBounds.Length; i++)
            {
                TempTimes.Add(exactPostTime[i]);
               TempTimes.Add(UpperBounds[i]);
                TempTimes.Add(LowerBounds[i]);

            }
            TempTimes = TempTimes.Distinct().ToList();
            TempTimes.Sort();
            foreach (var time in TempTimes)
            {
                int IsMatched = 0;
                for (int i = 0; i < LowerBounds.Length; i++)
                {
                    if (IsInBetweenIntervals(LowerBounds[i], time, UpperBounds[i]) == 1)
                    {
                        IsMatched++;
                    }
                }
                if (IsMatched == LowerBounds.Length)
                {
                    return time;
                }
            }
            return "impossible";
        }
         public static string getIndex(String exactPostTime, string showPostTime)
        {
            if (showPostTime.Contains("few"))
            {
                return exactPostTime;
            }
            if (showPostTime.Contains("minutes"))
            {
                String[] Minutes = showPostTime.Split(' ');
                DateTime CTime = Convert.ToDateTime(exactPostTime);
                CTime = CTime.AddMinutes(Int32.Parse(Minutes[0]));
                string result = CTime.ToString().Split(' ')[1];
                return result;
            }
            if (showPostTime.Contains("hours"))
            {
                String[] Hours = showPostTime.Split(' ');
                DateTime CTime = Convert.ToDateTime(exactPostTime);
                CTime = CTime.AddHours(Int32.Parse(Hours[0]));
                string result = CTime.ToString().Split(' ')[1];
                return result;
            }
            return null;
        }
        public static int IsInBetweenIntervals(string LowerBound, string CurrentCheckTime, string UpperBound)
        {
            String[] UpperBoundTemp = UpperBound.Split(':');
            int UpperBoundHour = Int32.Parse(UpperBoundTemp[0]);
            int UpperBoundMinute = Int32.Parse(UpperBoundTemp[1]);
            int UpperBoundSeconds = Int32.Parse(UpperBoundTemp[2]);
            if (UpperBoundMinute == 0)
            {
                UpperBoundMinute = 60;
            }
            if (UpperBoundSeconds == 0)
            {
                UpperBoundSeconds = 60;
            }
            UpperBound = UpperBoundHour + ":" + UpperBoundMinute + ':' + UpperBoundSeconds;
            if (String.Compare(LowerBound, CurrentCheckTime) <= 0 && String.Compare(CurrentCheckTime, UpperBound) <= 0)
            {
                return 1;
            }
            return 0;
        }
        public static string[] findLowerBounds(string[] exactPostTime, string[] showPostTime)
        {
            string[] LowerBounds = new string[exactPostTime.Length];
            for (int i = 0; i < exactPostTime.Length; i++)
            {
                if (showPostTime[i].Contains("few"))
                {
                    LowerBounds[i] = exactPostTime[i];
                }
                else if (showPostTime[i].Contains("minutes"))
                {
                    String[] Minutes = showPostTime[i].Split(' ');
                    DateTime CTime = Convert.ToDateTime(exactPostTime[i]);
                    CTime = CTime.AddMinutes(Int32.Parse(Minutes[0]));
                    LowerBounds[i] = CTime.ToString().Split(' ')[1];
                }
                if (showPostTime[i].Contains("hours"))
                {
                    String[] Hours = showPostTime[i].Split(' ');
                    DateTime CTime = Convert.ToDateTime(exactPostTime[i]);
                    CTime = CTime.AddHours(Int32.Parse(Hours[0]));
                    LowerBounds[i] = CTime.ToString().Split(' ')[1];
                }
            }
            return LowerBounds;
        }
        public static string[] findUpperBounds(string[] exactPostTime, string[] showPostTime)
        {
            string[] UpperBounds = new string[exactPostTime.Length];
            for (int i = 0; i < exactPostTime.Length; i++)
            {
                if (showPostTime[i].Contains("few"))
                {
                    DateTime CTime = Convert.ToDateTime(exactPostTime[i]);
                    CTime = CTime.AddSeconds(59);
                    UpperBounds[i] = CTime.ToString().Split(' ')[1];
                }
                else if (showPostTime[i].Contains("minutes"))
                {
                    String[] Minutes = showPostTime[i].Split(' ');
                    DateTime CTime = Convert.ToDateTime(exactPostTime[i]);
                    CTime = CTime.AddMinutes(Int32.Parse(Minutes[0]));
                    CTime = CTime.AddSeconds(59);
                    UpperBounds[i] = CTime.ToString().Split(' ')[1];
                }
                if (showPostTime[i].Contains("hours"))
                {
                    String[] Hours = showPostTime[i].Split(' ');
                    DateTime CTime = Convert.ToDateTime(exactPostTime[i]);
                    CTime = CTime.AddHours(Int32.Parse(Hours[0]));
                    CTime = CTime.AddMinutes(59);
                    CTime = CTime.AddSeconds(59);
                    UpperBounds[i] = CTime.ToString().Split(' ')[1];
                }
            }
            return UpperBounds;
        }
       
    }
}
