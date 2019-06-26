using System;
public class ForumPostEasy
{
 static ForumPostEasy obj ;
 public static bool IsValidInput(int[,] times,string[][] postTime)
 {
     try{
     string compare_string = null;
     string isEmp = null;
     string[] hashTable = new string[29];
    int hash_value = 0,hash_var=6;
    for(int i=0;i<times.Length/6;i++)
     {
         for(int j=0;j<6;j++)
           {
                 hash_value+=times[i,j]*hash_var;
                 hash_var--;
           }
           hash_var = 6;
           hash_value = (hash_value/100)*3+(hash_value/10)*2+hash_value%10;
           isEmp = hashTable[hash_value];
           if(string.IsNullOrEmpty(isEmp))
               {
                  hashTable[hash_value] = postTime[i][0] + postTime[i][1];
               }
            else if(!string.IsNullOrEmpty(isEmp))
                {
                  compare_string = postTime[i][0] + postTime[i][1];  
                  if(hashTable[hash_value].Equals(compare_string)==false)
                      return false;
                }
                hash_value = 0;
     }
     }
     catch(Exception e)
      {
          Console.WriteLine(e);
      }
     return true;
 }
 public static int ChooseAppropriate(int[,] times)
 {
      int max = 0;
      for(int i=1;i<times.Length/6;i++)
       {
           for(int j=0;j<6;j++)
            {
               if(times[i,j]>times[max,j])
               {
                  max = i;
                  break;
               } 
               if(times[i,j]<times[max,j])
                {
                    break;
                }
            }
       }
       return max;
 }
 public void MakeChanges(int i,int lower_index,int upper_index,int[,] times,int occured_time,int time_upper_limit,string type)
 {
    if(lower_index<0||upper_index<0)
      return ;
    int f = 0;  
    int t = times[i,lower_index]*10+times[i,upper_index];
    int remaining  = time_upper_limit-t;
    if(remaining>occured_time)
     {
      f = t + occured_time;
      times[i,lower_index] = f/10;
      times[i,upper_index] = f%10;   
     }
     else
     {
       f = occured_time - remaining;
       times[i,lower_index] = f/10;
       times[i,upper_index] = f%10; 
       if(type=="Seconds")
       obj.MakeChanges(i,lower_index-2,upper_index-2,times,1,60,"Minutes");
       if(type=="Minutes")
       obj.MakeChanges(i,lower_index-2,upper_index-2,times,1,24,"Hours");
     }
 }
 public  static int GetTime(int[,] times,string[][] b)
  {
      for(int i=0;i<b.Length;i++)
        {
            if(b[i][1]=="Minutes")
             {
                 obj.MakeChanges(i,2,3,times,Convert.ToInt32(b[i][0]),60,"Minutes");
             }
             if(b[i][1]=="Hours")
            {
                obj.MakeChanges(i,0,1,times,Convert.ToInt32(b[i][0]),24,"Hours");
            }
            if(b[i][1]=="Seconds")
            {
                obj.MakeChanges(i,4,5,times,0,60,"Seconds");
            }
        }
        return ChooseAppropriate(times);
  }


  public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
  {
        char[][] timeChar = new char[exactPostTime.Length][];
        for(int i=0;i<exactPostTime.Length;i++)
      {
        timeChar[i] = exactPostTime[i].ToCharArray();
      }
       int k = 0;
      int[,] timeInt = new int[timeChar.Length,6];
       for(int i=0;i<timeChar.Length;i++)
       {
           for(int j=0;j<8;j++)
            {
                   if(timeChar[i][j]!=':')
                {
                  timeInt[i,k++] = timeChar[i][j]-48;
                }
            }
            k=0;
       }
       string[][] posts = new string[showPostTime.Length][];
       for(int i=0;i<showPostTime.Length;i++)
         {
             posts[i] = showPostTime[i].Split(' ');
         }

       if(!IsValidInput(timeInt,posts))
          return "false";

       int index_best_answer  =   GetTime(timeInt,posts);
       
        int count=0;
        for(int j=0;j<6;j++)
           {
        
             count++;
             Console.Write(timeInt[index_best_answer,j]);
             if(count%2==0)
              {
                if(count!=6)
                Console.Write(":");
              }
           }
           count=0;
           Console.WriteLine(" ");

        return null;
        }


  public static void  Main(String[] args)
  {
     string inputTimes = Console.ReadLine();
     string[] exactPostTime = inputTimes.Split(' ');
     string postTime = Console.ReadLine();
     string[] showPostTime = postTime.Split(',');
     obj = new ForumPostEasy();
     string isValid = GetCurrentTime(exactPostTime,showPostTime);
     string validParameter = "false";
     if(string.Compare(isValid,validParameter,true)==0)
        Console.WriteLine("INVALID");
  }
}
