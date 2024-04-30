using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimeCounterBehaviour : MonoBehaviour
{
    [SerializeField]
    private TMP_Text gameCounter;

    // Start is called before the first frame update
    void Start()
    {
        GameTimeControl.GetInstance().startGameTime();
        InvokeRepeating("updateGameTimeCounter", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void updateGameTimeCounter()
    {
        GameTimeControl gameTimeControl = GameTimeControl.GetInstance();

        DateTime currentDateTime = DateTime.Now;

        if(GameSettings.SettingsLoaded)
        {
            bool timeLimitReached = gameTimeControl.checkIfTimeLimitHasBeenReached(currentDateTime,
                GameSettings.TimeToDefeatBossInMinutes);

            if (timeLimitReached)
            {
                gameTimeControl.resetGameTime();
                SceneManager.LoadScene("Game Over");
            }
        }
        
        TimeSpan timeSpan = gameTimeControl.getCurrentGameTime(currentDateTime);

        //Debug.Log(currentDateTime.ToString("hh:mm:ss"));
        //Debug.Log("Starting Game DateTime: " + gameTimeControl)

        gameCounter.text = timeSpan.ToString(@"mm\:ss");
    }
}
