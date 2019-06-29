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
            if(exactPostTime.Length != showPostTime.Length){
                return "Please give valid information.";
            }
            string ResultTime="impossible";

            TimeSpan[] ExactTimes = new TimeSpan[exactPostTime.Length];
            TimeSpan[] ShowTimes = new TimeSpan[exactPostTime.Length];

            for(int i=0; i<ExactTimes.Length; i++){
                try{
                    ExactTimes[i] = TimeSpan.Parse(exactPostTime[i]);
                }
                catch(Exception ex){
                    string resultError = "Please put a valid time. Error: "+ex;
                    return resultError;
                }
            }

             //Separating Shw Time
            string[] showPostWord1 = new string[showPostTime.Length];
            string[] showPostWord2 = new string[showPostTime.Length];
            //string[] shw3 = new string[showPostTime.Length];

            for(int i=0; i<showPostTime.Length; i++){
                string[] showPostWords = showPostTime[i].Split(" ");

                //Separating Hr, Min, Sec
                showPostWord1[i] = showPostWords[0];
                showPostWord2[i] = showPostWords[1];
                //shw3[i] = showPostWords[2];
            }

            //Checking for consistancy
            for(int i=0; i<ExactTimes.Length-1; i++){
                for(int j=i+1; j<ExactTimes.Length; j++){
                    if(ExactTimes[i] == ExactTimes[j]){
                        if(showPostWord1[i].Equals(showPostWord1[j]) && showPostWord2[i].Equals(showPostWord2[j])){
                            continue;
                        }else{
                            return "impossible";
                        }
                    }
                }
            }

            //Array for storing present or past day
            int[] day = new int[showPostTime.Length];
            for(int i=0; i<day.Length; i++){
                day[i] = 0;
            }

            for(int i=0; i<showPostTime.Length; i++){
                if(showPostWord1[i].Equals("few")){
                    ShowTimes[i] = ExactTimes[i];
                }else{
                    int number = Int32.Parse(showPostWord1[i]);

                    int[] time1 = {00,00,00};

                    //setting latest time
                    if(showPostWord2[i].Equals("seconds")){
                        if(number <= 59){
                            time1[0] = ExactTimes[i].Hours;
                            time1[1] = ExactTimes[i].Minutes;
                            time1[2] = ExactTimes[i].Seconds + number;

                            //rearranging time
                            if(time1[2] > 59){
                                time1[2] = time1[2] - 60;
                                time1[1] = time1[1] + 1;
                                if(time1[1] > 59){
                                    time1[1] = time1[1] - 60;
                                    time1[0] = time1[0] + 1;
                                    if(time1[0] > 23){
                                        time1[0] = time1[0] - 24;
                                        day[i] = -1;
                                    }
                                }
                            }
                        }
                        else{
                            return "put seconds less than 60";
                        }

                        ShowTimes[i] = TimeSpan.Parse(""+time1[0]+":"+time1[1]+":"+time1[2]);
                        
                    }
                    if(showPostWord2[i].Equals("hours")){
                        if(number <= 23){
                            time1[0] = ExactTimes[i].Hours + number;
                            time1[1] = ExactTimes[i].Minutes;
                            time1[2] = ExactTimes[i].Seconds;

                            //rearranging time
                            if(time1[0] > 23){
                                time1[0] = time1[0] - 24;
                                day[i] = -1;
                            }
                        }
                        else{
                            return "put hour in correct format";
                        }
                        ShowTimes[i] = TimeSpan.Parse(""+time1[0]+":"+time1[1]+":"+time1[2]);
                        
                    }
                    if(showPostWord2[i].Equals("minutes")){
                        if(number <= 59){
                            time1[0] = ExactTimes[i].Hours;
                            time1[1] = ExactTimes[i].Minutes + number;
                            time1[2] = ExactTimes[i].Seconds;

                            //rearranging time
                            if(time1[1] > 59){
                                    time1[1] = time1[1] - 60;
                                    time1[0] = time1[0] + 1;
                                    if(time1[0] > 23){
                                        time1[0] = time1[0] - 24;
                                        day[i] = -1;
                                    }
                                }
                        }
                        else{
                            return "put minutes less than 60";
                        }
                        ShowTimes[i] = TimeSpan.Parse(""+time1[0]+":"+time1[1]+":"+time1[2]);
                    }
                }
            }
            
            TimeSpan largestTime = ShowTimes[0];
            //
            int dayFlag = 0;
            for(int i=0; i<ShowTimes.Length; i++){
                if(day[i] == 0){
                    largestTime = ShowTimes[i];
                    break;
                }else{
                    dayFlag =1;
                }
            }

            for(int i=0; i<ShowTimes.Length; i++){
                if(ShowTimes[i] >= largestTime){
                    if(day[i] == 0){
                        largestTime = ShowTimes[i];
                    }else if(dayFlag == 1){
                        if(ShowTimes[i] >= largestTime){
                            largestTime = ShowTimes[i];
                        }
                    }
                }
            }

            ResultTime = largestTime.ToString();
            return ResultTime;
            throw new NotImplementedException();
        }
    }
}
