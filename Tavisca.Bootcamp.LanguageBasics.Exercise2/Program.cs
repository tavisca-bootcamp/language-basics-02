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
            
            List<string> timeOfPosting = new List<string>();
            List<string> timeOfShowing = new List<string>();
            Dictionary<string, string> postShowTimeRelation = new Dictionary<string, string>();
            string result="PASS";
            for (int i=0; i<postTimes.Length; i++){
                if (!postShowTimeRelation.ContainsKey(postTimes[i])){
                    timeOfPosting.Add(postTimes[i]);
                    timeOfShowing.Add(showTimes[i]);
                    postShowTimeRelation.Add(postTimes[i], showTimes[i]);
                }
                else if (!postShowTimeRelation.GetValueOrDefault(postTimes[i]).Equals(showTimes[i])){
                    result="FAIL";
                    break;
                }
            }


            if (!result.Equals("FAIL"))
            {
                /*Now sort {timeOfPosting, timeOfShowing} on the basis of timeOfShowing in decreasing oder.
                 For example 1.-> {01:00:00, few seconds ago}, 2.-> {11:00:00, 2 hours ago}
                 It will arrange in following order
                 1.-> {11:00:00, 2 hours ago}, 2.-> {01:00:00, few seconds ago}
                 */
                Sorting.MergeSort(timeOfPosting, timeOfShowing, 0, timeOfPosting.Count - 1);

                result = Time.GetCurrentTime(timeOfPosting, timeOfShowing, result).Equals(expected) ? "PASS" : "FAIL";
            }

            var postTimesCsv = string.Join(", ", postTimes);
            var showTimesCsv = string.Join(", ", showTimes);
            Console.WriteLine($"[{postTimesCsv}], [{showTimesCsv}] => {result}");
        }

        

        
    }
}
