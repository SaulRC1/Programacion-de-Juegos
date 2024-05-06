using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameBehaviour : MonoBehaviour
{
    [SerializeField]
    private string sceneName;

    [SerializeField]
    private TMP_InputField nameInputField;

    public void endGame()
    {
        int lowest = Int32.MaxValue;
        string lowestScorer = "";

        foreach (KeyValuePair<string, string> entry in ScoreReader.scores)
        {
            if(Int32.Parse(entry.Value) <= lowest)
            {
                lowestScorer = entry.Key;
            }
        }

        Debug.Log("Lowest Scorer: " + lowestScorer);

        ScoreReader.scores.Remove(lowestScorer);

        if(nameInputField.text == null || nameInputField.text.Length == 0 )
        {
            nameInputField.text = "DEFAULT";
        }

        ScoreReader.scores.Add(nameInputField.text, PlayerScore.score.ToString());

        ScoreReader.storeScoresIntoFile();
        SceneManager.LoadScene(sceneName);
    }
}
