using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class ScoreReader : MonoBehaviour
{
    public static Dictionary<string, string> scores;
    public static bool scoresLoaded = false;

    // Start is called before the first frame update
    void Start()
    {
        readScores();
        scoresLoaded = true;
    }

    private void readScores()
    {
        scores = new Dictionary<string, string>();

        StreamReader sr = null;

        try
        {
            string currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            //string projectFolder = Directory.GetParent(Directory.GetParent(currentPath).FullName).FullName;

            sr = new StreamReader("scores.txt");

            string line;

            while ((line = sr.ReadLine()) != null)
            {
                string[] splitLine = line.Split("=");

                scores.Add(splitLine[0], splitLine[1]);
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
        finally
        {
            if (sr != null)
            {
                sr.Close();
            }
        }
    }

    public static void storeScoresIntoFile()
    {
        StreamWriter streamWriter = null;

        try
        {
            string currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //string projectFolder = Directory.GetParent(Directory.GetParent(currentPath).FullName).FullName;

            streamWriter = new StreamWriter("scores.txt");

            List<KeyValuePair<string, string>> orderedScores = scores.ToList();

            orderedScores.Sort((pair1, pair2) =>
            {
                int score1 = Int32.Parse(pair1.Value);
                int score2 = Int32.Parse((pair2.Value));

                return score2.CompareTo(score1);
            });

            foreach (KeyValuePair<string, string> scorer in orderedScores)
            {
                streamWriter.WriteLine(scorer.Key + "=" + scorer.Value);
            }
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
