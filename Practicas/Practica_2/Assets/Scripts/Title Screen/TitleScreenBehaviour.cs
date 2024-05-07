using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenBehaviour : MonoBehaviour
{
    [SerializeField]
    private AudioSource titleScreenTheme;

    // Start is called before the first frame update
    void Start()
    {
        setGameVolume();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void setGameVolume()
    {
        GameSettings gameSettings = GameSettings.getInstance();

        titleScreenTheme.volume = gameSettings.GameVolume;
    }
}
