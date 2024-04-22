using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using System.Reflection;
using UnityEditor;
using System.Globalization;

public class GameSettings : MonoBehaviour
{    
    private static float gameVolume;
    private static int playerLifes;
    private static int timeToDefeatBossInMinutes;

    public const string GAME_VOLUME_SETTING_NAME = "game_volume";
    public const string PLAYER_LIFES_SETTING_NAME = "player_lifes";
    public const string TIME_TO_DEFEAT_BOSS_SETTING_NAME = "time_to_defeat_boss_in_minutes";

    private static bool settingsLoaded = false;

    void Start()
    {
        readGameSettingsFile();
        settingsLoaded = true;
    }

    private void readGameSettingsFile()
    {
        StreamReader sr = null;

        try
        {
            string currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            Debug.Log("Current path: " + currentPath);

            string projectFolder = Directory.GetParent(Directory.GetParent(currentPath).FullName).FullName;

            Debug.Log("Project folder: " + projectFolder);

            sr = new StreamReader(projectFolder + "\\game_settings.txt");

            string line;

            while((line = sr.ReadLine()) != null)
            {
                string[] splitLine = line.Split("=");

                switch (splitLine[0])
                {
                    case GAME_VOLUME_SETTING_NAME:

                        GameVolume = Convert.ToSingle(splitLine[1], CultureInfo.InvariantCulture); 
                        break;

                    case PLAYER_LIFES_SETTING_NAME: PlayerLifes = Int32.Parse(splitLine[1]); break;
                    case TIME_TO_DEFEAT_BOSS_SETTING_NAME: TimeToDefeatBossInMinutes = Int32.Parse(splitLine[1]); break;
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);

            gameVolume = 0.5f;
            playerLifes = 1;
            timeToDefeatBossInMinutes = 15;
        }
        finally
        {
            if(sr != null)
            {
                sr.Close();
            }

            /*Debug.Log("Game volume: " + gameVolume);
            Debug.Log("Player lifes: " + playerLifes);
            Debug.Log("Time to defeat boss (minutes): " + timeToDefeatBossInMinutes);*/
        }
    }

    public static void storeGameSettingsIntoFile()
    {
        StreamWriter streamWriter = null;

        try
        {
            string currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string projectFolder = Directory.GetParent(Directory.GetParent(currentPath).FullName).FullName;

            streamWriter = new StreamWriter(projectFolder + "\\game_settings.txt");

            streamWriter.WriteLine(GAME_VOLUME_SETTING_NAME + "=" + gameVolume.ToString(CultureInfo.InvariantCulture));
            streamWriter.WriteLine(PLAYER_LIFES_SETTING_NAME + "=" + playerLifes);
            streamWriter.WriteLine(TIME_TO_DEFEAT_BOSS_SETTING_NAME + "=" + timeToDefeatBossInMinutes);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
        finally
        {
            if(streamWriter != null)
            {
                streamWriter.Close();
            }
        }
    }

    public static float GameVolume
    {
        get { return gameVolume;}
        set 
        {
            if(value > 1)
            {
                gameVolume = 1;
            }
            else if(value < 0)
            {
                gameVolume = 0;
            }
            else
            {
                gameVolume = value;
            } 
        }
    }

    public static int PlayerLifes
    {
        get { return playerLifes; }
        set
        {
            if (value > 5)
            {
                playerLifes = 5;
            }
            else if (value < 1)
            {
                playerLifes = 1;
            }
            else
            {
                playerLifes = value;
            }
        }
    }

    public static int TimeToDefeatBossInMinutes
    {
        get { return timeToDefeatBossInMinutes; }
        set
        {
            if (value < 15)
            {
                timeToDefeatBossInMinutes = 15;
            }
            else
            {
                timeToDefeatBossInMinutes = value;
            }
        }
    }

    public static bool SettingsLoaded
    {
        get { return settingsLoaded; }
        set
        {
            settingsLoaded = value;
        }
    }

}
