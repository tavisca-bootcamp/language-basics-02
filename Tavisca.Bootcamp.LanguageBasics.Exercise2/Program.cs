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
            //check for contradiction
           for(int i=0; i<exactPostTime.Length-1; i++)
            {
                for(int j=i+1; j<exactPostTime.Length; j++)
                {
                    if(exactPostTime[i].Equals(exactPostTime[j]))
                    {if(!(showPostTime[i].Equals(showPostTime[j])))
                        {
                            return "impossible";
                        }
                    }
                }
            }

            DateTime givenPostTime, CurrentTime;
            String result,temp;
        result="";
            for(int i=0; i<exactPostTime.Length; i++)
                {
                givenPostTime = DateTime.Parse(exactPostTime[i]);
                CurrentTime = givenPostTime;

                if(showPostTime[i].Contains("seconds"))
                {
                    CurrentTime = givenPostTime;
                }
                else if(showPostTime[i].Contains("minutes"))
                {
                  //adding given minutes 
                  CurrentTime = givenPostTime.AddMinutes(Convert.ToDouble(showPostTime[i].Substring(0,2)));

                }
                else if(showPostTime[i].Contains("hours"))
                {
                    //adding given hours
                    CurrentTime = givenPostTime.AddHours(Convert.ToDouble(showPostTime[i].Substring(0,2)));
                }

                  temp=CurrentTime.ToString().Substring(11,8);//storing current time to string
                  
               if(CurrentTime.ToString().Contains("PM")){//convert to 24 hr format
                    int hourDigit=Convert.ToInt32(temp.Substring(0,2));
                    if(hourDigit!=12){
                        hourDigit+=12;
                    }
                 temp=hourDigit+CurrentTime.ToString().Substring(13,6);
               }else{//for 12 AM to 00
                    int hourDigit=Convert.ToInt32(temp.Substring(0,2));
                     if(hourDigit==12){
                        temp="00"+CurrentTime.ToString().Substring(13,6);
                    }
                 
               }

                if(i==0)
                 {
                    result = temp;
                 }else{
                        if(string.Compare(result,temp) < 1)
                           {result = temp;}
                     }

                
            }

        return result;
        }
    }
}
