using System;

public class TimeInterval
{
    private int Hours;
    private int Minutes;
    private int Seconds;
    public int[] StartTime ;
    public int[] EndTime ; //maintaing time intervals 
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
    public static bool FindIntersection(int [,]start,int[,] end,int[] result,int n)
    {
        
        int[] temp = new int[2];
        result[0] = start[0, 0] * 10000 + start[0, 1] * 100 + start[0, 2];
        result[1] = end[0, 0] * 10000 + end[0, 1] * 100 + end[0, 2];
        for (int i = 1; i < n; i++)
        {
            temp[0] = start[i, 0] * 10000 + start[i, 1] * 100 + start[i, 2];
            temp[1] = end[i, 0] * 10000 + end[i, 1] * 100 + end[i, 2];
            if (temp[0] >= result[0] && temp[0] < result[1])
            {
                result[0] = temp[0];
                result[1] = Math.Min(temp[1], result[1]);
            }
            else if (result[0] >= temp[0] && result[0] < temp[1])
            {
                result[1] = Math.Min(temp[1], result[1]);
            }
            else
                return false; 
        }
        return true;
    }

    public static string GettingStringFormat(int[] result)
    {
        if (result[0] > result[1])
            return "00:00:00";
        else
        {
            string temp = result[0].ToString();
            switch (temp.Length)
            {
                case 0: return "00:00:00";
                case 1: return String.Concat("00:00:0", temp);
                case 2: return String.Concat("00:00:", temp);
                case 3: return String.Concat("00:0", temp.Substring(0, 1), ":", temp.Substring(1, 2));
                case 4: return String.Concat("00:", temp.Substring(0, 2), ":", temp.Substring(2, 2));
                case 5: return String.Concat("0", temp.Substring(0, 1), ":", temp.Substring(1, 2), ":", temp.Substring(3, 2));
            }

            return String.Concat(temp.Substring(0, 2), ":", temp.Substring(2, 2), ":", temp.Substring(4, 2));
        }
    }
}
