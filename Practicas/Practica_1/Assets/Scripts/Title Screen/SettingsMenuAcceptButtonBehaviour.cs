using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuAcceptButtonBehaviour : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject volumeSlider;
    public GameObject lifesNumberSlider;

    //SerializeField is for private objects that should
    //appear on the unity editor
    [SerializeField]
    private TMP_InputField timeToDefeatBossInputText;

    [SerializeField]
    private GameObject titleText;

    [SerializeField]
    private GameObject startButton;

    [SerializeField]
    private GameObject settingsButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void acceptButtonClick()
    {
        float volumeSliderValue = volumeSlider.GetComponent<Slider>().value;
        //Debug.Log("Volume: " + volumeSliderValue);

        int playerLifes = (int) lifesNumberSlider.GetComponent<Slider>().value;
        //Debug.Log("Player lifes: " + volumeSliderValue);

        int timeToDefeatBossInMinutes = Int32.Parse(timeToDefeatBossInputText.text);
        //Debug.Log("Time to Defeat Boss: " + timeToDefeatBossInMinutes);

        GameSettings.GameVolume = volumeSliderValue;
        GameSettings.PlayerLifes = playerLifes;
        GameSettings.TimeToDefeatBossInMinutes = timeToDefeatBossInMinutes;

        GameSettings.storeGameSettingsIntoFile();

        settingsMenu.SetActive(false);
        titleText.SetActive(true);
        startButton.SetActive(true);
        settingsButton.SetActive(true);
    }
}
