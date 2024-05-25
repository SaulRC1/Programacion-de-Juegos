using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float timeRemaining = 900; // 15 minutos en segundos
    public TMPro.TextMeshProUGUI timerText; // UI Text para mostrar el tiempo
    private bool timerIsRunning = false;

    void Start()
    {
        // Asegúrate de que el temporizador no esté corriendo al inicio
        timerIsRunning = false;
        timerText.gameObject.SetActive(false); // Ocultar el temporizador al inicio
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                //manejar lo que sucede cuando el tiempo se agota
                Debug.Log("El tiempo se ha agotado!");
            }
        }
    }

    public void StartTimer()
    {
        timerIsRunning = true;
        timerText.gameObject.SetActive(true); // Mostrar el temporizador cuando comience
    }

    private void UpdateTimerDisplay()
    {
        // Convierte los segundos restantes en minutos y segundos y actualiza el texto del temporizador
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
