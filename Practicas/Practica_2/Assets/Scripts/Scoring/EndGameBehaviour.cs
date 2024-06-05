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

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void endGame()
    {
        string immediatelyLowerKey = null;

        foreach (KeyValuePair<string, string> entry in ScoreReader.scores)
        {
            if (Int32.Parse(entry.Value) <= PlayerScore.score)
            {
                Debug.Log("Examining: " + entry.Key + " | " + entry.Value);
                immediatelyLowerKey = entry.Key;
            }
        }

        Debug.Log("Immediately Lower Scorer: " + immediatelyLowerKey);

        if (immediatelyLowerKey != null)
        {
            ScoreReader.scores.Remove(immediatelyLowerKey);

            if (nameInputField.text == null || nameInputField.text.Length == 0)
            {
                nameInputField.text = "DEFAULT";
            }

            ScoreReader.scores.Add(nameInputField.text, PlayerScore.score.ToString());
        }

        ScoreReader.storeScoresIntoFile();
        SceneManager.LoadScene(sceneName);
    }
}
