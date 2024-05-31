using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimeCounterBehaviour : MonoBehaviour
{
    private TMP_Text gameTimeCounterText;

    private GameTimeCounter gameTimeCounter = GameTimeCounter.Instance;

    // Start is called before the first frame update
    void Start()
    {
        gameTimeCounterText = GetComponent<TMP_Text>();

        gameTimeCounter.InitializeGameTimeCounter(DateTime.Now);

        gameTimeCounterText.SetText(gameTimeCounter.ObtainRemainingTime());
    }

    // Update is called once per frame
    void Update()
    {
        string remainingTime = gameTimeCounter.ObtainRemainingTime();

        if (remainingTime == GameTimeCounter.NO_REMAINING_TIME)
        {
            SceneManager.LoadScene("Game Over Screen");
            return;
        }

        gameTimeCounterText.SetText(remainingTime);
    }
}
