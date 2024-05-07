using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuButtonsBehaviour : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField gameTimeInputField;

    [SerializeField]
    private TMP_InputField roundsInputField;

    [SerializeField]
    private Slider volumeSlider;

    [SerializeField]
    private GameObject settingsMenu;

    [SerializeField]
    private TMP_Text titleText;

    [SerializeField]
    private Button startButton;

    [SerializeField]
    private Button settingsButton;

    [SerializeField]
    private Button exitButton;

    public void acceptButtonOnClick()
    {
        GameSettings gameSettings = GameSettings.getInstance();

        gameSettings.GameVolume = volumeSlider.value;

        if(gameTimeInputField.text != null && gameTimeInputField.text.Length > 0 )
        {
            gameSettings.GameTimeInMinutes = Int32.Parse(gameTimeInputField.text);
        }
        else
        {
            gameSettings.GameTimeInMinutes = 15;
        }

        if (roundsInputField.text != null && roundsInputField.text.Length > 0)
        {
            gameSettings.GameRounds = Int32.Parse(roundsInputField.text);
        }
        else
        {
            gameSettings.GameRounds = 5;
        }

        gameSettings.storeGameSettingsIntoFile();

        exitSettingsMenu();
    }

    public void cancelButtonOnClick()
    {
        exitSettingsMenu();
    }

    private void exitSettingsMenu()
    {
        settingsMenu.SetActive(false);

        titleText.gameObject.SetActive(true);
        startButton.gameObject.SetActive(true);
        settingsButton.gameObject.SetActive(true);
        exitButton.gameObject.SetActive(true);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
