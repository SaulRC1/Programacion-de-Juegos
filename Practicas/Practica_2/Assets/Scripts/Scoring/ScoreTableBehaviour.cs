using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class ScoreTableBehaviour : MonoBehaviour
{
    [SerializeField]
    private TMP_Text[] names;

    [SerializeField]
    private TMP_Text[] scores;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int nameIndex = 0;
        int scoreIndex = 0;

        foreach (KeyValuePair<string, string> entry in ScoreReader.scores)
        {
            names[nameIndex].text = entry.Key;
            scores[scoreIndex].text = entry.Value;

            nameIndex++;
            scoreIndex++;
        }

    }
}
