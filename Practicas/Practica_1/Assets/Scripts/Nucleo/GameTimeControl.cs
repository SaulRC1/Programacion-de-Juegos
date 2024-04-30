using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeControl
{
    private static GameTimeControl instance = new GameTimeControl();

    private DateTime startingGameTime;

    private GameTimeControl() 
    {
        startingGameTime = DateTime.MinValue;
    }

    public static GameTimeControl GetInstance()
    {
        return instance;
    }

    public void startGameTime()
    {
        if(startingGameTime == DateTime.MinValue)
        {
            startingGameTime = DateTime.Now;
        }
    }

    public TimeSpan getCurrentGameTime(DateTime gameTime) 
    {
        if(gameTime.CompareTo(startingGameTime) < 0)
        {
            throw new Exception("The current game time specified is previous to the starting game time.");
        }

        return (gameTime - startingGameTime).Duration();
    }

    public bool checkIfTimeLimitHasBeenReached(DateTime currentDateTime, int timeLimitMinutes)
    {
        if (currentDateTime.CompareTo(startingGameTime) < 0)
        {
            throw new Exception("The current game time specified is previous to the starting game time.");
        }

        TimeSpan timeDifference = (currentDateTime - startingGameTime).Duration();

        Debug.Log("Time Difference: " + timeDifference.TotalMinutes);
        Debug.Log("Time limit minutes: " + timeLimitMinutes);

        return timeDifference.TotalMinutes >= timeLimitMinutes;
    }

    public void resetGameTime()
    {
        startingGameTime = DateTime.MinValue;
    }
}
