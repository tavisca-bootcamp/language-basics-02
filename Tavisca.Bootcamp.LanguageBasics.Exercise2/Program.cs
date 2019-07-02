using System;
using System.Collections.Generic;

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
           // Console.ReadKey(true);
           //Test(new[] { "11:59:13", "11:13:23", "12:25:15" }, new[] { "23 hours ago", "46 minutes ago", "few seconds ago"}, "11:59:23");
        }

        private static void Test(string[] postTimes, string[] showTimes, string expected)
        {
            var postTimesCsv = string.Join(", ", postTimes);
            var showTimesCsv = string.Join(", ", showTimes);
            List<string> timeOfPosting = new List<string>();
            List<string> timeOfShowing = new List<string>();
            Dictionary<string, string> hs = new Dictionary<string, string>();
            string s="possible";
            for (int i=0; i<postTimes.Length; i++){
                if (!hs.ContainsKey(postTimes[i])){
                    timeOfPosting.Add(postTimes[i]);
                    timeOfShowing.Add(showTimes[i]);
                    hs.Add(postTimes[i], showTimes[i]);
                }
                else if (!hs.GetValueOrDefault(postTimes[i]).Equals(showTimes[i])){
                    s="impossible";
                    break;
                }
            }

            if (s.Equals("impossible")){
                Console.WriteLine($"[{postTimesCsv}], [{showTimesCsv}] => FAIL"); 
                return;   
            }

            Sorting.MergeSort(timeOfPosting, timeOfShowing, 0, timeOfPosting.Count - 1);
           /*for (int w=0; w<pt.Count; w++){
                Console.WriteLine(pt[w]+"    "+st[w]);
           }*/
            


            var result = Time.GetCurrentTime(timeOfPosting, timeOfShowing, ref s).Equals(expected) ? "PASS" : "FAIL";
           
            Console.WriteLine($"[{postTimesCsv}], [{showTimesCsv}] => {result}");
        }

        

        
    }
}
