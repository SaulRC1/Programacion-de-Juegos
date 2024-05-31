using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeCounter
{
    private static GameTimeCounter instance = new GameTimeCounter();

    private DateTime startTimeStamp;
    private DateTime endTimeStamp;

    public const string NO_REMAINING_TIME = "00:00";

    private GameTimeCounter()
    {

    }

    public static GameTimeCounter Instance
    { get { return instance; } }

    public DateTime StartTimeStamp
    { 
        get { return startTimeStamp; }
        set {  startTimeStamp = value; } 
    }

    public DateTime EndTimeStamp
    {
        get { return startTimeStamp; }
        set { startTimeStamp = value; }
    }

    public void InitializeGameTimeCounter(DateTime startTimeStamp)
    {
        this.startTimeStamp = startTimeStamp;

        this.endTimeStamp = startTimeStamp.AddMinutes(GameSettings.getInstance().GameTimeInMinutes);
    }

    public string ObtainRemainingTime()
    {
        DateTime currentTimeStamp = DateTime.Now;
        
        bool isAfterEndTimeStamp = DateTime.Compare(endTimeStamp, currentTimeStamp) < 0;

        if(isAfterEndTimeStamp) 
        {
            return NO_REMAINING_TIME;
        }

        TimeSpan remainingTime = endTimeStamp.Subtract(currentTimeStamp);

        return remainingTime.ToString(@"mm\:ss");//string.Format("{0}:{1}", remainingTime.Minutes, remainingTime.Seconds);
    }
}
