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
            // Add your code here.
            //throw new NotImplementedException();
            int rh=-1, rm=-1, rs = -1;
            for (int i = 0; i < exactPostTime.Length; i++)
            {
                for (int j = 0; j < exactPostTime.Length; j++)
                {
                    if(exactPostTime[i].Equals(exactPostTime[j])&& !showPostTime[i].Equals(showPostTime[j]))
                    {
                        return "impossible";
                    }
                }
            }
            for (int i = 0; i < exactPostTime.Length; i++)
            {
                int[] myInts = Array.ConvertAll(exactPostTime[i].Split(':'), int.Parse);
                int gh = myInts[0], gm = myInts[1], gs = myInts[2];
                string[] sp = showPostTime[i].Split(separator: ' ');
                int X=-1;
                if (!sp[0].Equals("few"))
                {
                    X = int.Parse(sp[0]);
                }
                
                string unit = sp[1];
                if (unit.Equals("seconds"))
                {
                    rh = gh;
                   
                    if (rs == -1)
                    {
                        rm = gm;
                        rs = gs;
                    }
                    else if(gm == rm+1 || gm ==rm - 1)
                    {
                        rm = gm;
                        rs = gs;
                    }
                    //Console.Write(rh + ":" + rm + ":" + rs+ "\n");
                }
                else if (unit.Equals("minutes"))
                {
                    if (gh == 23 && gm+X>=60)
                    {
                        rh = 00;
                        rm = (gm + X) % 60;
                        if (rs == -1)
                        {
                            rs = gs;
                        }
                    }
                    else if(gm+X>=60)
                    {
                        rh = gh + 1;
                        rm = (gm + X) % 60;
                        rs = gs;

                    }
                    else
                    {
                        rh = gh;
                        rm = gm + X;
                       
                        rs = gs;
                    }
                    //Console.Write(rh + ":" + rm + ":" + rs+"\n");
                }
                else
                {
                    rh = (gh + X) % 24;
                    if (rm == -1)
                    {
                        rm = gm;
                        rs = gs;
                    }
                    //Console.Write(rh + ":" + rm + ":" + rs+"\n");
                }
            }
            string[] arr = new string[3];
            arr[0] = rh.ToString();
            arr[1] = rm.ToString();
            arr[2] = rs.ToString();
            for(int i=0;i<arr.Length;i++)
            {
                if (int.Parse(arr[i])<10)
                {
                    arr[i] = "0"+arr[i];
                }
            }
            //Console.Write(string.Join(':', arr));
            return string.Join(':', arr);


        }
    }
}
