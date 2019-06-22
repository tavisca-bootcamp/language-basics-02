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

        public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {
            int leng = exactPostTime.Length;
            DateTime[] exactT = new DateTime[leng];
            for(int i=0;i<leng;i++)
            {
                string[] temp = exactPostTime[i].Split(':');
                TimeSpan ts = new TimeSpan(Int32.Parse(temp[0]), Int32.Parse(temp[1]), Int32.Parse(temp[2]));
                exactT[i] = exactT[i].Date + ts;
                string[] tempShowData = showPostTime[i].Split(' ');
                if(tempShowData[0][0]!='f')
                {
                    int addd = Int32.Parse(tempShowData[0]);
                    if(tempShowData[1][0]=='m')
                    {
                       exactT[i]= exactT[i].AddMinutes(addd);
                    }
                    else if(tempShowData[1][0]=='h')
                    {
                       exactT[i]= exactT[i].AddHours(addd);
                    }
                }
            }
            string[] exactTstr = new string[leng];
            for(int i=0;i<leng;i++)
            {
                exactTstr[i] = exactT[i].ToString("HH:mm:ss", CultureInfo.InvariantCulture);
            }
            bool ans = checkImpossible(exactPostTime, showPostTime);
            if (ans)
            {
                return "impossible";
            }
            else
            {
                CultureInfo ci = CultureInfo.InvariantCulture;
                string lexiMax = exactTstr[0];
                for (int i = 1; i < leng; i++)
                {
                    string current = exactTstr[i];
                    int key = string.Compare(lexiMax, current, true);
                    if (key == -1)
                    {
                        lexiMax = current;
                    }
                }

                return lexiMax;
            throw new NotImplementedException();
        }
            static bool checkImpossible(string[] e, string[] s)
        {
            int leng = e.Length;
            for(int i=0;i<leng;i++)
            {
                for(int j=0;j<leng;j++)
                {
                    if (((string.Compare(e[i], e[j])) == 0) && ((string.Compare(s[i], s[j])) != 0)) return true;
                }
            }
            return false;
        }
    }
}
