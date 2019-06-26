using System;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise2
{
    class ForumPostEasy
    {
        //To FIND IF THE GIVEN CONDITION IS POSSIBLE OR NOT
 public Boolean possible(int[] total_second,int[] add_second,int length) // bb cc l
        {
            for(int i=0;i<length-1;i++)
            {
                for(int y=i+1;y<length;y++)
                {
                  if(total_second[i]<total_second[y])               
                  {
                      int t=total_second[i];
                      total_second[i]=total_second[y];
                      total_second[y]=t;
                      t=add_second[i];
                      add_second[i]=add_second[y];
                      add_second[y]=t;
                      
                  }
                }
            }
            //Two posts made in the same second cannot have two different times when they are read
              for(int i=0;i<length-1;i++)
              {
                  if(total_second[i]==total_second[i+1] && add_second[i]!=add_second[i+1])
                  return false;
              }
              return true;
        }

        public string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {
            int total_sec,add_sec=0; 
            int length=exactPostTime.Length;
            
            string[] result=new string[length]; 
            int[] total_second=new int[length];  
            int[] add_second=new int[length];  
             int[] final_second=new int[length]; 

            for(int i=0;i<length;i++) 
            {
             string[] ExactPost=exactPostTime[i].Split(':');
             string[] ShowPost=showPostTime[i].Split(' ');

             total_sec=Int32.Parse(ExactPost[0])*60*60+Int32.Parse(ExactPost[1])*60+Int32.Parse(ExactPost[2]);

             if(ShowPost[0].Equals("few"))
             add_sec=0;

             if(ShowPost[1].StartsWith('m'))
             add_sec=Int32.Parse(ShowPost[0])*60;

             if(ShowPost[1].StartsWith('h'))
             add_sec=Int32.Parse(ShowPost[0])*60*60;

             total_second[i]=total_sec;
             add_second[i]=add_sec;
             int final_sec=total_sec+add_sec; 

             //calculating hour,min,sec
             if(final_sec>=86400)
             final_sec=final_sec-86400;
             int hour=final_sec/3600; 
             int remain_sec=final_sec%3600; 
             int minutes=remain_sec/60; 
             int sec=remain_sec%60;  

             if(hour==0)
              result[i]="00:"+minutes+":"+sec;
             else
              result[i]=hour+":"+minutes+":"+sec;
              //calculating final_second for the forth test cases   
              final_second[i]=hour*60*60+minutes*60+sec;
            }

             if(!possible(total_second,add_second,length))
            {
            return "impossible";
            }
            
            //FOR LAST TEST CASE
            //performing sorting
            for(int y=0;y<length-1;y++)
            {
           for(int u=y+1;u<length;u++)
           {
               if(final_second[y]<final_second[u])
               {
                   int j=final_second[y];
                   final_second[y]=final_second[u];
                   final_second[u]=j;
                   string t=result[y];
                   result[y]=result[u];
                   result[u]=t;
               }
           }
            }
             return result[0];
       
        }

    }
    }