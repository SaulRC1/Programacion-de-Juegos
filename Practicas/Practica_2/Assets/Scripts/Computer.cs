using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    public GameTimer gameTimer; // Referencia al script del temporizador

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E)) // Asumiendo que el jugador usa la tecla 'E' para interactuar
        {
            gameTimer.StartTimer();
        }
    }
}
