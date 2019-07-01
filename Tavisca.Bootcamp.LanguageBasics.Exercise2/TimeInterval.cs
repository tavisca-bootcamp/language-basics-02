using System;

public class TimeInterval
{
    private int Hours;
    private int Minutes;
    private int Seconds;
    public int[] StartTime ;
    public int[] EndTime ; //maintaing time intervals 
    public static readonly int HOURS = 0;
    public static readonly int MINUTES = 1;
    public static readonly int SECONDS = 2;
    public TimeInterval(int hours,int minutes,int seconds)
	{
        Hours = hours;
        Minutes = minutes;
        Seconds = seconds;
	}
    public void CalculateStartTime(int hours,int minutes,int seconds)
    {
        int[] temp = new int[3] { Hours, Minutes, Seconds };
        AddingSecondsToTime(seconds,temp);
        AddingMinutesToTime(minutes,temp);
        AddingHoursToTime(hours,temp);
        StartTime = temp;

    }
    public void CalculateEndTime(int hours, int minutes, int seconds)
    {
        int[] temp = new int[3] { StartTime[0], StartTime[1],StartTime[2]};
        AddingSecondsToTime(seconds, temp);
        AddingMinutesToTime(minutes, temp);
        AddingHoursToTime(hours,temp);
        EndTime = temp;

    }
    private void AddingSecondsToTime(int addedTime,int[] tempTime)
    {            if (addedTime == 0)
                  return;
       
                if (tempTime[2] == 0)
                    tempTime[2] = 59;


                else
                {
                    tempTime[2]--;
                    if (tempTime[1] < 59)
                        tempTime[1]++;
                    else
                    {
                        tempTime[1] = 0;
                        tempTime[0] = (tempTime[0] + 1) % 24;
                    }



               
                }
    }
    private void AddingMinutesToTime(int addedTime, int[] tempTime)
    {
        tempTime[1] = tempTime[1] + addedTime;
        if(tempTime[1]>59)
        {
            tempTime[1] = tempTime[1] % 60;
            tempTime[0] = (tempTime[0] + 1) % 24;
        }
    }
    private void AddingHoursToTime(int addedTime, int[] tempTime)
    {
        tempTime[0] = (tempTime[0] + addedTime) % 24;
    }
    
    public  void UpdateTimeInterval(int[,] startTime, int[,] endTime,int index)
    {
        startTime[index, HOURS] = StartTime[HOURS]; endTime[index, HOURS] = EndTime[HOURS];
        startTime[index, MINUTES] = StartTime[MINUTES]; endTime[index, MINUTES] = EndTime[1];
        startTime[index, SECONDS] = StartTime[SECONDS]; endTime[index, SECONDS] = EndTime[2];


    }

   
   }

