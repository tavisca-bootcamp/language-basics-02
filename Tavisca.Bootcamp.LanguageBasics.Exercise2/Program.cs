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
            
            if(exactPostTime.Length != showPostTime.Length){
                return "Please give valid information.";
            }
            string str="impossible";

            TimeSpan[] ts1 = new TimeSpan[exactPostTime.Length];
            TimeSpan[] ts2 = new TimeSpan[exactPostTime.Length];

            for(int i=0; i<ts1.Length; i++){
                try{
                    ts1[i] = TimeSpan.Parse(exactPostTime[i]);
                }
                catch(Exception ex){
                    string res = "Please put a valid time. Error: "+ex;
                    return res;
                }
            }

             //Separating Shw Time
            string[] shw1 = new string[showPostTime.Length];
            string[] shw2 = new string[showPostTime.Length];
            string[] shw3 = new string[showPostTime.Length];

            for(int i=0; i<showPostTime.Length; i++){
                string[] shwTime = showPostTime[i].Split(" ");

                //Separating Hr, Min, Sec
                shw1[i] = shwTime[0];
                shw2[i] = shwTime[1];
                shw3[i] = shwTime[2];
            }

            for(int i=0; i<ts1.Length-1; i++){
                for(int j=i+1; j<ts1.Length; j++){
                    if(ts1[i] == ts1[j]){
                        if(shw1[i] == shw1[j] && shw2[i].Equals(shw2[j])){
                            continue;
                        }else{
                            return "impossible";
                        }
                    }
                }
            }

            int[] day = new int[showPostTime.Length];
            for(int i=0; i<day.Length; i++){
                day[i] = 0;
            }

            for(int i=0; i<showPostTime.Length; i++){
                if(shw1[i].Equals("few")){
                    ts2[i] = ts1[i];
                }else{
                    int num = Int32.Parse(shw1[i]);

                    int[] time1 = {00,00,00};

                    //setting latest time
                    if(shw2[i].Equals("seconds")){
                        if(num <= 59){
                            time1[0] = ts1[i].Hours;
                            time1[1] = ts1[i].Minutes;
                            time1[2] = ts1[i].Seconds + num;

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

                        ts2[i] = TimeSpan.Parse(""+time1[0]+":"+time1[1]+":"+time1[2]);
                        
                    }
                    if(shw2[i].Equals("hours")){
                        if(num <= 23){
                            time1[0] = ts1[i].Hours + num;
                            time1[1] = ts1[i].Minutes;
                            time1[2] = ts1[i].Seconds;

                            //rearranging time
                            if(time1[0] > 23){
                                time1[0] = time1[0] - 24;
                                day[i] = -1;
                            }
                        }
                        else{
                            return "put hour in correct format";
                        }
                        ts2[i] = TimeSpan.Parse(""+time1[0]+":"+time1[1]+":"+time1[2]);
                        
                    }
                    if(shw2[i].Equals("minutes")){
                        if(num <= 59){
                            time1[0] = ts1[i].Hours;
                            time1[1] = ts1[i].Minutes + num;
                            time1[2] = ts1[i].Seconds;

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
                        ts2[i] = TimeSpan.Parse(""+time1[0]+":"+time1[1]+":"+time1[2]);
                    }
                }
            }
            
            TimeSpan lrgst = ts2[0];
            int all = 0;
            for(int i=0; i<ts2.Length; i++){
                if(day[i] == 0){
                    lrgst = ts2[i];
                    break;
                }else{
                    all =1;
                }
            }

            for(int i=0; i<ts2.Length; i++){
                if(ts2[i] >= lrgst){
                    if(day[i] == 0){
                        lrgst = ts2[i];
                    }else if(all == 1){
                        if(ts2[i] >= lrgst){
                            lrgst = ts2[i];
                        }
                    }
                }
            }

            str = lrgst.ToString();
            return str;
            throw new NotImplementedException();
        }
    }
}
