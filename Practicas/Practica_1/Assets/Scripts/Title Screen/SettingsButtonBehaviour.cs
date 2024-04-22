using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsButtonBehaviour : MonoBehaviour
{
    [SerializeField]
    private Slider volumeSlider;

    [SerializeField]
    private Slider playerLifesSlider;

    [SerializeField]
    private TMP_InputField timeToDefeatBossInputField;

    [SerializeField]
    private AudioSource titleScreenTheme;

    public void initializeSettingsMenu()
    {
        volumeSlider.value = GameSettings.GameVolume;
        playerLifesSlider.value = GameSettings.PlayerLifes;
        timeToDefeatBossInputField.text = GameSettings.TimeToDefeatBossInMinutes.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        while(!GameSettings.SettingsLoaded) { /* wait */ }

        //Debug.Log("Audio Source Volume: " + GameSettings.GameVolume);
        titleScreenTheme.volume = GameSettings.GameVolume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
