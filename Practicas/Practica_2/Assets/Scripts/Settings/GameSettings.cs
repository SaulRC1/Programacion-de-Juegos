using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using UnityEngine;

public class GameSettings
{
    private static GameSettings instance = new GameSettings();

    private float gameVolume;
    private int gameTimeInMinutes;
    private int gameRounds;

    public const string GAME_VOLUME_SETTING_NAME = "game_volume";
    public const string GAME_TIME_SETTING_NAME = "game_time_minutes";
    public const string GAME_ROUNDS_SETTING_NAME = "game_rounds";

    private GameSettings() 
    {
        readGameSettingsFile();
    }

    public static GameSettings getInstance() { return instance; }
    
    private void readGameSettingsFile()
    {
        StreamReader sr = null;

        try
        {
            string currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            Debug.Log("Current path: " + currentPath);

            sr = new StreamReader(currentPath + "\\game_settings.txt");

            string line;

            while ((line = sr.ReadLine()) != null)
            {
                string[] splitLine = line.Split("=");

                switch (splitLine[0])
                {
                    case GAME_VOLUME_SETTING_NAME:

                        GameVolume = Convert.ToSingle(splitLine[1], CultureInfo.InvariantCulture);
                        break;

                    case GAME_TIME_SETTING_NAME: GameTimeInMinutes = Int32.Parse(splitLine[1]); break;
                    case GAME_ROUNDS_SETTING_NAME: GameRounds = Int32.Parse(splitLine[1]); break;
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);

            gameVolume = 0.5f;
            gameTimeInMinutes = 15;
            gameRounds = 5;
        }
        finally
        {
            if (sr != null)
            {
                sr.Close();
            }
        }
    }

    public void storeGameSettingsIntoFile()
    {
        StreamWriter streamWriter = null;

        try
        {
            string currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            streamWriter = new StreamWriter(currentPath + "\\game_settings.txt");

            streamWriter.WriteLine(GAME_VOLUME_SETTING_NAME + "=" + gameVolume.ToString(CultureInfo.InvariantCulture));
            streamWriter.WriteLine(GAME_TIME_SETTING_NAME + "=" + gameTimeInMinutes);
            streamWriter.WriteLine(GAME_ROUNDS_SETTING_NAME + "=" + gameRounds);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
        finally
        {
            if (streamWriter != null)
            {
                streamWriter.Close();
            }
        }
    }

    public float GameVolume
    {
        get { return gameVolume; }
        set
        {
            if (value > 1)
            {
                gameVolume = 1;
            }
            else if (value < 0)
            {
                gameVolume = 0;
            }
            else
            {
                gameVolume = value;
            }
        }
    }

    public int GameTimeInMinutes
    {
        get { return gameTimeInMinutes; }
        set
        {
            if (value < 1)
            {
                gameTimeInMinutes = 1;
            }
            else
            {
                gameTimeInMinutes = value;
            }
        }
    }

    public int GameRounds
    {
        get { return gameRounds; }
        set
        {
            if (value < 1)
            {
                gameRounds = 1;
            }
            else
            {
                gameRounds = value;
            }
        }
    }
}
