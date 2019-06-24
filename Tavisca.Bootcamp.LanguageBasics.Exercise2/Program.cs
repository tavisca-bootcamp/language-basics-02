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

       string PostTime;
        
        public string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {


            if(exactPostTime.Length > 1)
            {
                for (int i = 0; i < exactPostTime.Length; i++)
                {
                       for(int j=i+1; j<exactPostTime.Length; j++)
                    {
                        if(exactPostTime[i].Equals(exactPostTime[j]))
                        {
                            if(!showPostTime[i].Equals(showPostTime[j]))
                            {
                                return "impossible";
                            }
                        }
                    }
                }



            string[] results = new string[exactPostTime.Length];
            int[] sum = new int[exactPostTime.Length];
            int mintime = 0;
            string time;
            for(int i =0; i< exactPostTime.Length; i++)
            {
                //Console.WriteLine(exactPostTime[i]);

                int hr = int.Parse(exactPostTime[i].Split(':')[0]);
                int min = int.Parse(exactPostTime[i].Split(':')[1]);
                int sec = int.Parse(exactPostTime[i].Split(':')[2]);


                if(showPostTime[i].Contains("seconds"))
                {
                    int newsec = sec + 59;

                    if(newsec>60)
                    {
                        min = min + 1;
                        sec = sec % 60;
                    }
                    if(min>60)
                    {
                        hr = hr + 1;
                        min = min % 60;
                    }

                    if(hr>23)
                    {
                        hr = hr % 24;

                    }

                    time = hr + ":" + min + ":" + sec;
                    sum[i] = hr + min + sec;
                    results[i] = time; 

                }
                else if(showPostTime[i].Contains("minutes"))
                {
                    int st = int.Parse(showPostTime[i].Split(' ')[0]);

                    int newmin = min + st;
                    if (newmin > 60)
                    {
                        hr = hr + 1;
                        min = newmin % 60;
                    }

                    if (hr > 23)
                    {
                        hr = hr % 24;

                    }

                    time = hr + ":" + min + ":" + sec;
                    sum[i] = hr + min + sec;
                    results[i] = time;


                }
                else if(showPostTime[i].Contains("hours"))
                {
                    int st = int.Parse(showPostTime[i].Split(' ')[0]);

                    int newhr = hr + st;

                    if(newhr>24)
                    {
                        hr = newhr % 24;
                    }

                    time = hr + ":" + min + ":" + sec;
                    sum[i] = hr + min + sec;

                    results[i] = time;
                }


                //Console.WriteLine(hr + "" + min + "" + sec);


                /*if (showPostTime[i].Equals("few seconds ago"))
                {
                    PostTime = exactPostTime[i];
                }*/

            }
            /*
            for(int i=0; i< showPostTime.Length; i++)
            {
                Console.WriteLine(showPostTime[i]);
            }*/
          
            for(int i=0; i<sum.Length; i++)
            {
                int min = sum[0];

                if(min>sum[i])
                {
                    min = sum[i];
                    mintime = i;
                }
            }




            return results[mintime];
        }
    }
}
