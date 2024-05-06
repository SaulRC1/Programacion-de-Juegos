using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsBehaviour : MonoBehaviour
{
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


    public void startButtonOnClick()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void settingsButtonOnClick()
    {
        settingsMenu.SetActive(true);
        titleText.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        settingsButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
    }

    public void exitButtonOnClick()
    {
        if(Application.isPlaying)
        {
           UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
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
