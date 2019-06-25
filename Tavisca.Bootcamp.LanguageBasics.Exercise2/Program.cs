using System;
using System.Collections.Generic;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
  public static class Program
  {
    static void Main(string[] args)
    {
      Test(new[] { "12:12:12" }, new[] { "few seconds ago" }, "12:12:12");
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
      var currentTime = new List<TimeSpan>();
      int length = exactPostTime.Length;
      for (var index = 0; index < length; index++)
      {
        for (var nextIndex = index + 1; nextIndex < length; nextIndex++)
        {
          if (exactPostTime[index] == exactPostTime[nextIndex])
            if (showPostTime[index] != showPostTime[nextIndex])
              return "impossible";
        }

        TimeSpan timeSpan = TimeSpan.Parse(exactPostTime[index]);
        if (showPostTime[index].Contains("few"))
        {
          TimeSpan time = new TimeSpan(timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
          currentTime.Add(time);
        }
        else if (showPostTime[index].Contains("minutes"))
        {
          string minute = showPostTime[index].Split(' ')[0];
          timeSpan = timeSpan.Add(TimeSpan.FromMinutes(double.Parse(minute)));
          TimeSpan time = new TimeSpan(timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
          currentTime.Add(time);
        }
        else
        {
          string hours = showPostTime[index].Split(' ')[0];
          timeSpan = timeSpan.Add(TimeSpan.FromHours(double.Parse(hours)));
          TimeSpan time = new TimeSpan(timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
          currentTime.Add(time);
        }

      }
      currentTime.Sort();
      return currentTime[currentTime.Count - 1].ToString();
    }
  }
}
