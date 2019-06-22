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
        public static string[] findstarts(string[] exactPostTime, string[] showPostTime)
        {
            string[] starts = new string[exactPostTime.Length];
            int h, m, s;
            for (int i=0;i<exactPostTime.Length;i++)
            {
                if(showPostTime[i].Contains("few"))
                {
                    starts[i] = exactPostTime[i];
                }
                else if (showPostTime[i].Contains("minutes"))
                {
                    String[] extramin = showPostTime[i].Split(' ');
                    String[] hms = exactPostTime[i].Split(':');
                    h = Int32.Parse(hms[0]);
                    m = Int32.Parse(hms[1]);
                    s = Int32.Parse(hms[2]);
                    starts[i] = addtime(h, m, s, 0, Int32.Parse(extramin[0]), 0);
                }
                if (showPostTime[i].Contains("hours"))
                {
                    String[] extrahours = showPostTime[i].Split(' ');
                    String[] hms = exactPostTime[i].Split(':');
                    h = Int32.Parse(hms[0]);
                    m = Int32.Parse(hms[1]);
                    s = Int32.Parse(hms[2]);
                    starts[i] = addtime(h, m, s, Int32.Parse(extrahours[0]), 0, 0);
                }
            }
            return starts;
        }
        public static string[] findends(string[] exactPostTime, string[] showPostTime)
        {
            string[] ends = new string[exactPostTime.Length];
            int h, m, s;
            for (int i = 0; i < exactPostTime.Length; i++)
            {
                if (showPostTime[i].Contains("few"))
                {
                    String[] hms = exactPostTime[i].Split(':');
                    h = Int32.Parse(hms[0]);
                    m = Int32.Parse(hms[1]);
                    s = Int32.Parse(hms[2]);
                    ends[i] = addtime(h, m, s, 0, 0, 59);
                    
                }
                else if (showPostTime[i].Contains("minutes"))
                {
                    String[] extramin = showPostTime[i].Split(' ');
                    String[] hms = exactPostTime[i].Split(':');
                    h = Int32.Parse(hms[0]);
                    m = Int32.Parse(hms[1]);
                    s = Int32.Parse(hms[2]);
                    ends[i] = addtime(h, m, s, 0, Int32.Parse(extramin[0]), 59);
                }
                if (showPostTime[i].Contains("hours"))
                {
                    String[] extrahours = showPostTime[i].Split(' ');
                    String[] hms = exactPostTime[i].Split(':');
                    h = Int32.Parse(hms[0]);
                    m = Int32.Parse(hms[1]);
                    s = Int32.Parse(hms[2]);
                    ends[i] = addtime(h, m, s, Int32.Parse(extrahours[0]), 59, 59);
                }
            }
            return ends;
        }
        private static void Test(string[] postTimes, string[] showTimes, string expected)
        {
            var result = GetCurrentTime(postTimes, showTimes).Equals(expected) ? "PASS" : "FAIL";
            var postTimesCsv = string.Join(", ", postTimes);
            var showTimesCsv = string.Join(", ", showTimes);
            Console.WriteLine($"[{postTimesCsv}], [{showTimesCsv}] => {result}");
        }

        public static string singleindex(String exactPostTime, string showPostTime)
        {
            if (showPostTime.Contains("few"))
            {
                return exactPostTime;
            }
            if (showPostTime.Contains("minutes"))
            {
                String[] extramin = showPostTime.Split(' ');
                String[] hms = exactPostTime.Split(':');
                int h = Int32.Parse(hms[0]);
                int m = Int32.Parse(hms[1]);
                int s = Int32.Parse(hms[2]);
                string answer = addtime(h,m,s,0,Int32.Parse(extramin[0]),0);
                return answer;
            }
            if (showPostTime.Contains("hours"))
            {
                String[] extrahours = showPostTime.Split(' ');
                String[] hms = exactPostTime.Split(':');
                int h = Int32.Parse(hms[0]);
                int m = Int32.Parse(hms[1]);
                int s = Int32.Parse(hms[2]);
                string answer = addtime(h, m, s, Int32.Parse(extrahours[0]),0, 0);
                return answer;
            }
            return null;
        }
        public static int checkin(string s,string t,string e)
        {
            String[] sms = s.Split(':');
            int sh = Int32.Parse(sms[0]);
            int sm = Int32.Parse(sms[1]);
            int ss = Int32.Parse(sms[2]);
            String[] tms = t.Split(':');
            int th = Int32.Parse(tms[0]);
            int tm = Int32.Parse(tms[1]);
            int ts = Int32.Parse(tms[2]);
            String[] ems =e.Split(':');
            int eh = Int32.Parse(ems[0]);
            int em = Int32.Parse(ems[1]);
            int es = Int32.Parse(ems[2]);
            if (em == 0)
            {
                em = 60;
            }
            if(sh<=th && th<=eh)
            {
                if((sm<=tm && tm<=em)||(th<eh && tm>=sm))
                {
                    if ((ss<=ts && ts<=es)||(tm<em && ss<=ts)||(th<eh&&tm>sm))
                    {
                        return 1;
                    }
                }
            }
            return 0;
        }
        public static string addtime(int h,int m,int s,int eh,int em,int es)
        {
            int anh=0,anm=0,ans=0;
            string a, b, c;
            if((es+s)<60)
            {
                ans= es + s;
            }
            else 
            {
                ans = (es + s - 60);
                em = em + 1;
            }
            if(em+m<60)
            {
                anm = em + m;
            }
            else
            {
                anm = em + m - 60;
                eh = eh + 1;
            }
            anh = (eh + h)%24;
            if (anh==24)
            {
                anh = 0;
            }
            if (anh < 10)
            {
                 a = "0"+anh.ToString();
            }
            else
            {
                 a =anh.ToString();
            }
            if (anm < 10)
            {
                 b = "0" + anm.ToString();
            }
            else
            {
                 b = anm.ToString();
            }
            if (ans < 10)
            {
                 c = "0" + ans.ToString();
            }
            else
            {
                 c = ans.ToString();
            }
            string answer = a+":"+ b +":"+ c;
            return answer;

        }
        public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {
            string answer = null;
            if(exactPostTime.Length==1)
            {
                answer = singleindex(exactPostTime[0], showPostTime[0]);
                return answer;
            }
            int count = 0;
            for (int i=0;i<showPostTime.Length;i++)
            {
                for(int j=i+1;j<showPostTime.Length;j++)
                {
                    if(exactPostTime[i]==exactPostTime[j])
                    {
                        count++;
                        if(showPostTime[i]!=showPostTime[j])
                        {
                            return "impossible";
                        }
                    }
                }
            }
            if((count*2)==exactPostTime.Length)
            {
                answer = singleindex(exactPostTime[0], showPostTime[0]);
                return answer;
            }
            string[] starts = findstarts(exactPostTime, showPostTime);
            string[] ends = findends(exactPostTime, showPostTime);
            for (int j=0;j<starts.Length;j++)
            {
                int checks = 0;
                for (int i = 0; i<starts.Length; i++)
                {
                    if (checkin(starts[i], starts[j], ends[i]) == 1)
                    {
                        checks++;
                    }
                }
                if(checks==starts.Length)
                {
                    return starts[j];
                }
            }
            return "impossible";  
        }
    }
}
