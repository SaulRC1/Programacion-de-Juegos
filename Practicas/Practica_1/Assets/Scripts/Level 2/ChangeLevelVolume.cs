using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLevelVolume : MonoBehaviour
{
    [SerializeField]
    private AudioSource levelVolume;

    // Start is called before the first frame update
    void Start()
    {
        while(!GameSettings.SettingsLoaded) { /* wait */ }

        Debug.Log("Level volume set to: " + GameSettings.GameVolume);
        levelVolume.volume = GameSettings.GameVolume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
