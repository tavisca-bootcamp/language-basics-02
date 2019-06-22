

/*

Created By Ragu Balagi Karuppaiah.

Date : 22.06.2019


Solution For Second Assignment Second Problem.(Tavisca)



*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondProblem
{
    public class ForumPostEasy
    {
        string PostTime;
        
        public string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {
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
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the Array Limits");
            string arraySize = Console.ReadLine();
            int length = int.Parse(arraySize);
            string[] ept = new string[length];
            string[] spt = new string[length];

            Console.WriteLine("Enter the exact post time");

            for(int i=0; i<length; i++)
            {
                ept[i] = Console.ReadLine();
                
            }

            Console.WriteLine("Enter the Show time");

            for(int i=0; i<length; i++)
            {
                spt[i] = Console.ReadLine();
            }

            ForumPostEasy forumPostEasy = new ForumPostEasy();
            Console.WriteLine(forumPostEasy.GetCurrentTime(ept, spt));


        }
    }
}
