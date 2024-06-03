using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public static int score = 0;

    public static void incrementScore(int scoreIncrement)
    {
        score += scoreIncrement;
        Debug.Log("Current score: " + score);
    }
}
